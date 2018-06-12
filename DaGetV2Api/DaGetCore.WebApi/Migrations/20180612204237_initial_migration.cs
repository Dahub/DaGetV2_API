using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaGetCore.WebApi.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "BankAccountAccess",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Wording = table.Column<string>(type: "nvarchar", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountAccess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Wording = table.Column<string>(type: "nvarchar", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultOperationsTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Wording = table.Column<string>(type: "nvarchar", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultOperationsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FK_BankAccountType = table.Column<int>(type: "int", nullable: false),
                    Wording = table.Column<string>(type: "nvarchar", maxLength: 512, nullable: false),
                    Number = table.Column<string>(type: "nvarchar", maxLength: 256, nullable: false),
                    SoldeInitial = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Solde = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateSolde = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_BankAccountTypes_FK_BankAccountType",
                        column: x => x.FK_BankAccountType,
                        principalSchema: "dbo",
                        principalTable: "BankAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountOperationsTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Wording = table.Column<string>(type: "nvarchar", maxLength: 256, nullable: false),
                    FK_BankAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountOperationsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountOperationsTypes_BankAccounts_FK_BankAccount",
                        column: x => x.FK_BankAccount,
                        principalSchema: "dbo",
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersBankAccounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar", maxLength: 32, nullable: false),
                    FK_BankAccount = table.Column<int>(type: "int", nullable: false),
                    FK_BankAccountAccess = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersBankAccounts_BankAccountAccess_FK_BankAccountAccess",
                        column: x => x.FK_BankAccountAccess,
                        principalSchema: "dbo",
                        principalTable: "BankAccountAccess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersBankAccounts_BankAccounts_FK_BankAccount",
                        column: x => x.FK_BankAccount,
                        principalSchema: "dbo",
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FK_BankAccountOperationsType = table.Column<int>(type: "int", nullable: false),
                    FK_ParentOperation = table.Column<int>(type: "int", nullable: true),
                    FK_BankAccount = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_BankAccounts_FK_BankAccount",
                        column: x => x.FK_BankAccount,
                        principalSchema: "dbo",
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_BankAccountOperationsTypes_FK_BankAccountOperationsType",
                        column: x => x.FK_BankAccountOperationsType,
                        principalSchema: "dbo",
                        principalTable: "BankAccountOperationsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Operations_FK_ParentOperation",
                        column: x => x.FK_ParentOperation,
                        principalSchema: "dbo",
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReccurentsOperations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FK_BankAccount = table.Column<int>(type: "int", nullable: false),
                    FK_BankAccountOperationsType = table.Column<int>(type: "int", nullable: false),
                    OperationDayOfMonth = table.Column<short>(type: "smallint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    January = table.Column<bool>(type: "bit", nullable: false),
                    February = table.Column<bool>(type: "bit", nullable: false),
                    March = table.Column<bool>(type: "bit", nullable: false),
                    April = table.Column<bool>(type: "bit", nullable: false),
                    May = table.Column<bool>(type: "bit", nullable: false),
                    June = table.Column<bool>(type: "bit", nullable: false),
                    July = table.Column<bool>(type: "bit", nullable: false),
                    August = table.Column<bool>(type: "bit", nullable: false),
                    September = table.Column<bool>(type: "bit", nullable: false),
                    October = table.Column<bool>(type: "bit", nullable: false),
                    November = table.Column<bool>(type: "bit", nullable: false),
                    December = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReccurentsOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReccurentsOperations_BankAccounts_FK_BankAccount",
                        column: x => x.FK_BankAccount,
                        principalSchema: "dbo",
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReccurentsOperations_BankAccountOperationsTypes_FK_BankAccountOperationsType",
                        column: x => x.FK_BankAccountOperationsType,
                        principalSchema: "dbo",
                        principalTable: "BankAccountOperationsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountOperationsTypes_FK_BankAccount",
                schema: "dbo",
                table: "BankAccountOperationsTypes",
                column: "FK_BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_FK_BankAccountType",
                schema: "dbo",
                table: "BankAccounts",
                column: "FK_BankAccountType");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FK_BankAccount",
                schema: "dbo",
                table: "Operations",
                column: "FK_BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FK_BankAccountOperationsType",
                schema: "dbo",
                table: "Operations",
                column: "FK_BankAccountOperationsType");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FK_ParentOperation",
                schema: "dbo",
                table: "Operations",
                column: "FK_ParentOperation");

            migrationBuilder.CreateIndex(
                name: "IX_ReccurentsOperations_FK_BankAccount",
                schema: "dbo",
                table: "ReccurentsOperations",
                column: "FK_BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_ReccurentsOperations_FK_BankAccountOperationsType",
                schema: "dbo",
                table: "ReccurentsOperations",
                column: "FK_BankAccountOperationsType");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBankAccounts_FK_BankAccountAccess",
                schema: "dbo",
                table: "UsersBankAccounts",
                column: "FK_BankAccountAccess");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBankAccounts_FK_BankAccount",
                schema: "dbo",
                table: "UsersBankAccounts",
                column: "FK_BankAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultOperationsTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Operations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ReccurentsOperations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsersBankAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BankAccountOperationsTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BankAccountAccess",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BankAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BankAccountTypes",
                schema: "dbo");
        }
    }
}
