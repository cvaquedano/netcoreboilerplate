using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Migrations
{
    public partial class RemoveConcurrencyControlOnDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ExampleMasterEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
