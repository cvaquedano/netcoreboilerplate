using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Data.Migrations
{
    public partial class addingcolumntomaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterStatusEntityId",
                table: "ExampleMasterEntity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity",
                column: "MasterStatusEntityId",
                principalTable: "MasterStatusEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterStatusEntityId",
                table: "ExampleMasterEntity",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ExampleMasterEntity_MasterStatusEntity_MasterStatusEntityId",
                table: "ExampleMasterEntity",
                column: "MasterStatusEntityId",
                principalTable: "MasterStatusEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
