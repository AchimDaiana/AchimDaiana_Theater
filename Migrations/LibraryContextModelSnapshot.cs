﻿// <auto-generated />
using System;
using AchimDaiana_Theater.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AchimDaianaTheater.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AchimDaiana_Theater.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Play", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WriterID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("WriterID");

                    b.ToTable("Play", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("PlayID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReservationID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PlayID");

                    b.ToTable("Reservation", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Theater", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("TheaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Theater", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.TheaterPlay", b =>
                {
                    b.Property<int>("PlayID")
                        .HasColumnType("int");

                    b.Property<int>("TheaterID")
                        .HasColumnType("int");

                    b.HasKey("PlayID", "TheaterID");

                    b.HasIndex("TheaterID");

                    b.ToTable("TheaterPlay", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Writer", b =>
                {
                    b.Property<int>("WriterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WriterID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WriterID");

                    b.ToTable("Writer", (string)null);
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Play", b =>
                {
                    b.HasOne("AchimDaiana_Theater.Models.Writer", "Writer")
                        .WithMany("Plays")
                        .HasForeignKey("WriterID");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Reservation", b =>
                {
                    b.HasOne("AchimDaiana_Theater.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AchimDaiana_Theater.Models.Play", "Play")
                        .WithMany("Reservations")
                        .HasForeignKey("PlayID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Play");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.TheaterPlay", b =>
                {
                    b.HasOne("AchimDaiana_Theater.Models.Play", "Play")
                        .WithMany("TheaterPlays")
                        .HasForeignKey("PlayID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AchimDaiana_Theater.Models.Theater", "Theater")
                        .WithMany("TheaterPlays")
                        .HasForeignKey("TheaterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Play");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Play", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("TheaterPlays");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Theater", b =>
                {
                    b.Navigation("TheaterPlays");
                });

            modelBuilder.Entity("AchimDaiana_Theater.Models.Writer", b =>
                {
                    b.Navigation("Plays");
                });
#pragma warning restore 612, 618
        }
    }
}
