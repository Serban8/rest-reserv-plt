using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reserv_plt.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatereservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ReservationId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackID",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Feedbacks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FeedbackID",
                table: "Reservations",
                column: "FeedbackID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RestaurantId",
                table: "Feedbacks",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Restaurant_RestaurantId",
                table: "Feedbacks",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations",
                column: "FeedbackID",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Restaurant_RestaurantId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Feedbacks_FeedbackID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FeedbackID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_RestaurantId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Feedbacks");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ReservationId",
                table: "Feedbacks",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Reservations_ReservationId",
                table: "Feedbacks",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
