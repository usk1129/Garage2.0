﻿// <auto-generated />
using System;
using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage2._0.Migrations
{
    [DbContext(typeof(Garage2_0Context))]
    [Migration("20220219145411_index2")]
    partial class index2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Garage2._0.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Garage2._0.Models.ParkingSpot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ParkVehicleID")
                        .HasColumnType("int");

                    b.Property<int>("ParkingSpotNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParkVehicleID");

                    b.ToTable("ParkingSpot");
                });

            modelBuilder.Entity("Garage2._0.Models.ParkVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("ParkingSpotId")
                        .HasColumnType("int");

                    b.Property<string>("RegNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("VehicleTypeID")
                        .HasColumnType("int");

                    b.Property<int>("Wheels")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("VehicleTypeID");

                    b.ToTable("ParkVehicle");
                });

            modelBuilder.Entity("Garage2._0.Models.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("Garage2._0.Models.ParkingSpot", b =>
                {
                    b.HasOne("Garage2._0.Models.ParkVehicle", "ParkVehicle")
                        .WithMany("Parkings")
                        .HasForeignKey("ParkVehicleID");

                    b.Navigation("ParkVehicle");
                });

            modelBuilder.Entity("Garage2._0.Models.ParkVehicle", b =>
                {
                    b.HasOne("Garage2._0.Models.Member", "Member")
                        .WithMany("Vehicles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage2._0.Models.VehicleType", "VehicleType")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("Garage2._0.Models.Member", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Garage2._0.Models.ParkVehicle", b =>
                {
                    b.Navigation("Parkings");
                });

            modelBuilder.Entity("Garage2._0.Models.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
