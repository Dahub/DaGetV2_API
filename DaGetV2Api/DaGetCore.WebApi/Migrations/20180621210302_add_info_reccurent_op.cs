using Microsoft.EntityFrameworkCore.Migrations;

namespace DaGetCore.WebApi.Migrations
{
    public partial class add_info_reccurent_op : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HadApplyCurrentOperationsThisMonth",
                schema: "dbo",
                table: "BankAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HadApplyCurrentOperationsThisMonth",
                schema: "dbo",
                table: "BankAccounts");
        }
    }
}
