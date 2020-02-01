using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    public partial class RemoveCommunityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_communities_communities_community_id",
                table: "member_communities");

            migrationBuilder.DropForeignKey(
                name: "fk_trust_level_communities_communities_community_id",
                table: "trust_level_communities");

            migrationBuilder.AlterColumn<long>(
                name: "community_id",
                table: "trust_level_communities",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "community_id",
                table: "member_communities",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "fk_member_communities_communities_community_id",
                table: "member_communities",
                column: "community_id",
                principalTable: "communities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_trust_level_communities_communities_community_id",
                table: "trust_level_communities",
                column: "community_id",
                principalTable: "communities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_communities_communities_community_id",
                table: "member_communities");

            migrationBuilder.DropForeignKey(
                name: "fk_trust_level_communities_communities_community_id",
                table: "trust_level_communities");

            migrationBuilder.AlterColumn<long>(
                name: "community_id",
                table: "trust_level_communities",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "community_id",
                table: "member_communities",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_member_communities_communities_community_id",
                table: "member_communities",
                column: "community_id",
                principalTable: "communities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_trust_level_communities_communities_community_id",
                table: "trust_level_communities",
                column: "community_id",
                principalTable: "communities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
