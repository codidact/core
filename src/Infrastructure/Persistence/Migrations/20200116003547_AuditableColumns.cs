using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    public partial class AuditableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_date_at",
                table: "trust_levels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "created_by_member_id",
                table: "trust_levels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "last_modified_by_member_id",
                table: "trust_levels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "trust_levels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date_at",
                table: "members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "created_by_member_id",
                table: "members",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "deleted_by_member_id",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_date_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "last_modified_by_member_id",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date_at",
                table: "communities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "created_by_member_id",
                table: "communities",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "deleted_by_member_id",
                table: "communities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_date_at",
                table: "communities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "communities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "last_modified_by_member_id",
                table: "communities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "communities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_date_at",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "created_by_member_id",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "last_modified_by_member_id",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "create_date_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "created_by_member_id",
                table: "members");

            migrationBuilder.DropColumn(
                name: "deleted_by_member_id",
                table: "members");

            migrationBuilder.DropColumn(
                name: "deleted_date_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "members");

            migrationBuilder.DropColumn(
                name: "last_modified_by_member_id",
                table: "members");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "members");

            migrationBuilder.DropColumn(
                name: "create_date_at",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "created_by_member_id",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "deleted_by_member_id",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "deleted_date_at",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "last_modified_by_member_id",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "communities");
        }
    }
}
