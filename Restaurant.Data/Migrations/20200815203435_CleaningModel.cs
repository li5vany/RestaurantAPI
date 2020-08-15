using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class CleaningModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuCard_MenuCardId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Menu");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "MenuCard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModification",
                table: "MenuCard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Menu",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModification",
                table: "Menu",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuCard_MenuCardId",
                table: "Menu",
                column: "MenuCardId",
                principalTable: "MenuCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuCard_MenuCardId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "MenuCard");

            migrationBuilder.DropColumn(
                name: "LastModification",
                table: "MenuCard");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "LastModification",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "Menu",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "Menu",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuCard_MenuCardId",
                table: "Menu",
                column: "MenuCardId",
                principalTable: "MenuCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
