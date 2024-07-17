using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class ForeignKeys : Migration
    {
        // L'instruction ALTER TABLE est en conflit avec la contrainte FOREIGN KEY "FK_OrderItems_Products_ProductId". Le conflit s'est produit dans la base de données "MyShopDb", table "dbo.Products", column 'Id'

        // C normal qu'on a ce pb pask dans la table order items on a des lignes orphelins qui ne sont pas lié à des lignes product or que
        // C obligatoire qu'on a tjr une liaison du coup pour sql => c une vioaltion de foreign key

        // Solution : supprimer tous les lignes de product et de order items

        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems");
        }
    }
}
