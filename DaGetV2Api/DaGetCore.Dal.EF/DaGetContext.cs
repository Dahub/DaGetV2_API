using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace DaGetCore.Dal.EF
{
    public class DaGetContext : DbContext, IContext
    {
        public DaGetContext(DbContextOptions options) : base(options) { }

        public DbSet<BankAccountAccess> BankAccountAccess { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<UserBankAccount> UsersBankAccounts { get; set; }
        public DbSet<DefaultOperationType> DefaultOperationsTypes { get; set; }
        public DbSet<BankAccountOperationType> BankAccountOperationsTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<ReccurentOperation> ReccurentsOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<BankAccountAccess>().ToTable("BankAccountAccess");
            modelBuilder.Entity<BankAccountAccess>().HasKey(baa => baa.Id);
            modelBuilder.Entity<BankAccountAccess>().Property(baa => baa.Wording).HasColumnName("Wording").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);

            modelBuilder.Entity<BankAccountType>().ToTable("BankAccountTypes");
            modelBuilder.Entity<BankAccountType>().HasKey(bat => bat.Id);
            modelBuilder.Entity<BankAccountType>().Property(baa => baa.Wording).HasColumnName("Wording").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);

            modelBuilder.Entity<BankAccount>().ToTable("BankAccounts");
            modelBuilder.Entity<BankAccount>().HasKey(ba => ba.Id);
            modelBuilder.Entity<BankAccount>().Property(ba => ba.BankAccountTypeId).HasColumnName("FK_BankAccountType").HasColumnType("int").IsRequired();
            modelBuilder.Entity<BankAccount>().HasOne<BankAccountType>(ba => ba.BankAccountType).WithMany(bat => bat.BankAccounts).HasForeignKey(ba => ba.BankAccountTypeId);
            modelBuilder.Entity<BankAccount>().Property(ba => ba.CreationDate).HasColumnName("CreationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<BankAccount>().Property(ba => ba.DateSolde).HasColumnName("DateSolde").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<BankAccount>().Property(ba => ba.ModificationDate).HasColumnName("ModificationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<BankAccount>().Property(ba => ba.Number).HasColumnName("Number").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            modelBuilder.Entity<BankAccount>().Property(ba => ba.Solde).HasColumnName("Solde").HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<BankAccount>().Property(ba => ba.SoldeInitial).HasColumnName("SoldeInitial").HasColumnType("decimal(18, 2)").IsRequired();
            modelBuilder.Entity<BankAccount>().Property(ba => ba.Wording).HasColumnName("Wording").HasColumnType("nvarchar").HasMaxLength(512).IsRequired();

            modelBuilder.Entity<UserBankAccount>().ToTable("UsersBankAccounts");
            modelBuilder.Entity<UserBankAccount>().HasKey(uba => uba.Id);
            modelBuilder.Entity<UserBankAccount>().Property(uba => uba.UserName).HasColumnName("UserName").HasColumnType("nvarchar").HasMaxLength(32).IsRequired();
            modelBuilder.Entity<UserBankAccount>().Property(uba => uba.BankAccountAccessId).HasColumnName("FK_BankAccountAccess").HasColumnType("int").IsRequired();
            modelBuilder.Entity<UserBankAccount>().Property(uba => uba.BankAccountId).HasColumnName("FK_BankAccount").HasColumnType("int").IsRequired();
            modelBuilder.Entity<UserBankAccount>().Property(uba => uba.UserId).HasColumnName("UserId").HasColumnType("uniqueidentifier").IsRequired();
            modelBuilder.Entity<UserBankAccount>().HasOne<BankAccountAccess>(uba => uba.BankAccountAccess).WithMany(baa => baa.UsersBankAccounts).HasForeignKey(uba => uba.BankAccountAccessId);
            modelBuilder.Entity<UserBankAccount>().HasOne<BankAccount>(uba => uba.BankAccount).WithMany(ba => ba.UsersBankAccounts).HasForeignKey(uba => uba.BankAccountId);

            modelBuilder.Entity<DefaultOperationType>().ToTable("DefaultOperationsTypes");
            modelBuilder.Entity<DefaultOperationType>().HasKey(dot => dot.Id);
            modelBuilder.Entity<DefaultOperationType>().Property(dot => dot.Wording).HasColumnName("Wording").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);

            modelBuilder.Entity<BankAccountOperationType>().ToTable("BankAccountOperationsTypes");
            modelBuilder.Entity<BankAccountOperationType>().HasKey(baot => baot.Id);
            modelBuilder.Entity<BankAccountOperationType>().Property(baot => baot.Wording).HasColumnName("Wording").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            modelBuilder.Entity<BankAccountOperationType>().Property(baot => baot.BankAccountId).HasColumnName("FK_BankAccount").HasColumnType("int").IsRequired();
            modelBuilder.Entity<BankAccountOperationType>().HasOne<BankAccount>(baot => baot.BankAccount).WithMany(ba => ba.BankAccountOperationsTypes).HasForeignKey(baot => baot.BankAccountId);

            modelBuilder.Entity<Operation>().ToTable("Operations");
            modelBuilder.Entity<Operation>().HasKey(o => o.Id);
            modelBuilder.Entity<Operation>().Property(o => o.CreationDate).HasColumnName("CreationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.ModificationDate).HasColumnName("ModificationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.BankAccountOperationTypeId).HasColumnName("FK_BankAccountOperationsType").HasColumnType("int").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.BankAccountId).HasColumnName("FK_BankAccount").HasColumnType("int").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.ParentOperationId).HasColumnName("FK_ParentOperation").HasColumnType("int");
            modelBuilder.Entity<Operation>().HasOne<BankAccountOperationType>(o => o.BankAccountOperationType).WithMany(baot => baot.Operations).HasForeignKey(o => o.BankAccountOperationTypeId);
            modelBuilder.Entity<Operation>().HasOne<BankAccount>(o => o.BankAccount).WithMany(ba => ba.Operations).HasForeignKey(o => o.BankAccountId);
            modelBuilder.Entity<Operation>().HasOne<Operation>(o => o.ParentOperation).WithMany(o => o.ChildOperations).HasForeignKey(o => o.ParentOperationId);
            modelBuilder.Entity<Operation>().Property(o => o.OperationDate).HasColumnName("OperationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.Closed).HasColumnName("Closed").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<Operation>().Property(o => o.Amount).HasColumnName("Amount").HasColumnType("decimal(18, 2)").IsRequired();

            modelBuilder.Entity<ReccurentOperation>().ToTable("ReccurentsOperations");
            modelBuilder.Entity<ReccurentOperation>().HasKey(ro => ro.Id);
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.CreationDate).HasColumnName("CreationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.ModificationDate).HasColumnName("ModificationDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.BankAccountId).HasColumnName("FK_BankAccount").HasColumnType("int").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.BankAccountOperationTypeId).HasColumnName("FK_BankAccountOperationsType").HasColumnType("int").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().HasOne<BankAccount>(ro => ro.BankAccount).WithMany(ba => ba.ReccurentsOperations).HasForeignKey(ro => ro.BankAccountId);
            modelBuilder.Entity<ReccurentOperation>().HasOne<BankAccountOperationType>(o => o.BankAccountOperationType).WithMany(baot => baot.ReccurentsOperations).HasForeignKey(ro => ro.BankAccountOperationTypeId);
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.OperationDayOfMonth).HasColumnName("OperationDayOfMonth").HasColumnType("smallint").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.StartDate).HasColumnName("StartDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.EndDate).HasColumnName("EndDate").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.Amount).HasColumnName("Amount").HasColumnType("decimal(18, 2)").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.January).HasColumnName("January").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.February).HasColumnName("February").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.March).HasColumnName("March").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.April).HasColumnName("April").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.May).HasColumnName("May").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.June).HasColumnName("June").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.July).HasColumnName("July").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.August).HasColumnName("August").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.September).HasColumnName("September").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.October).HasColumnName("October").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.November).HasColumnName("November").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ReccurentOperation>().Property(ro => ro.December).HasColumnName("December").HasColumnType("bit").IsRequired();
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public async void CommitAsync()
        {
            await this.SaveChangesAsync();
        }
    }
}
