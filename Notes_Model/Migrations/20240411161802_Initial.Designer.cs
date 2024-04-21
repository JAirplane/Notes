﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notes_Model.PostgresDB;

#nullable disable

namespace Notes_Model.Migrations
{
    [DbContext(typeof(NotesContext))]
    [Migration("20240411161802_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NoteTag", b =>
                {
                    b.Property<int>("NoteTagsId")
                        .HasColumnType("int");

                    b.Property<int>("NotesId")
                        .HasColumnType("int");

                    b.HasKey("NoteTagsId", "NotesId");

                    b.HasIndex("NotesId");

                    b.ToTable("NoteTag");
                });

            modelBuilder.Entity("Notes_Model.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Note");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notes_Model.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("Notes_Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Notes_Model.Reminder", b =>
                {
                    b.HasBaseType("Notes_Model.Note");

                    b.Property<DateTime>("RemindTime")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Reminder");
                });

            modelBuilder.Entity("NoteTag", b =>
                {
                    b.HasOne("Notes_Model.Tag", null)
                        .WithMany()
                        .HasForeignKey("NoteTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notes_Model.Note", null)
                        .WithMany()
                        .HasForeignKey("NotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Notes_Model.Note", b =>
                {
                    b.HasOne("Notes_Model.User", null)
                        .WithMany("UserNotes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Notes_Model.Tag", b =>
                {
                    b.HasOne("Notes_Model.User", null)
                        .WithMany("UserTags")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Notes_Model.User", b =>
                {
                    b.OwnsOne("Notes_Model.Credentials", "Сredentials", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("Login")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Password")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Сredentials")
                        .IsRequired();
                });

            modelBuilder.Entity("Notes_Model.User", b =>
                {
                    b.Navigation("UserNotes");

                    b.Navigation("UserTags");
                });
#pragma warning restore 612, 618
        }
    }
}