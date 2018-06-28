using DaGetCore.Domain;
using DaGetCore.Service.Dto;
using DaGetCore.Service.Tools;
using System;
using System.Linq;

namespace DaGetCore.Service
{
    public class BankAccountService : ServiceBase, IBankAccountService
    {
        public void Delete(Guid? userId, int id)
        {
            try
            {
                if (userId == null)
                    throw new DaGetServiceException("Impossible de supprimer le compte, utilisateur non défini");
            
                using (var context = Factory.CreateContext(ConnexionString))
                {
                    // on vérifie les droits
                    var baRepo = Factory.GetBankAccountRepository(context);
                    var bankAccount = ExtractBankAccount(userId, id, context);

                    var ubaRepo = Factory.GetUserBankAccountRepository(context);
                    UserBankAccount userbankAccount = ubaRepo.GetByUserPublicIdAndBankAccountId(userId.Value, id);

                    if (userbankAccount == null)
                        throw new DaGetServiceException("Impossible de supprimer le compte, compte non existant ou n'appartenant pas à l'utilisateur");

                    var baoRepo = Factory.GetBankAccountOperationTypeRepository(context);
                    foreach (var bao in baoRepo.GetAllByBankAccountId(id))
                    {
                        baoRepo.Delete(bao);
                    }

                    var oRepo = Factory.GetOperationRepository(context);
                    foreach (var o in oRepo.GetAllByBankAccountId(id))
                    {
                        oRepo.Delete(o);
                    }

                    var roRepo = Factory.GetReccurentOperationRepository(context);
                    foreach (var ro in roRepo.GetAllByBankAccountId(id))
                    {
                        roRepo.Delete(ro);
                    }

                    ubaRepo.Delete(userbankAccount);
                    baRepo.Delete(bankAccount);

                    context.Commit();
                }
            }
            catch (DaGetServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DaGetServiceException(
                    String.Format("Erreur lors de suppression du compte pour l'utilisateur {0}", userId), ex);
            }
        }

        public BankAccountDto Create(Guid? userId, string userName, BankAccountDto toCreate)
        {
            BankAccountDto result = null;

            try
            {
                if (userId == null)
                    throw new DaGetServiceException("Impossible de créer le compte, utilisateur non défini");

                using (var context = Factory.CreateContext(ConnexionString))
                {
                    var baRepo = Factory.GetBankAccountRepository(context);

                    // on vérifie que le nom du compte n'est pas déjà utilisé par l'utilisateur
                    if (baRepo.GetAllByIdUser(userId.Value).Any(ba => ba.Wording.Equals(toCreate.Wording)))
                        throw new DaGetServiceException("Vous possédez déjà un compte avec ce nom");

                    // création du compte
                    BankAccount bankAccountToAdd = new BankAccount()
                    {
                        BankAccountTypeId = toCreate.BankAccountTypeId.Value,
                        CreationDate = DateTime.Now,
                        DateSolde = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        Number = toCreate.Number,
                        Solde = 0M,
                        SoldeInitial = toCreate.SoldeInitial,
                        Wording = toCreate.Wording
                    };
                    baRepo.Add(bankAccountToAdd);

                    // création de l'user
                    var ubaRepo = Factory.GetUserBankAccountRepository(context);
                    UserBankAccount userBankAccountToAdd = new UserBankAccount()
                    {
                        BankAccountId = bankAccountToAdd.Id,
                        BankAccountAccessId = 1,
                        UserId = userId.Value,
                        UserName = userName
                    };
                    ubaRepo.Add(userBankAccountToAdd);

                    context.Commit();

                    result = bankAccountToAdd.ToDto();
                }
            }
            catch (DaGetServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DaGetServiceException(
                    String.Format("Erreur lors de création du compte pour l'utilisateur {0}", userId), ex);
            }

            return result;
        }

        public BankAccountDto GetById(Guid? userId, int id)
        {
            BankAccountDto toReturn = null;

            try
            {
                if (userId.HasValue)
                {
                    using (var context = Factory.CreateContext(ConnexionString))
                    {
                        toReturn = ExtractBankAccount(userId, id, context).ToDto();
                    }
                }
            }
            catch (DaGetServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DaGetServiceException(
                    String.Format("Erreur lors de la récupération du compte {0} pour l'utilisateur {1}", id, userId), ex);
            }

            return toReturn;
        }

        public BankAccountDto Update(Guid? userId, BankAccountDto toUpdate)
        {
            BankAccountDto toReturn = null;

            try
            {
                if (userId.HasValue && toUpdate != null && toUpdate.Id.HasValue)
                {
                    using (var context = Factory.CreateContext(ConnexionString))
                    {
                        BankAccount ba = ExtractBankAccount(userId, toUpdate.Id.Value, context);

                        var baRepo = Factory.GetBankAccountRepository(context);                     

                        ba.BankAccountTypeId = toUpdate.BankAccountTypeId.HasValue ? toUpdate.BankAccountTypeId.Value : ba.BankAccountTypeId;
                        ba.DateSolde = toUpdate.DateSolde.HasValue ? toUpdate.DateSolde.Value : ba.DateSolde;
                        ba.ModificationDate = DateTime.Now;
                        ba.Number = toUpdate.Number;
                        ba.Wording = toUpdate.Wording;

                        baRepo.Update(ba);
                    }
                }
                else
                {
                    throw new DaGetServiceException("Utilisateur ou compte inconnus");
                }
            }
            catch (DaGetServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DaGetServiceException(
                    String.Format("Erreur lors de la mise à jour du compte {0} pour l'utilisateur {1}", toUpdate.Id, userId), ex);
            }

            return toReturn;
        }       
    }
}
