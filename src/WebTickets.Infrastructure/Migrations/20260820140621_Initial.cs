using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTickets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Permissions = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_tag_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_user_author_id",
                        column: x => x.author_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    storage_key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    content_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    size_bytes = table.Column<long>(type: "bigint", nullable: false),
                    uploaded_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: true),
                    message_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_message_message_id",
                        column: x => x.message_id,
                        principalTable: "Message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_file_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_file_message_id",
                table: "File",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_ticket_id",
                table: "File",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_author_id",
                table: "Message",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_ticket_id_sent_at",
                table: "Message",
                columns: new[] { "ticket_id", "sent_at" });

            migrationBuilder.CreateIndex(
                name: "ix_role_role_name",
                table: "Role",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tag_tag_name",
                table: "Tag",
                column: "tag_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_tag_tag_id",
                table: "TicketTag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_tag_ticket_id",
                table: "TicketTag",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_role_id",
                table: "User",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "TicketTag");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
