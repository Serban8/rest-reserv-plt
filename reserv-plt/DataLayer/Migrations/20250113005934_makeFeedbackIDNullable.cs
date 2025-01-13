using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reserv_plt.Server.Migrations
{
    /// <inheritdoc />
    public partial class makeFeedbackIDNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackID",
                table: "Reservations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations",
                column: "FeedbackID",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackID",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations",
                column: "FeedbackID",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
