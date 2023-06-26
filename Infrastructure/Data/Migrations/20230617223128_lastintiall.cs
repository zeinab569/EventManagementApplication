using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.migrations
{
    public partial class lastintiall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPurchases_User_UserID",
                table: "TicketPurchases");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TicketPurchases",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPurchases_User_UserID",
                table: "TicketPurchases",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPurchases_User_UserID",
                table: "TicketPurchases");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TicketPurchases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPurchases_User_UserID",
                table: "TicketPurchases",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
