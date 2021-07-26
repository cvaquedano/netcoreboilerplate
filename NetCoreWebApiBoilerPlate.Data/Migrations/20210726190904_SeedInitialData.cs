using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MasterStatusEntity",
                columns: new[] { "Id", "Description", "Value" },
                values: new object[] { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "An active entity", "Active" });

            migrationBuilder.InsertData(
                table: "MasterStatusEntity",
                columns: new[] { "Id", "Description", "Value" },
                values: new object[] { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "An Inactive entity", "Inactive" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Status", "Username" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "chvb2002@gmail.com", "Carlos", "Vaquedano", "5f9537838499389e3819df87bf03fa035dc82c7b47123f84b89dd324812ea548", 1, "cvaquedano" });

            migrationBuilder.InsertData(
                table: "ExampleMasterEntity",
                columns: new[] { "Id", "DOB", "FirstName", "Gender", "LastName", "MasterStatusEntityId" },
                values: new object[] { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), new DateTime(1988, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", true, "Vaquedano", new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96") });

            migrationBuilder.InsertData(
                table: "MasterDetailEntity",
                columns: new[] { "Id", "ExampleMasterEntityId", "Price", "Quantity", "Total", "Value" },
                values: new object[] { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), 1f, 10, 100f, "Detail Value" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterDetailEntity",
                keyColumn: "Id",
                keyValue: new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"));

            migrationBuilder.DeleteData(
                table: "MasterStatusEntity",
                keyColumn: "Id",
                keyValue: new Guid("2902b665-1190-4c70-9915-b9c2d7680450"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"));

            migrationBuilder.DeleteData(
                table: "ExampleMasterEntity",
                keyColumn: "Id",
                keyValue: new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"));

            migrationBuilder.DeleteData(
                table: "MasterStatusEntity",
                keyColumn: "Id",
                keyValue: new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"));
        }
    }
}
