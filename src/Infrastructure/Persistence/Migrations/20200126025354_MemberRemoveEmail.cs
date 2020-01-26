using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    public partial class MemberRemoveEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_members_email",
                table: "members");

            migrationBuilder.DropColumn(
                name: "email",
                table: "members");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "members");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "members",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "members",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ix_members_email",
                table: "members",
                column: "email",
                unique: true);
        }
    }
}
