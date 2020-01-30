using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    public partial class CommunityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trust_level_communities",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    create_date_at = table.Column<DateTime>(nullable: false),
                    last_modified_at = table.Column<DateTime>(nullable: true),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    explanation = table.Column<string>(nullable: true),
                    community_id = table.Column<long>(nullable: false),
                    is_same_as_instance = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trust_level_communities", x => x.id);
                    table.ForeignKey(
                        name: "fk_trust_level_communities_communities_community_id",
                        column: x => x.community_id,
                        principalTable: "communities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_communities",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    create_date_at = table.Column<DateTime>(nullable: false),
                    last_modified_at = table.Column<DateTime>(nullable: true),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: true),
                    bio = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    is_from_stack_exchange = table.Column<bool>(nullable: false),
                    stack_exchange_id = table.Column<long>(nullable: true),
                    stack_exchange_validated_at = table.Column<DateTime>(nullable: true),
                    stack_exchange_last_imported_at = table.Column<DateTime>(nullable: true),
                    is_email_verified = table.Column<bool>(nullable: false),
                    is_suspended = table.Column<bool>(nullable: false),
                    suspension_end_at = table.Column<DateTime>(nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true),
                    upvotes_cast = table.Column<long>(nullable: false),
                    downvotes_cast = table.Column<long>(nullable: false),
                    profile_views = table.Column<long>(nullable: false),
                    reputation = table.Column<long>(nullable: false),
                    is_same_as_instance = table.Column<bool>(nullable: false),
                    trust_level_community_id = table.Column<long>(nullable: false),
                    community_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_communities", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_communities_communities_community_id",
                        column: x => x.community_id,
                        principalTable: "communities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_communities_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_communities_trust_level_communities_trust_level_commun~",
                        column: x => x.trust_level_community_id,
                        principalTable: "trust_level_communities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_community_id",
                table: "member_communities",
                column: "community_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_member_id",
                table: "member_communities",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_communities_trust_level_community_id",
                table: "member_communities",
                column: "trust_level_community_id");

            migrationBuilder.CreateIndex(
                name: "ix_trust_level_communities_community_id",
                table: "trust_level_communities",
                column: "community_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_communities");

            migrationBuilder.DropTable(
                name: "trust_level_communities");
        }
    }
}
