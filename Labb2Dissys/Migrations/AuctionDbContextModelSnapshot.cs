﻿// <auto-generated />
using System;
using Labb2Dissys.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Labb2Dissys.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    partial class AuctionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Labb2Dissys.Persistence.AuctionDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Seller")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("AuctionDbs");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Description = "Classic vintage cars from the 1950s.",
                            EndDate = new DateTime(2024, 12, 19, 22, 3, 49, 714, DateTimeKind.Local).AddTicks(4428),
                            Seller = "user1@kth.se",
                            StartingPrice = 10000m,
                            Title = "Test Car Auction"
                        },
                        new
                        {
                            Id = -2,
                            Description = "Någonting.",
                            EndDate = new DateTime(2024, 12, 26, 22, 3, 49, 714, DateTimeKind.Local).AddTicks(4468),
                            Seller = "user1@kth.se",
                            StartingPrice = 2000m,
                            Title = "Antique Vase Auction Test Data"
                        });
                });

            modelBuilder.Entity("Labb2Dissys.Persistence.BidDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AuctionId")
                        .HasColumnType("int");

                    b.Property<string>("Bidder")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.ToTable("BidDbs");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Amount = 10500m,
                            AuctionId = -1,
                            Bidder = "bidder1@kth.se",
                            Timestamp = new DateTime(2024, 12, 11, 22, 3, 49, 714, DateTimeKind.Local).AddTicks(4625)
                        },
                        new
                        {
                            Id = -2,
                            Amount = 12000m,
                            AuctionId = -2,
                            Bidder = "bidder2@kth.se",
                            Timestamp = new DateTime(2024, 12, 10, 22, 3, 49, 714, DateTimeKind.Local).AddTicks(4628)
                        });
                });

            modelBuilder.Entity("Labb2Dissys.Persistence.BidDb", b =>
                {
                    b.HasOne("Labb2Dissys.Persistence.AuctionDb", "AuctionDb")
                        .WithMany("BidDbs")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuctionDb");
                });

            modelBuilder.Entity("Labb2Dissys.Persistence.AuctionDb", b =>
                {
                    b.Navigation("BidDbs");
                });
#pragma warning restore 612, 618
        }
    }
}
