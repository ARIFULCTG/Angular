﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NG6_R51;

#nullable disable

namespace NG6_R51.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230209043456_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NG6_R51.player", b =>
                {
                    b.Property<string>("playercode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("earned")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("matchdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("playername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("traincost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("trainerid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("playercode");

                    b.HasIndex("trainerid");

                    b.ToTable("players");
                });

            modelBuilder.Entity("NG6_R51.trainer", b =>
                {
                    b.Property<string>("trainerid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trainername")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("trainerid");

                    b.ToTable("trainers");
                });

            modelBuilder.Entity("NG6_R51.player", b =>
                {
                    b.HasOne("NG6_R51.trainer", "trainer")
                        .WithMany("player")
                        .HasForeignKey("trainerid");

                    b.Navigation("trainer");
                });

            modelBuilder.Entity("NG6_R51.trainer", b =>
                {
                    b.Navigation("player");
                });
#pragma warning restore 612, 618
        }
    }
}
