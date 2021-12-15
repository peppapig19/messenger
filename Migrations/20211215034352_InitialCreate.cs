using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Messenger.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
/*             migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    un = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pw = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pk", x => x.un);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    un = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    msg = table.Column<string>(type: "text", nullable: false),
                    sent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "messages_fk",
                        column: x => x.un,
                        principalTable: "users",
                        principalColumn: "un",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_un",
                table: "messages",
                column: "un"); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
/*             migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users"); */
        }
    }
}