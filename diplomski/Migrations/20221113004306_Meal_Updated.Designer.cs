﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using diplomski.Data.Context;

#nullable disable

namespace diplomski.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221113004306_Meal_Updated")]
    partial class Meal_Updated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("diplomski.Data.Models.AdminData", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("AdminDatas");
                });

            modelBuilder.Entity("diplomski.Data.Models.AdminDefault", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSubject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatteningText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeepingFitText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnpersonalizedText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeightLossText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminDefaults");
                });

            modelBuilder.Entity("diplomski.Data.Models.Foodstuff", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("real");

                    b.Property<float>("Fats")
                        .HasColumnType("real");

                    b.Property<int>("Grams")
                        .HasColumnType("int");

                    b.Property<float>("Proteins")
                        .HasColumnType("real");

                    b.HasKey("Name");

                    b.ToTable("Foodstuffs");
                });

            modelBuilder.Entity("diplomski.Data.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("real");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<float>("Fats")
                        .HasColumnType("real");

                    b.Property<string>("Ingredient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mass")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<float>("Proteins")
                        .HasColumnType("real");

                    b.Property<string>("Recipe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TemplateName");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("diplomski.Data.Models.Template", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("diplomski.Data.Models.UserData", b =>
                {
                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ActivityLevel")
                        .HasColumnType("int");

                    b.Property<string>("Additions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("DailyNumberOfMeals")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Mail");

                    b.ToTable("UserDatas");
                });

            modelBuilder.Entity("diplomski.Data.Models.Meal", b =>
                {
                    b.HasOne("diplomski.Data.Models.Template", "Template")
                        .WithMany("Meals")
                        .HasForeignKey("TemplateName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("diplomski.Data.Models.Template", b =>
                {
                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
