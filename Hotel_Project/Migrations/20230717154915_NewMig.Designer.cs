﻿// <auto-generated />
using System;
using Hotel_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotel_Project.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230717154915_NewMig")]
    partial class NewMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Hotel_Project.Models.Entities.Account.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Hotel_Project.Models.Entities.Web.FisrtBaner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BanerButton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BanerTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("fisrtBaners");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.AdvantageRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("advantageRooms");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.AdvantageToRoom", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("AdvantageId")
                        .HasColumnType("int");

                    b.HasKey("RoomId", "AdvantageId");

                    b.HasIndex("AdvantageId");

                    b.ToTable("advantageToRs");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("EntryTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExitTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<int>("StageCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelAddress", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("HotelId");

                    b.ToTable("hotelAddresses");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("hotelGalleries");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BedCount")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("RoomPrice")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("hotelRooms");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("hotelRules");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.ReserveDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("IsReserve")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReserveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("reserveDates");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.AdvantageToRoom", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.AdvantageRoom", "advantageRoom")
                        .WithMany("advantageToRooms")
                        .HasForeignKey("AdvantageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hotel_Project.Models.Product.HotelRoom", "hotelRoom")
                        .WithMany("advantageToRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("advantageRoom");

                    b.Navigation("hotelRoom");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelAddress", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.Hotel", "hotel")
                        .WithOne("hotelAddress")
                        .HasForeignKey("Hotel_Project.Models.Product.HotelAddress", "HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelGallery", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.Hotel", "hotel")
                        .WithMany("hotelGalleries")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelRoom", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.Hotel", "hotel")
                        .WithMany("hotelRooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelRule", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.Hotel", "hotel")
                        .WithMany("hotelRules")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.ReserveDate", b =>
                {
                    b.HasOne("Hotel_Project.Models.Product.HotelRoom", "HotelRoom")
                        .WithMany("reserveDates")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelRoom");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.AdvantageRoom", b =>
                {
                    b.Navigation("advantageToRooms");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.Hotel", b =>
                {
                    b.Navigation("hotelAddress")
                        .IsRequired();

                    b.Navigation("hotelGalleries");

                    b.Navigation("hotelRooms");

                    b.Navigation("hotelRules");
                });

            modelBuilder.Entity("Hotel_Project.Models.Product.HotelRoom", b =>
                {
                    b.Navigation("advantageToRooms");

                    b.Navigation("reserveDates");
                });
#pragma warning restore 612, 618
        }
    }
}
