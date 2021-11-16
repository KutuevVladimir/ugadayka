using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiExample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(4000)", nullable: true),
                    PlayerId = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.PlaylistId);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistsToTracks",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistsToTracks", x => new { x.TrackId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistsToTracks_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistsToTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistsToLikes",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PlayerId1 = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistsToLikes", x => new { x.PlayerId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistsToLikes_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(4000)", nullable: true),
                    State = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    RequiresPassword = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PlayerId = table.Column<string>(type: "varchar(767)", nullable: true),
                    PlaylistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<string>(type: "varchar(767)", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_RoomId",
                table: "Players",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_PlayerId",
                table: "Playlists",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsToLikes_PlayerId1",
                table: "PlaylistsToLikes",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsToLikes_PlaylistId",
                table: "PlaylistsToLikes",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsToTracks_PlaylistId",
                table: "PlaylistsToTracks",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PlayerId",
                table: "Rooms",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PlaylistId",
                table: "Rooms",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Players_PlayerId",
                table: "Playlists",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistsToLikes_Players_PlayerId1",
                table: "PlaylistsToLikes",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Players_PlayerId",
                table: "Rooms",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Rooms_RoomId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "PlaylistsToLikes");

            migrationBuilder.DropTable(
                name: "PlaylistsToTracks");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
