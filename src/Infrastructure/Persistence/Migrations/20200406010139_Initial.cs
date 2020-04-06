using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Codidact.Core.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit.history_activity_type", "CREATE,UPDATE_BEFORE,UPDATE_AFTER,DELETE")
                .Annotation("Npgsql:PostgresExtension:adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    member_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    body = table.Column<string>(nullable: false),
                    upvotes = table.Column<short>(nullable: false),
                    downvotes = table.Column<short>(nullable: false),
                    net_votes = table.Column<long>(nullable: true, computedColumnSql: "(upvotes - downvotes)"),
                    score = table.Column<decimal>(type: "numeric", nullable: false),
                    views = table.Column<long>(nullable: false),
                    post_type_id = table.Column<long>(nullable: false),
                    is_accepted = table.Column<bool>(nullable: false),
                    is_closed = table.Column<bool>(nullable: false),
                    is_protected = table.Column<bool>(nullable: false),
                    parent_post_id = table.Column<long>(nullable: true),
                    category_id = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "post_parent_post_fk",
                        column: x => x.parent_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment_votes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    comment_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    vote_type_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_votes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    url_part = table.Column<string>(maxLength: 20, nullable: true),
                    is_primary = table.Column<bool>(nullable: false),
                    short_explanation = table.Column<string>(nullable: true),
                    long_explanation = table.Column<string>(nullable: true),
                    contributes_to_trust_level = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    calculations = table.Column<long>(nullable: true, defaultValueSql: "0"),
                    participation_minimum_trust_level_id = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_post_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    category_id = table.Column<long>(nullable: false),
                    post_type_id = table.Column<long>(nullable: false),
                    is_active = table.Column<bool>(nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_post_types", x => x.id);
                },
                comment: "CategoryPostType");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    member_id = table.Column<long>(nullable: false),
                    post_id = table.Column<long>(nullable: false),
                    parent_comment_id = table.Column<long>(nullable: true),
                    body = table.Column<string>(nullable: false),
                    upvotes = table.Column<long>(nullable: false),
                    downvotes = table.Column<long>(nullable: false),
                    net_votes = table.Column<long>(nullable: true, computedColumnSql: "(upvotes - downvotes)"),
                    score = table.Column<decimal>(type: "numeric", nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "comment_parent_comment_fk",
                        column: x => x.parent_comment_id,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "comment_post_fk",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for the comments on posts, both questions and answers.");

            migrationBuilder.CreateTable(
                name: "member_privileges",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    member_id = table.Column<long>(nullable: false),
                    privilege_id = table.Column<long>(nullable: false),
                    is_suspended = table.Column<bool>(nullable: false),
                    privilege_suspension_start_at = table.Column<DateTime>(nullable: true),
                    privilege_suspension_end_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_privileges", x => x.id);
                },
                comment: "For recording which members have which privilege in a community. If a member has a privilege suspended, then that is also recorded here, and a nightly task will auto undo the suspension once the privelege_suspension_end_date has passed.");

            migrationBuilder.CreateTable(
                name: "member_social_media_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    social_media_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_social_media_types", x => x.id);
                },
                comment: "The social media that the member would like to display in their community specific profile");

            migrationBuilder.CreateTable(
                name: "post_duplicate_posts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    original_post_id = table.Column<long>(nullable: false),
                    duplicate_post_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_duplicate_posts", x => x.id);
                    table.ForeignKey(
                        name: "post_duplicate_post_duplicate_post_fk",
                        column: x => x.duplicate_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "post_duplicate_post_original_post_fk",
                        column: x => x.original_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_status_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_status_types", x => x.id);
                },
                comment: "For setting the status of a post locked/featured etc");

            migrationBuilder.CreateTable(
                name: "post_statuss",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    post_id = table.Column<long>(nullable: false),
                    post_status_type_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_statuss", x => x.id);
                    table.ForeignKey(
                        name: "post_status_post_id_fk",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "post_status_post_status_type_fk",
                        column: x => x.post_status_type_id,
                        principalTable: "post_status_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    post_id = table.Column<long>(nullable: false),
                    tag_id = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tags", x => x.id);
                    table.ForeignKey(
                        name: "post_tag_post_fk",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_types", x => x.id);
                },
                comment: "Records the type of post, question/answer/blog etc");

            migrationBuilder.CreateTable(
                name: "post_votes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    post_id = table.Column<long>(nullable: false),
                    vote_type_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_votes", x => x.id);
                    table.ForeignKey(
                        name: "post_vote_post_fk",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "The reason for this table is so that votes by spammers/serial voters can be undone.");

            migrationBuilder.CreateTable(
                name: "privileges",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_privileges", x => x.id);
                },
                comment: "Table for privileges");

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    account_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_media_types", x => x.id);
                },
                comment: "The types of social media that the member can display in their profile");

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    body = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    tag_wiki = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    synonym_tag_id = table.Column<long>(nullable: true),
                    parent_tag_id = table.Column<long>(nullable: true),
                    usages = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                    table.ForeignKey(
                        name: "tag_parent_tag_fk",
                        column: x => x.parent_tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for all of the tags");

            migrationBuilder.CreateTable(
                name: "trust_levels",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    explanation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trust_levels", x => x.id);
                },
                comment: "Name for each trust level and an explanation of each that a user should get when they get to that level.");

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false),
                    bio = table.Column<string>(nullable: true),
                    profile_picture_link = table.Column<string>(nullable: true),
                    is_temporarily_suspended = table.Column<bool>(nullable: false),
                    temporary_suspension_end_at = table.Column<DateTime>(nullable: true),
                    temporary_suspension_reason = table.Column<string>(nullable: true),
                    trust_level_id = table.Column<long>(nullable: false),
                    network_account_id = table.Column<long>(nullable: true, comment: "link to 'network_account' table?"),
                    is_moderator = table.Column<bool>(nullable: false),
                    is_administrator = table.Column<bool>(nullable: false),
                    is_synced_with_network_account = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    user_id = table.Column<long>(nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.id);
                    table.ForeignKey(
                        name: "member_created_by_member_fk",
                        column: x => x.created_by_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "member_last_modified_by_member_fk",
                        column: x => x.last_modified_by_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "member_trust_level_fk",
                        column: x => x.trust_level_id,
                        principalTable: "trust_levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "This table will hold the global member records for a Codidact Instance. A member should only have one email to login with, that would be stored here. Does not include details such as password storage and hashing.");

            migrationBuilder.CreateTable(
                name: "vote_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    display_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vote_types", x => x.id);
                    table.ForeignKey(
                        name: "vote_type_created_by_member_fk",
                        column: x => x.created_by_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "vote_type_last_modified_by_member_fk",
                        column: x => x.last_modified_by_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for the vote types, upvote/downvote.");

            migrationBuilder.CreateTable(
                name: "category_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    url_part = table.Column<string>(maxLength: 20, nullable: true),
                    is_primary = table.Column<bool>(nullable: false),
                    short_explanation = table.Column<string>(nullable: true),
                    long_explanation = table.Column<string>(nullable: true),
                    contributes_to_trust_level = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    calculations = table.Column<long>(nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "category_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category_post_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    category_id = table.Column<long>(nullable: false),
                    post_type_id = table.Column<long>(nullable: false),
                    is_active = table.Column<bool>(nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_post_type_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "category_post_type_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "CategoryPostType");

            migrationBuilder.CreateTable(
                name: "comment_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    post_id = table.Column<long>(nullable: false),
                    parent_comment_id = table.Column<long>(nullable: true),
                    body = table.Column<string>(nullable: false),
                    upvotes = table.Column<long>(nullable: false),
                    downvotes = table.Column<long>(nullable: false),
                    net_votes = table.Column<long>(nullable: true, computedColumnSql: "(upvotes - downvotes)"),
                    score = table.Column<decimal>(type: "numeric", nullable: false),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_by_member_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comment_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "fk_comment_histories_members_deleted_by_member_id",
                        column: x => x.deleted_by_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "comment_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for the comments on posts, both questions and answers.");

            migrationBuilder.CreateTable(
                name: "comment_vote_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    comment_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    vote_type_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comment_vote_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "comment_vote_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    bio = table.Column<string>(nullable: true),
                    profile_picture_link = table.Column<string>(nullable: true),
                    is_temporarily_suspended = table.Column<bool>(nullable: false),
                    temporary_suspension_end_at = table.Column<DateTime>(nullable: true),
                    temporary_suspension_reason = table.Column<string>(nullable: true),
                    trust_level_id = table.Column<long>(nullable: false),
                    network_account_id = table.Column<long>(nullable: true, comment: "link to 'network_account' table?"),
                    is_moderator = table.Column<bool>(nullable: false),
                    is_administrator = table.Column<bool>(nullable: false),
                    is_synced_with_network_account = table.Column<bool>(nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("member_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "member_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "This table will hold the global member records for a Codidact Instance. A member should only have one email to login with, that would be stored here. Does not include details such as password storage and hashing.");

            migrationBuilder.CreateTable(
                name: "member_privilege_history",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    privilege_id = table.Column<long>(nullable: false),
                    is_suspended = table.Column<bool>(nullable: false),
                    privilege_suspension_start_at = table.Column<DateTime>(nullable: true),
                    privilege_suspension_end_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("member_privilege_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "member_privilege_histry_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "For recording which members have which privilege in a community. If a member has a privilege suspended, then that is also recorded here, and a nightly task will auto undo the suspension once the privelege_suspension_end_date has passed.");

            migrationBuilder.CreateTable(
                name: "member_social_media_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    social_media_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("member_social_media_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "member_social_media_type_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "The social media that the member would like to display in their community specific profile");

            migrationBuilder.CreateTable(
                name: "post_duplicate_post_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    original_post_id = table.Column<long>(nullable: false),
                    duplicate_post_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_duplicate_post_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_duplicate_post_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    body = table.Column<string>(nullable: false),
                    upvotes = table.Column<short>(nullable: false),
                    downvotes = table.Column<short>(nullable: false),
                    net_votes = table.Column<long>(nullable: true, computedColumnSql: "(upvotes - downvotes)"),
                    score = table.Column<decimal>(type: "numeric", nullable: false),
                    views = table.Column<long>(nullable: false),
                    post_type_id = table.Column<long>(nullable: false),
                    is_accepted = table.Column<bool>(nullable: false),
                    is_closed = table.Column<bool>(nullable: false),
                    is_protected = table.Column<bool>(nullable: false),
                    parent_post_id = table.Column<long>(nullable: true),
                    category_id = table.Column<long>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_status_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    post_id = table.Column<long>(nullable: false),
                    post_status_type_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_status_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_status_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_status_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_status_type_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_status_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "For setting the status of a post locked/featured etc");

            migrationBuilder.CreateTable(
                name: "post_tag_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    post_id = table.Column<long>(nullable: false),
                    tag_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_tag_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_tag_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_type_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_type_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Records the type of post, question/answer/blog etc");

            migrationBuilder.CreateTable(
                name: "post_vote_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    post_id = table.Column<long>(nullable: false),
                    vote_type_id = table.Column<long>(nullable: false),
                    member_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("post_vote_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "post_vote_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "The reason for this table is so that votes by spammers/serial voters can be undone.");

            migrationBuilder.CreateTable(
                name: "privilege_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("privilege_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "privilege_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for privileges");

            migrationBuilder.CreateTable(
                name: "setting_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("setting_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "setting_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "social_media_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    account_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("social_media_type_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "social_media_type_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "The types of social media that the member can display in their profile");

            migrationBuilder.CreateTable(
                name: "tag_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    history_activity_member_id1 = table.Column<long>(nullable: true),
                    id = table.Column<long>(nullable: false),
                    body = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    tag_wiki = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false, defaultValueSql: "true"),
                    synonym_tag_id = table.Column<long>(nullable: true),
                    parent_tag_id = table.Column<long>(nullable: true),
                    usages = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tag_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "tag_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tag_histories_members_history_activity_member_id1",
                        column: x => x.history_activity_member_id1,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for all of the tags - history");

            migrationBuilder.CreateTable(
                name: "trust_level_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false),
                    explanation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("trust_level_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "trust_level_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Name for each trust level and an explanation of each that a user should get when they get to that level.");

            migrationBuilder.CreateTable(
                name: "vote_type_histories",
                schema: "audit",
                columns: table => new
                {
                    history_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    created_by_member_id = table.Column<long>(nullable: false),
                    last_modified_by_member_id = table.Column<long>(nullable: true),
                    history_activity_at = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    history_activity_member_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    display_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vote_type_history_pk", x => x.history_id);
                    table.ForeignKey(
                        name: "vote_type_history_member_fk",
                        column: x => x.history_activity_member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table for the vote types, upvote/downvote.");

            migrationBuilder.CreateIndex(
                name: "ix_categories_created_by_member_id",
                table: "categories",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_last_modified_by_member_id",
                table: "categories",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_participation_minimum_trust_level_id",
                table: "categories",
                column: "participation_minimum_trust_level_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_post_types_created_by_member_id",
                table: "category_post_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_post_types_last_modified_by_member_id",
                table: "category_post_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "category_post_type_category_post_type_uc",
                table: "category_post_types",
                columns: new[] { "category_id", "post_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comment_votes_created_by_member_id",
                table: "comment_votes",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_votes_last_modified_by_member_id",
                table: "comment_votes",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_votes_member_id",
                table: "comment_votes",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_votes_vote_type_id",
                table: "comment_votes",
                column: "vote_type_id");

            migrationBuilder.CreateIndex(
                name: "comment_vote_comment_member_uc",
                table: "comment_votes",
                columns: new[] { "comment_id", "member_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comments_created_by_member_id",
                table: "comments",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_deleted_by_member_id",
                table: "comments",
                column: "deleted_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_last_modified_by_member_id",
                table: "comments",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_member_id",
                table: "comments",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_parent_comment_id",
                table: "comments",
                column: "parent_comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_post_id",
                table: "comments",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_privileges_created_by_member_id",
                table: "member_privileges",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_privileges_last_modified_by_member_id",
                table: "member_privileges",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_privileges_privilege_id",
                table: "member_privileges",
                column: "privilege_id");

            migrationBuilder.CreateIndex(
                name: "member_privilege_member_privilege_uc",
                table: "member_privileges",
                columns: new[] { "member_id", "privilege_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_member_social_media_types_created_by_member_id",
                table: "member_social_media_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_social_media_types_last_modified_by_member_id",
                table: "member_social_media_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_social_media_types_social_media_id",
                table: "member_social_media_types",
                column: "social_media_id");

            migrationBuilder.CreateIndex(
                name: "member_social_media_social_media_member_uc",
                table: "member_social_media_types",
                columns: new[] { "member_id", "social_media_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_members_created_by_member_id",
                table: "members",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_last_modified_by_member_id",
                table: "members",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_trust_level_id",
                table: "members",
                column: "trust_level_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_duplicate_posts_created_by_member_id",
                table: "post_duplicate_posts",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_duplicate_posts_duplicate_post_id",
                table: "post_duplicate_posts",
                column: "duplicate_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_duplicate_posts_last_modified_by_member_id",
                table: "post_duplicate_posts",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "post_duplicate_post_original_post_duplicate_post_uc",
                table: "post_duplicate_posts",
                columns: new[] { "original_post_id", "duplicate_post_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_status_types_created_by_member_id",
                table: "post_status_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_status_types_display_name",
                table: "post_status_types",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_status_types_last_modified_by_member_id",
                table: "post_status_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_statuss_created_by_member_id",
                table: "post_statuss",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_statuss_last_modified_by_member_id",
                table: "post_statuss",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_statuss_post_status_type_id",
                table: "post_statuss",
                column: "post_status_type_id");

            migrationBuilder.CreateIndex(
                name: "post_status_post_status_type_post_uc",
                table: "post_statuss",
                columns: new[] { "post_id", "post_status_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_tags_created_by_member_id",
                table: "post_tags",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tags_last_modified_by_member_id",
                table: "post_tags",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tags_tag_id",
                table: "post_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "post_tag_post_tag_uc",
                table: "post_tags",
                columns: new[] { "post_id", "tag_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_types_created_by_member_id",
                table: "post_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_types_display_name",
                table: "post_types",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_types_last_modified_by_member_id",
                table: "post_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_votes_created_by_member_id",
                table: "post_votes",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_votes_last_modified_by_member_id",
                table: "post_votes",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_votes_member_id",
                table: "post_votes",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_votes_vote_type_id",
                table: "post_votes",
                column: "vote_type_id");

            migrationBuilder.CreateIndex(
                name: "post_vote_post_member_uc",
                table: "post_votes",
                columns: new[] { "post_id", "member_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_posts_category_id",
                table: "posts",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_created_by_member_id",
                table: "posts",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_last_modified_by_member_id",
                table: "posts",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_member_id",
                table: "posts",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_parent_post_id",
                table: "posts",
                column: "parent_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_post_type_id",
                table: "posts",
                column: "post_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_privileges_created_by_member_id",
                table: "privileges",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "privilege_display_name_uc",
                table: "privileges",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_privileges_last_modified_by_member_id",
                table: "privileges",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_settings_created_by_member_id",
                table: "settings",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_settings_last_modified_by_member_id",
                table: "settings",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "social_media_type_account_url_uc",
                table: "social_media_types",
                column: "account_url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_social_media_types_created_by_member_id",
                table: "social_media_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "social_media_type_display_name_uc",
                table: "social_media_types",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_social_media_types_last_modified_by_member_id",
                table: "social_media_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "tag_body_uc",
                table: "tags",
                column: "body",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tags_created_by_member_id",
                table: "tags",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_last_modified_by_member_id",
                table: "tags",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_parent_tag_id",
                table: "tags",
                column: "parent_tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_created_by_member_id",
                table: "trust_levels",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "trust_level_display_name_uq",
                table: "trust_levels",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "trust_level_explanation_uq",
                table: "trust_levels",
                column: "explanation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trust_levels_last_modified_by_member_id",
                table: "trust_levels",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_vote_types_created_by_member_id",
                table: "vote_types",
                column: "created_by_member_id");

            migrationBuilder.CreateIndex(
                name: "vote_type_display_name_uc",
                table: "vote_types",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vote_types_last_modified_by_member_id",
                table: "vote_types",
                column: "last_modified_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_histories_history_activity_member_id",
                schema: "audit",
                table: "category_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_post_type_histories_history_activity_member_id",
                schema: "audit",
                table: "category_post_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_histories_deleted_by_member_id",
                schema: "audit",
                table: "comment_histories",
                column: "deleted_by_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_histories_history_activity_member_id",
                schema: "audit",
                table: "comment_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_vote_histories_history_activity_member_id",
                schema: "audit",
                table: "comment_vote_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_histories_history_activity_member_id",
                schema: "audit",
                table: "member_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_privilege_history_history_activity_member_id",
                schema: "audit",
                table: "member_privilege_history",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_social_media_type_histories_history_activity_member_~",
                schema: "audit",
                table: "member_social_media_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_duplicate_post_histories_history_activity_member_id",
                schema: "audit",
                table: "post_duplicate_post_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_histories_history_activity_member_id",
                schema: "audit",
                table: "post_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_status_histories_history_activity_member_id",
                schema: "audit",
                table: "post_status_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_status_type_histories_history_activity_member_id",
                schema: "audit",
                table: "post_status_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tag_histories_history_activity_member_id",
                schema: "audit",
                table: "post_tag_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_type_histories_history_activity_member_id",
                schema: "audit",
                table: "post_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_vote_histories_history_activity_member_id",
                schema: "audit",
                table: "post_vote_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_privilege_histories_history_activity_member_id",
                schema: "audit",
                table: "privilege_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_setting_histories_history_activity_member_id",
                schema: "audit",
                table: "setting_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_media_type_histories_history_activity_member_id",
                schema: "audit",
                table: "social_media_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_histories_history_activity_member_id",
                schema: "audit",
                table: "tag_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_histories_history_activity_member_id1",
                schema: "audit",
                table: "tag_histories",
                column: "history_activity_member_id1");

            migrationBuilder.CreateIndex(
                name: "ix_trust_level_histories_history_activity_member_id",
                schema: "audit",
                table: "trust_level_histories",
                column: "history_activity_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_vote_type_histories_history_activity_member_id",
                schema: "audit",
                table: "vote_type_histories",
                column: "history_activity_member_id");

            migrationBuilder.AddForeignKey(
                name: "post_created_by_member_fk",
                table: "posts",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_last_modified_by_member_fk",
                table: "posts",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_member_fk",
                table: "posts",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_category_fk",
                table: "posts",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_post_type_fk",
                table: "posts",
                column: "post_type_id",
                principalTable: "post_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_vote_created_by_member_fk",
                table: "comment_votes",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_vote_last_modified_by_member_fk",
                table: "comment_votes",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_vote_member_fk",
                table: "comment_votes",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "commentvote_comment_fk",
                table: "comment_votes",
                column: "comment_id",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_vote_vote_type_fk",
                table: "comment_votes",
                column: "vote_type_id",
                principalTable: "vote_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_created_by_member_fk",
                table: "categories",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_last_modified_by_member_fk",
                table: "categories",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_participation_minimum_trust_level_fk",
                table: "categories",
                column: "participation_minimum_trust_level_id",
                principalTable: "trust_levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_post_type_created_by_member_fk",
                table: "category_post_types",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "category_post_type_last_modified_by_member_fk",
                table: "category_post_types",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_created_by_member_fk",
                table: "comments",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_comments_members_deleted_by_member_id",
                table: "comments",
                column: "deleted_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_last_modified_by_member_fk",
                table: "comments",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "comment_member_fk",
                table: "comments",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_privilege_created_by_member_fk",
                table: "member_privileges",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_privilege_last_modified_by_member_fk",
                table: "member_privileges",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_privilege_member_fk",
                table: "member_privileges",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_privilege_privlege_fk",
                table: "member_privileges",
                column: "privilege_id",
                principalTable: "privileges",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_social_media_created_by_member_fk",
                table: "member_social_media_types",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_social_media_last_modified_by_member_fk",
                table: "member_social_media_types",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_social_media_member_fk",
                table: "member_social_media_types",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "member_social_media_social_media_fk",
                table: "member_social_media_types",
                column: "social_media_id",
                principalTable: "social_media_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_duplicate_post_created_by_member_fk",
                table: "post_duplicate_posts",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_duplicate_post_last_modified_by_member_fk",
                table: "post_duplicate_posts",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_status_created_by_member_fk",
                table: "post_status_types",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_status_last_modified_by_member_fk",
                table: "post_status_types",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_status_created_by_member_fk",
                table: "post_statuss",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_status_last_modified_by_member_fk",
                table: "post_statuss",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_tag_created_by_member_fk",
                table: "post_tags",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_tag_last_modified_by_member_fk",
                table: "post_tags",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_tag_tag_fk",
                table: "post_tags",
                column: "tag_id",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_type_created_by_member_fk",
                table: "post_types",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_type_last_modified_by_member_fk",
                table: "post_types",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_vote_created_by_member_fk",
                table: "post_votes",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_vote_last_modified_by_member_fk",
                table: "post_votes",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_vote_member_fk",
                table: "post_votes",
                column: "member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "post_vote_vote_type_fk",
                table: "post_votes",
                column: "vote_type_id",
                principalTable: "vote_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "privilege_created_by_member_fk",
                table: "privileges",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "privilege_last_modified_by_member",
                table: "privileges",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "setting_created_by_member_fk",
                table: "settings",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "setting_last_modified_by_member_fk",
                table: "settings",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "social_media_created_by_member_fk",
                table: "social_media_types",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "social_media_last_modified_by_member_fk",
                table: "social_media_types",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "tag_created_by_member_fk",
                table: "tags",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "tag_last_modified_by_member_fk",
                table: "tags",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "created_by_member_fk",
                table: "trust_levels",
                column: "created_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "last_modified_by_member_fk",
                table: "trust_levels",
                column: "last_modified_by_member_id",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "created_by_member_fk",
                table: "trust_levels");

            migrationBuilder.DropForeignKey(
                name: "last_modified_by_member_fk",
                table: "trust_levels");

            migrationBuilder.DropTable(
                name: "category_post_types");

            migrationBuilder.DropTable(
                name: "comment_votes");

            migrationBuilder.DropTable(
                name: "member_privileges");

            migrationBuilder.DropTable(
                name: "member_social_media_types");

            migrationBuilder.DropTable(
                name: "post_duplicate_posts");

            migrationBuilder.DropTable(
                name: "post_statuss");

            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "post_votes");

            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropTable(
                name: "category_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "category_post_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "comment_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "comment_vote_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "member_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "member_privilege_history",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "member_social_media_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_duplicate_post_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_status_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_status_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_tag_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "post_vote_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "privilege_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "setting_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "social_media_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "tag_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "trust_level_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "vote_type_histories",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "privileges");

            migrationBuilder.DropTable(
                name: "social_media_types");

            migrationBuilder.DropTable(
                name: "post_status_types");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "vote_types");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "post_types");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "trust_levels");
        }
    }
}
