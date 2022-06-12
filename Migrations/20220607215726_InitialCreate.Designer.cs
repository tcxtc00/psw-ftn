﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using psw_ftn.Data;

namespace psw_ftn.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220607215726_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("psw_ftn.Models.CancelledCheckUp", b =>
                {
                    b.Property<int>("CancelledCheckUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CancelationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CheckUpId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("CancelledCheckUpId");

                    b.HasIndex("CheckUpId");

                    b.HasIndex("PatientId");

                    b.ToTable("CancelledCheckUps");
                });

            modelBuilder.Entity("psw_ftn.Models.CheckUp", b =>
                {
                    b.Property<int>("CheckUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CancellationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PatientUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("CheckUpId");

                    b.HasIndex("DoctorUserId");

                    b.HasIndex("PatientUserId");

                    b.ToTable("CheckUps");
                });

            modelBuilder.Entity("psw_ftn.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<bool>("Incognito")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForDisplay")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("PatientId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("psw_ftn.Models.HistoryCheckUp", b =>
                {
                    b.Property<int>("CheckUpId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.HasKey("CheckUpId");

                    b.ToTable("HistoryCheckUps");
                });

            modelBuilder.Entity("psw_ftn.Models.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Admin", b =>
                {
                    b.HasBaseType("psw_ftn.Models.User.User");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Doctor", b =>
                {
                    b.HasBaseType("psw_ftn.Models.User.User");

                    b.Property<string>("Expertise")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Patient", b =>
                {
                    b.HasBaseType("psw_ftn.Models.User.User");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("psw_ftn.Models.CancelledCheckUp", b =>
                {
                    b.HasOne("psw_ftn.Models.CheckUp", "CheckUp")
                        .WithMany("CancelledCheckUps")
                        .HasForeignKey("CheckUpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("psw_ftn.Models.User.UserTypes.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckUp");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("psw_ftn.Models.CheckUp", b =>
                {
                    b.HasOne("psw_ftn.Models.User.UserTypes.Doctor", "Doctor")
                        .WithMany("CheckUps")
                        .HasForeignKey("DoctorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("psw_ftn.Models.User.UserTypes.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientUserId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("psw_ftn.Models.Feedback", b =>
                {
                    b.HasOne("psw_ftn.Models.User.UserTypes.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("psw_ftn.Models.HistoryCheckUp", b =>
                {
                    b.HasOne("psw_ftn.Models.CheckUp", "CheckUp")
                        .WithOne("HistoryCheckUp")
                        .HasForeignKey("psw_ftn.Models.HistoryCheckUp", "CheckUpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckUp");
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Admin", b =>
                {
                    b.HasOne("psw_ftn.Models.User.User", null)
                        .WithOne()
                        .HasForeignKey("psw_ftn.Models.User.UserTypes.Admin", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Doctor", b =>
                {
                    b.HasOne("psw_ftn.Models.User.User", null)
                        .WithOne()
                        .HasForeignKey("psw_ftn.Models.User.UserTypes.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Patient", b =>
                {
                    b.HasOne("psw_ftn.Models.User.User", null)
                        .WithOne()
                        .HasForeignKey("psw_ftn.Models.User.UserTypes.Patient", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("psw_ftn.Models.CheckUp", b =>
                {
                    b.Navigation("CancelledCheckUps");

                    b.Navigation("HistoryCheckUp");
                });

            modelBuilder.Entity("psw_ftn.Models.User.UserTypes.Doctor", b =>
                {
                    b.Navigation("CheckUps");
                });
#pragma warning restore 612, 618
        }
    }
}
