﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace reserv_plt.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("DataLayer.Models.Confirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ReservationID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReservationID")
                        .IsUnique();

                    b.ToTable("Confirmations");
                });

            modelBuilder.Entity("DataLayer.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ReservationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RestaurantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserID");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("DataLayer.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FeedbackID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TableID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackID")
                        .IsUnique();

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableID");

                    b.HasIndex("UserID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DataLayer.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Schedule")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("DataLayer.Models.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RestaurantID")
                        .HasColumnType("TEXT");

                    b.Property<int>("Seats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TableNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Models.Confirmation", b =>
                {
                    b.HasOne("DataLayer.Models.Reservation", "Reservation")
                        .WithOne("Confirmation")
                        .HasForeignKey("DataLayer.Models.Confirmation", "ReservationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("DataLayer.Models.Feedback", b =>
                {
                    b.HasOne("DataLayer.Models.Restaurant", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("RestaurantId");

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Reservation", b =>
                {
                    b.HasOne("DataLayer.Models.Feedback", "Feedback")
                        .WithOne("Reservation")
                        .HasForeignKey("DataLayer.Models.Reservation", "FeedbackID");

                    b.HasOne("DataLayer.Models.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feedback");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Table", b =>
                {
                    b.HasOne("DataLayer.Models.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("DataLayer.Models.Feedback", b =>
                {
                    b.Navigation("Reservation")
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Reservation", b =>
                {
                    b.Navigation("Confirmation")
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Restaurant", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("DataLayer.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
