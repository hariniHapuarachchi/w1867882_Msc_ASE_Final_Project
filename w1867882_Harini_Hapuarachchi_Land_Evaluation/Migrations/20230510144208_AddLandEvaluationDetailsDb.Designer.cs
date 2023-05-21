﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;

#nullable disable

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230510144208_AddLandEvaluationDetailsDb")]
    partial class AddLandEvaluationDetailsDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Evaluation", b =>
                {
                    b.Property<int>("EvaluationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvaluationId"));

                    b.Property<int>("LandId")
                        .HasColumnType("int");

                    b.Property<string>("Prediction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EvaluationId");

                    b.HasIndex("LandId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Land", b =>
                {
                    b.Property<int>("LandId")
                        .HasColumnType("int");

                    b.Property<int>("ClassOfLandUnit")
                        .HasColumnType("int");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<float>("EvaluationLow")
                        .HasColumnType("real");

                    b.Property<float>("EvaluationMis")
                        .HasColumnType("real");

                    b.Property<float>("EvaluationUp")
                        .HasColumnType("real");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MeanAnualRF")
                        .HasColumnType("real");

                    b.Property<string>("PastErosion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("RockOutcrops")
                        .HasColumnType("real");

                    b.Property<float>("SlopeAngle")
                        .HasColumnType("real");

                    b.Property<float>("SoilDepth")
                        .HasColumnType("real");

                    b.Property<string>("SoilDrainageClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SoilPH")
                        .HasColumnType("real");

                    b.Property<string>("SoilTexture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("StonesAndGrovels")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LandId");

                    b.HasIndex("UserId");

                    b.ToTable("Lands");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Evaluation", b =>
                {
                    b.HasOne("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Land", "Land")
                        .WithMany("Evaluations")
                        .HasForeignKey("LandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Land");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Land", b =>
                {
                    b.HasOne("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.User", "User")
                        .WithMany("Lands")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.Land", b =>
                {
                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("w1867882_Harini_Hapuarachchi_Land_Evaluation.Models.User", b =>
                {
                    b.Navigation("Lands");
                });
#pragma warning restore 612, 618
        }
    }
}