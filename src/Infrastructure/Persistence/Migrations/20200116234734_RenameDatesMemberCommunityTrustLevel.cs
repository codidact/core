using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Core.Infrastructure.Persistence.Migrations
{
    public partial class RenameDatesMemberCommunityTrustLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_trust_levels_content",
                table: "trust_levels");

            migrationBuilder.DropIndex(
                name: "ix_communities_tagline",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "content",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "deleted_date_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "members");

            migrationBuilder.DropColumn(
                name: "stack_exchange_last_imported",
                table: "members");

            migrationBuilder.DropColumn(
                name: "stack_exchange_validated",
                table: "members");

            migrationBuilder.DropColumn(
                name: "suspension_end_date",
                table: "members");

            migrationBuilder.DropColumn(
                name: "deleted_date_at",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "communities");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_at",
                table: "trust_levels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "trust_levels",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "members",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "stack_exchange_last_imported_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "stack_exchange_validated_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "suspension_end_at",
                table: "members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "communities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_at",
                table: "communities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_name",
                table: "trust_levels",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_trust_levels_name",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "last_modified_at",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "name",
                table: "trust_levels");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "last_modified_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "stack_exchange_last_imported_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "stack_exchange_validated_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "suspension_end_at",
                table: "members");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "communities");

            migrationBuilder.DropColumn(
                name: "last_modified_at",
                table: "communities");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "trust_levels",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "trust_levels",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "members",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 320);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_date_at",
                table: "members",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "members",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "stack_exchange_last_imported",
                table: "members",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "stack_exchange_validated",
                table: "members",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "suspension_end_date",
                table: "members",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_date_at",
                table: "communities",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "communities",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_content",
                table: "trust_levels",
                column: "content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_communities_tagline",
                table: "communities",
                column: "tagline",
                unique: true);
        }
    }
}
