using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebApiBoilerPlate.Migrations
{
    public partial class AddDetailEntityForMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterDetailEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    ExampleMasterEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDetailEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterDetailEntity_ExampleMasterEntity_ExampleMasterEntityId",
                        column: x => x.ExampleMasterEntityId,
                        principalTable: "ExampleMasterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailEntity_ExampleMasterEntityId",
                table: "MasterDetailEntity",
                column: "ExampleMasterEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterDetailEntity");
        }
    }
}
