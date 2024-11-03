﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pokedex_back.Data;

#nullable disable

namespace pokedex_back.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241102183941_table-users")]
    partial class tableusers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("pokedex_back.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("createdAt");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("hash");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("salt");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updatedAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
