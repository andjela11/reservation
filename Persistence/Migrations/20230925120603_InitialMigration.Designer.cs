﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230925120603_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumbers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovieId = 1,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 2,
                            MovieId = 2,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 3,
                            MovieId = 3,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 4,
                            MovieId = 4,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 5,
                            MovieId = 5,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 6,
                            MovieId = 6,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 7,
                            MovieId = 7,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 8,
                            MovieId = 8,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 9,
                            MovieId = 9,
                            SeatNumbers = 250
                        },
                        new
                        {
                            Id = 10,
                            MovieId = 10,
                            SeatNumbers = 250
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
