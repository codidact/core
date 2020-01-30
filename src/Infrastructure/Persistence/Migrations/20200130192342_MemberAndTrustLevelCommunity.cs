using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    public partial class MemberAndTrustLevelCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_member_communities_member_id",
                table: "member_communities");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "trust_level_communities",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_same_as_instance",
                table: "trust_level_communities",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "is_suspended",
                table: "member_communities",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "is_same_as_instance",
                table: "member_communities",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "is_from_stack_exchange",
                table: "member_communities",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "member_communities",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "member_communities",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_level_communities_explanation",
                table: "trust_level_communities",
                column: "explanation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_level_communities_name",
                table: "trust_level_communities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_email",
                table: "member_communities",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_member_id_community_id",
                table: "member_communities",
                columns: new[] { "member_id", "community_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_trust_level_communities_explanation",
                table: "trust_level_communities");

            migrationBuilder.DropIndex(
                name: "ix_trust_level_communities_name",
                table: "trust_level_communities");

            migrationBuilder.DropIndex(
                name: "ix_member_communities_email",
                table: "member_communities");

            migrationBuilder.DropIndex(
                name: "ix_member_communities_member_id_community_id",
                table: "member_communities");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "trust_level_communities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "is_same_as_instance",
                table: "trust_level_communities",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_suspended",
                table: "member_communities",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_same_as_instance",
                table: "member_communities",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_from_stack_exchange",
                table: "member_communities",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "member_communities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "member_communities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_member_id",
                table: "member_communities",
                column: "member_id");
        }
    }
}
