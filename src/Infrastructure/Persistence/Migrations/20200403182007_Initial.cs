using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Codidact.Core.Infrastructure.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "communities",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    create_date_at = table.Column<DateTime>(nullable: false),
                    last_modified_at = table.Column<DateTime>(nullable: true),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 40, nullable: false),
                    tagline = table.Column<string>(maxLength: 100, nullable: false),
                    url = table.Column<string>(maxLength: 255, nullable: false),
                    status = table.Column<int>(nullable: false, defaultValue: 1),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_communities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    create_date_at = table.Column<DateTime>(nullable: false),
                    last_modified_at = table.Column<DateTime>(nullable: true),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(maxLength: 100, nullable: false),
                    bio = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    is_from_stack_exchange = table.Column<bool>(nullable: false, defaultValue: false),
                    stack_exchange_id = table.Column<long>(nullable: true),
                    stack_exchange_validated_at = table.Column<DateTime>(nullable: true),
                    stack_exchange_last_imported_at = table.Column<DateTime>(nullable: true),
                    is_suspended = table.Column<bool>(nullable: false, defaultValue: false),
                    suspension_end_at = table.Column<DateTime>(nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true),
                    user_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trust_levels",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    create_date_at = table.Column<DateTime>(nullable: false),
                    last_modified_at = table.Column<DateTime>(nullable: true),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    explanation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trust_levels", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_communities_name",
                table: "communities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_communities_url",
                table: "communities",
                column: "url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_explanation",
                table: "trust_levels",
                column: "explanation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_name",
                table: "trust_levels",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "communities");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "trust_levels");
        }
    }
}
