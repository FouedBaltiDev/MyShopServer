using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "Id", "Address", "DeliveryDate", "OrderId", "Status" },
                values: new object[,]
                {
                    { 1, "85 cité ferdaouis", new DateTime(2024, 7, 2, 16, 22, 40, 158, DateTimeKind.Local).AddTicks(6618), 1, "Shipped" },
                    { 2, "13 cité santé agba", new DateTime(2024, 7, 1, 16, 22, 40, 158, DateTimeKind.Local).AddTicks(6628), 2, "Delivered" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 30, 16, 22, 40, 158, DateTimeKind.Local).AddTicks(6440), "Pending", 21.98m, 1 },
                    { 2, new DateTime(2024, 6, 30, 16, 22, 40, 158, DateTimeKind.Local).AddTicks(6455), "Completed", 20.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Description1", "T-Shirt", 10.99m, 100 },
                    { 2, "Description2", "Bucket Hat", 20.99m, 50 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Password", "PhoneNumber", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "85 cité ferdaouis", "fbalti@gmail.com", "123456", "29201085", "Admin", "BaltiFoued" },
                    { 2, "13 cité santé agba", "wbalti@gmail.com", "123456", "51377057", "Customer", "BaltiWiem" }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CartId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { 1, null, 1, 1, 2, 10.99m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CartId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { 2, null, 2, 2, 1, 20.99m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
