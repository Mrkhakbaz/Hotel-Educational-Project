using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Project.Migrations
{
    public partial class NewOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReserveDate_OrderDetails_DetailId",
                table: "OrderReserveDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReserveDate",
                table: "OrderReserveDate");

            migrationBuilder.RenameTable(
                name: "OrderReserveDate",
                newName: "OrderReserveDates");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReserveDate_DetailId",
                table: "OrderReserveDates",
                newName: "IX_OrderReserveDates_DetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReserveDates",
                table: "OrderReserveDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReserveDates_OrderDetails_DetailId",
                table: "OrderReserveDates",
                column: "DetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReserveDates_OrderDetails_DetailId",
                table: "OrderReserveDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReserveDates",
                table: "OrderReserveDates");

            migrationBuilder.RenameTable(
                name: "OrderReserveDates",
                newName: "OrderReserveDate");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReserveDates_DetailId",
                table: "OrderReserveDate",
                newName: "IX_OrderReserveDate_DetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReserveDate",
                table: "OrderReserveDate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReserveDate_OrderDetails_DetailId",
                table: "OrderReserveDate",
                column: "DetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
