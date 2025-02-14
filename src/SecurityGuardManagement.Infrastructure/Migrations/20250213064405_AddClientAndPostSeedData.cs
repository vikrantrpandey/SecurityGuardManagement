using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGuardManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientAndPostSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuardAssignments_Posts_PostId",
                table: "GuardAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId",
                table: "Guards");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Clients_ClientId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "RequiredGuardCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuardLevelId1",
                table: "Guards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Status", "UpdatedAt", "ContactInfo_Email", "ContactInfo_Phone" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Example Corporation", 1, null, "contact@example.com", "+9779841000000" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "ClientId", "CreatedAt", "Description", "Name", "RequiredGuardCount", "Status", "UpdatedAt", "PostAddress_City", "PostAddress_Country", "PostAddress_District", "PostAddress_GoogleLocation", "PostAddress_PostalCode", "PostAddress_Province", "PostAddress_State", "PostAddress_Street" },
                values: new object[] { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Main entrance security post", "Main Office Security", 2, 1, null, "Kathmandu", "Nepal", "Kathmandu", null, "44600", "Bagmati", "Bagmati", "123 Main Street" });

            migrationBuilder.CreateIndex(
                name: "IX_Guards_GuardLevelId1",
                table: "Guards",
                column: "GuardLevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GuardAssignments_Posts_PostId",
                table: "GuardAssignments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId",
                table: "Guards",
                column: "GuardLevelId",
                principalTable: "GuardLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId1",
                table: "Guards",
                column: "GuardLevelId1",
                principalTable: "GuardLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Clients_ClientId",
                table: "Posts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuardAssignments_Posts_PostId",
                table: "GuardAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId",
                table: "Guards");

            migrationBuilder.DropForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId1",
                table: "Guards");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Clients_ClientId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Guards_GuardLevelId1",
                table: "Guards");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "RequiredGuardCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "GuardLevelId1",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuardAssignments_Posts_PostId",
                table: "GuardAssignments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guards_GuardLevels_GuardLevelId",
                table: "Guards",
                column: "GuardLevelId",
                principalTable: "GuardLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Clients_ClientId",
                table: "Posts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
