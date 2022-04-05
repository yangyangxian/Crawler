﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yang.Entities;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(SeashellContext))]
    partial class SeashellContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Yang.Entities.AdministrativeDistrict", b =>
                {
                    b.Property<int>("AdministrativeDistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdministrativeDistrictId"), 1L, 1);

                    b.Property<string>("AdministrativeDistrictName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AdministrativeDistrictId");

                    b.ToTable("AdministrativeDistrict");

                    b.HasData(
                        new
                        {
                            AdministrativeDistrictId = 1,
                            AdministrativeDistrictName = "碑林区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 2,
                            AdministrativeDistrictName = "新城区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 3,
                            AdministrativeDistrictName = "莲湖区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 4,
                            AdministrativeDistrictName = "雁塔区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 5,
                            AdministrativeDistrictName = "未央区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 6,
                            AdministrativeDistrictName = "灞桥区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 7,
                            AdministrativeDistrictName = "长安区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 8,
                            AdministrativeDistrictName = "高陵区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 9,
                            AdministrativeDistrictName = "鄠邑区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 10,
                            AdministrativeDistrictName = "临潼区"
                        },
                        new
                        {
                            AdministrativeDistrictId = 11,
                            AdministrativeDistrictName = "蓝田县"
                        },
                        new
                        {
                            AdministrativeDistrictId = 12,
                            AdministrativeDistrictName = "周至县"
                        },
                        new
                        {
                            AdministrativeDistrictId = 13,
                            AdministrativeDistrictName = "西咸新区（西安）"
                        });
                });

            modelBuilder.Entity("Yang.Entities.Community", b =>
                {
                    b.Property<int>("CommunityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunityId"), 1L, 1);

                    b.Property<int?>("AdministrativeDistrictId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("BuildingNumber")
                        .HasColumnType("int");

                    b.Property<string>("CommunityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("External_id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Neighborhood")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SeashellURL")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("CommunityId");

                    b.HasAlternateKey("CommunityName", "AdministrativeDistrictId");

                    b.HasIndex("AdministrativeDistrictId");

                    b.ToTable("Community", (string)null);
                });

            modelBuilder.Entity("Yang.Entities.CommunityHistoryInfo", b =>
                {
                    b.Property<int>("CommunityHistoryInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunityHistoryInfoId"), 1L, 1);

                    b.Property<int>("CommunityId")
                        .HasColumnType("int");

                    b.Property<decimal>("CommunityListingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CommunityListingUnits")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime2");

                    b.HasKey("CommunityHistoryInfoId");

                    b.HasIndex("CommunityId");

                    b.ToTable("CommunityHistoryInfos");
                });

            modelBuilder.Entity("Yang.Entities.Community", b =>
                {
                    b.HasOne("Yang.Entities.AdministrativeDistrict", "AdministrativeDistrict")
                        .WithMany("Community")
                        .HasForeignKey("AdministrativeDistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdministrativeDistrict");
                });

            modelBuilder.Entity("Yang.Entities.CommunityHistoryInfo", b =>
                {
                    b.HasOne("Yang.Entities.Community", "Community")
                        .WithMany("CommunityHistoryInfo")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("Yang.Entities.AdministrativeDistrict", b =>
                {
                    b.Navigation("Community");
                });

            modelBuilder.Entity("Yang.Entities.Community", b =>
                {
                    b.Navigation("CommunityHistoryInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
