use master
go

if exists(select * from sys.databases where name='DaGet')
	drop database [DaGet]
go

create database [DaGet]
go

use [DaGet]
go

create table BankAccountAccess
(
	Id integer not null primary key identity(1,1),
	Wording nvarchar(128) not null
)
go

create table BankAccountTypes
(
	Id integer not null primary key identity(1,1),
	Wording nvarchar(128) not null
)
go

create table BankAccounts
(
	Id integer not null primary key identity(1,1),
	CreationDate datetime not null,
	ModificationDate datetime not null,
	FK_BankAccountType integer not null foreign key references BankAccountTypes(Id),
	Wording nvarchar(512) not null,
	Number nvarchar(256) null,
	SoldeInitial decimal(18,2) not null,
	Solde decimal(18,2) not null,
	DateSolde datetime not null
)
go

create table UsersBankAccounts
(
	Id integer not null primary key identity(1,1),
	UserName nvarchar(32) not null,
	FK_BankAccount integer not null foreign key references BankAccounts(Id),
	FK_BankAccountAccess integer not null foreign key references BankAccountAccess(Id),
	constraint UQ_UserNameBankAccount unique nonclustered
    (
        UserName, FK_BankAccount
    )
)
go

create table DefaultOperationsTypes
(
	Id integer not null primary key identity(1,1),
	Wording nvarchar(256) not null
)

create table BankAccountOperationsTypes
(
	Id integer not null primary key identity(1,1),
	Wording nvarchar(256) not null,
	FK_BankAccount integer not null foreign key references BankAccounts(Id)
)

create table Operations
(
	Id integer not null primary key identity(1,1),
	CreationDate datetime not null,
	ModificationDate datetime not null,
	FK_BankAccountOperationsType integer not null foreign key references BankAccountOperationsTypes(Id),
	FK_ParentOperation integer null foreign key references Operations(Id),
	FK_BankAccount integer not null foreign key references BankAccounts(Id),
	OperationDate datetime not null,
	Closed bit not null,
	Amount decimal(18,2) not null
)
go

create table ReccurentsOperations
(
	Id integer not null primary key identity(1,1),
	CreationDate datetime not null,
	ModificationDate datetime not null,
	FK_BankAccount integer not null foreign key references BankAccounts(Id),
	FK_BankAccountOperationsType integer not null foreign key references BankAccountOperationsTypes(Id),
	OperationDayOfMonth smallint not null,
	StartDate datetime not null,
	EndDate datetime not null,
	Amount decimal(18,2) not null,
	January bit not null,
	February bit not null,
	March bit not null,
	April bit not null,
	May bit not null,
	June bit not null,
	July bit not null,
	August bit not null,
	September bit not null,
	October bit not null,
	November bit not null,
	December bit not null
)
go

insert into BankAccountTypes(Wording) values ('Compte courant')
insert into BankAccountTypes(Wording) values ('Compte épargne')
go

insert into BankAccountAccess(Wording) values ('Propriétaire')
insert into BankAccountAccess(Wording) values ('Propriétaire')
insert into BankAccountAccess(Wording) values ('Lecteur')
go

insert into DefaultOperationsTypes(Wording) values ('Alimentation')
insert into DefaultOperationsTypes(Wording) values ('Santé')
insert into DefaultOperationsTypes(Wording) values ('Gaz')
insert into DefaultOperationsTypes(Wording) values ('Electricité')
insert into DefaultOperationsTypes(Wording) values ('Loyer')
insert into DefaultOperationsTypes(Wording) values ('Loisir')
insert into DefaultOperationsTypes(Wording) values ('Cadeau')
insert into DefaultOperationsTypes(Wording) values ('Don')
insert into DefaultOperationsTypes(Wording) values ('Frais banquaires')
insert into DefaultOperationsTypes(Wording) values ('Scolarité')
insert into DefaultOperationsTypes(Wording) values ('Habillement')
insert into DefaultOperationsTypes(Wording) values ('Assurance')
insert into DefaultOperationsTypes(Wording) values ('Impôts')
insert into DefaultOperationsTypes(Wording) values ('Salaire')
insert into DefaultOperationsTypes(Wording) values ('Epargne')
insert into DefaultOperationsTypes(Wording) values ('Téléphonie')
insert into DefaultOperationsTypes(Wording) values ('Retrait')
insert into DefaultOperationsTypes(Wording) values ('Virement commun')
insert into DefaultOperationsTypes(Wording) values ('Allocations')
insert into DefaultOperationsTypes(Wording) values ('Repas midi')
insert into DefaultOperationsTypes(Wording) values ('Transports')
insert into DefaultOperationsTypes(Wording) values ('Crêche')
insert into DefaultOperationsTypes(Wording) values ('Frais pro')
insert into DefaultOperationsTypes(Wording) values ('Quotidien-administratif')
insert into DefaultOperationsTypes(Wording) values ('Equipement maison')
insert into DefaultOperationsTypes(Wording) values ('Intérêts')
insert into DefaultOperationsTypes(Wording) values ('Enfant')
go