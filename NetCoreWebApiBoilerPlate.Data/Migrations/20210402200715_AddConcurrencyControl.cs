using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Data.Migrations
{
    public partial class AddConcurrencyControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "User",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ExampleMasterEntity",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ExampleMasterEntity");
        }
    }
}
