﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OtpCode.Api.Data;

#nullable disable

namespace OtpCode.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230909002537_Add_GetOtp_Stored_Proc")]
    partial class AddGetOtpStoredProc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OtpCode.Api.Data.Entities.OtpEntry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InvalidAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Metadata")
                        .HasColumnType("longtext");

                    b.Property<string>("OtpCode")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Purpose")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("OtpEntries", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}