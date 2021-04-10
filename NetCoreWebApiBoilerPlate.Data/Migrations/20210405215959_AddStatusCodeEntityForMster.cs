using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Data.Migrations
{
    public partial class AddStatusCodeEntityForMster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MasterStatusEntityId",
                table: "ExampleMasterEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterStatusEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterStatusEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMasterEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity",
                column: "MasterStatusEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity",
                column: "MasterStatusEntityId",
                principalTable: "MasterStatusEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity");

            migrationBuilder.DropTable(
                name: "MasterStatusEntity");

            migrationBuilder.DropIndex(
                name: "IX_ExampleMasterEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity");

            migrationBuilder.DropColumn(
                name: "MasterStatusEntityId",
                table: "ExampleMasterEntity");
        }
    }
}
