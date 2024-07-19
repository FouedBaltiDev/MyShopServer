using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class DeliveryUserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries");


            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Deliveries");
        }
    }
}
