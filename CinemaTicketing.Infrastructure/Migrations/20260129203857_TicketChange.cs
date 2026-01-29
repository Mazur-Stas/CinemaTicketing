using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
