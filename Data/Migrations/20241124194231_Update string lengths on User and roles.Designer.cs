﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Net8StarterAuthApi.Data;

#nullable disable

namespace Net8StarterAuthApi.Data.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20241124194231_Update string lengths on User and roles")]
    partial class UpdatestringlengthsonUserandroles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Net8StarterAuthApi.Models.AdminRole", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("name");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("description");

                    b.HasKey("Name")
                        .HasName("pk_admin_roles");

                    b.ToTable("admin_roles", (string)null);
                });

            modelBuilder.Entity("Net8StarterAuthApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
