using DaGetCore.Domain;
using DaGetCore.Service.Dto;
using DaGetCore.Service.Tools;
using System;
using System.Linq;

namespace DaGetCore.Service
{
    public class OperationService : ServiceBase, IOperationService
    {
        public OperationDto Create(Guid? userId, OperationDto toCreate)
        {
            try
            {
                if (toCreate == null || !toCreate.BankAccountId.HasValue)
                    throw new DaGetServiceException("Création de l'opération impossible, opération non définie ou non associée à un bank account");

                using (var context = Factory.CreateContext(ConnexionString))
                {
                    var opRepo = Factory.GetOperationRepository(context);
                    BankAccount ba = ExtractBankAccount(userId, toCreate.BankAccountId.Value, context); // une exception est lancée si le bank account n'appartient pas à l'user

                    // on vérifie que le type d'opération est bien possible pour ce compte
                    CheckIfOperationTypeIsCorrect(toCreate, context);

                    // on ajout l'opération
                    Operation toAdd = new Operation()
                    {
                        Amount = toCreate.Amount.Value,
                        BankAccountId = toCreate.BankAccountId.Value,
                        BankAccountOperationTypeId = toCreate.BankAccountOperationTypeId.Value,
                        Closed = false,
                        CreationDate = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        OperationDate = toCreate.OperationDate.Value,
                        ParentOperationId = toCreate.ParentOperationId
                    };

                    opRepo.Add(toAdd);

                    toCreate.Id = toAdd.Id;

                    // màj du solde du compte
                    ba.Solde += toAdd.Amount;
                    ba.ModificationDate = DateTime.Now;
                    var baRepo = Factory.GetBankAccountRepository(context);
                    baRepo.Update(ba);

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
                    String.Format("Erreur lors de la création d'une nouvelle opération par l'utilisateur {0}", userId), ex);
            }

            return toCreate;
        }

        private void CheckIfOperationTypeIsCorrect(OperationDto operation, Dal.Interface.IContext context)
        {
            var baOpTyRepo = Factory.GetBankAccountOperationTypeRepository(context);
            if (!baOpTyRepo.GetAllByBankAccountId(operation.BankAccountId.Value).Any(ot => ot.Id.Equals(operation.BankAccountOperationTypeId)))
                throw new DaGetServiceException(String.Format("Type d'opération {0} non autorisé sur ce compte {1}", operation.BankAccountOperationTypeId, operation.BankAccountId));
        }

        public void Delete(Guid? userId, int bankAccountId, int id)
        {
            try
            {    
                using (var context = Factory.CreateContext(ConnexionString))
                {
                    var opRepo = Factory.GetOperationRepository(context);
                    var baRepo = Factory.GetBankAccountRepository(context);

                    var toDelete = opRepo.GetById(id);
                    if (toDelete == null || toDelete.BankAccountId != bankAccountId)
                        throw new DaGetServiceException(String.Format("Operation d'id {0} inconnue", id));                    

                    BankAccount ba = ExtractBankAccount(userId, toDelete.BankAccountId, context);

                    ba.Solde -= toDelete.Amount;
                    ba.ModificationDate = DateTime.Now;
                    baRepo.Update(ba);

                    // suppression de l'opération
                    opRepo.Delete(toDelete);                  

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
                    String.Format("Erreur lors de la suppression de l'opération par l'utilisateur {0}", userId), ex);
            }
        }

        public OperationDto GetById(Guid? userId, int id)
        {
            OperationDto operation = null;

            try
            {
                using (var context = Factory.CreateContext(ConnexionString))
                {
                    var opRepo = Factory.GetOperationRepository(context);

                    Operation myOp = opRepo.GetById(id);
                    if (myOp != null)
                    {
                        BankAccount ba = ExtractBankAccount(userId, myOp.BankAccountId, context); // une exception est lancée si le bank account n'appartient pas à l'user
                        operation = myOp.ToDto();
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
                    String.Format("Erreur lors de la récupération de l'opération d'id {0} par l'utilisateur {0}", id, userId), ex);
            }

            return operation;
        }

        public OperationDto Update(Guid? userId, OperationDto toUpdate)
        {
            OperationDto toReturn = null;

            try
            {
                if (toUpdate == null || !toUpdate.BankAccountId.HasValue || !toUpdate.Id.HasValue)
                    throw new DaGetServiceException("Opération non définie ou mal construite");

                using (var context = Factory.CreateContext(ConnexionString))
                {
                    BankAccount ba = ExtractBankAccount(userId, toUpdate.BankAccountId.Value, context);
                    
                    CheckIfOperationTypeIsCorrect(toUpdate, context);

                    // on récupère l'opération en l'état pour la mise à jour du solde du compte
                    var opRepo = Factory.GetOperationRepository(context);
                    Operation myOperation = opRepo.GetById(toUpdate.Id.Value);

                    if (myOperation == null || myOperation.BankAccountId != ba.Id)
                        throw new DaGetServiceException("Opération non existante ou vous ne possédez pas les droits");

                    if(!myOperation.Amount.Equals(toUpdate.Amount)) // mise à jour du solde
                    {
                        ba.Solde -= myOperation.Amount;
                        ba.Solde += toUpdate.Amount.Value;
                        ba.ModificationDate = DateTime.Now;
                        var baRepo = Factory.GetBankAccountRepository(context);
                        baRepo.Update(ba);
                    }

                    myOperation.Amount = toUpdate.Amount.Value;
                    myOperation.BankAccountOperationType = null;
                    myOperation.BankAccountOperationTypeId = toUpdate.BankAccountOperationTypeId.Value;
                    myOperation.Closed = toUpdate.Closed.Value;
                    myOperation.ModificationDate = DateTime.Now;
                    myOperation.OperationDate = toUpdate.OperationDate.Value;
                    myOperation.ParentOperation = null;
                    myOperation.ParentOperationId = toUpdate.ParentOperationId.Value;

                    opRepo.Update(myOperation);

                    toReturn = myOperation.ToDto();

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
                    String.Format("Erreur lors de la mise à jour de l'opération d'id {0} par l'utilisateur {0}", toUpdate.Id, userId), ex);
            }

            return toReturn;
        }
    }
}
