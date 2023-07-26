using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserIdInsteadOfEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Employees_EmployeeId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Employees_EmployeeId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorksOn_Employees_EmployeeId",
                table: "WorksOn");

            migrationBuilder.DropIndex(
                name: "IX_WorksOn_EmployeeId",
                table: "WorksOn");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_EmployeeId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_EmployeeId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WorksOn");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CheckIns");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorksOn",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CheckOuts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CheckIns",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorksOn");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CheckIns");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "WorksOn",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "CheckOuts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "CheckIns",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_WorksOn_EmployeeId",
                table: "WorksOn",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_EmployeeId",
                table: "CheckOuts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_EmployeeId",
                table: "CheckIns",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Employees_EmployeeId",
                table: "CheckIns",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Employees_EmployeeId",
                table: "CheckOuts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorksOn_Employees_EmployeeId",
                table: "WorksOn",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
