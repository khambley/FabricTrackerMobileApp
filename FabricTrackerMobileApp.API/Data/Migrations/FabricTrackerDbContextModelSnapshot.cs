﻿// <auto-generated />
using System;
using FabricTrackerMobileApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FabricTrackerMobileApp.API.Data.Migrations
{
    [DbContext(typeof(FabricTrackerDbContext))]
    partial class FabricTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.28");

            modelBuilder.Entity("FabricTrackerMobileApp.API.Models.Fabrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccentColor1")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccentColor2")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccentColor3")
                        .HasColumnType("TEXT");

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Designer")
                        .HasColumnType("TEXT");

                    b.Property<string>("FabricType")
                        .HasColumnType("TEXT");

                    b.Property<int>("FatQtrQty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDiscontinued")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("MainCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalInches")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Fabrics");
                });

            modelBuilder.Entity("FabricTrackerMobileApp.API.Models.MainCategory", b =>
                {
                    b.Property<int>("MainCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MainCategoryName")
                        .HasColumnType("TEXT");

                    b.HasKey("MainCategoryId");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("FabricTrackerMobileApp.API.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MainCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("TEXT");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("FabricTrackerMobileApp.API.Models.SubCategory", b =>
                {
                    b.HasOne("FabricTrackerMobileApp.API.Models.MainCategory", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
