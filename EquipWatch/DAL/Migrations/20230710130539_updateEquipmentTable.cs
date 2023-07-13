using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateEquipmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Commissions_CommissionId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_CommissionId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CommissionId",
                table: "Equipment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommissionId",
                table: "Equipment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CommissionId",
                table: "Equipment",
                column: "CommissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Commissions_CommissionId",
                table: "Equipment",
                column: "CommissionId",
                principalTable: "Commissions",
                principalColumn: "Id");
        }
    }
}
