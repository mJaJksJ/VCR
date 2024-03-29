﻿// <auto-generated />
using System;
using Iris.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable
#pragma warning disable 1591
namespace Iris.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220430130119_AddTokenToUser")]
    partial class AddTokenToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Iris.Database.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConnectionProtocol")
                        .HasColumnType("TEXT");

                    b.Property<int>("MailServerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseSsl")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MailServerId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Iris.Database.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Blob")
                        .HasColumnType("BLOB");

                    b.Property<int>("LetterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Iris.Database.AuthRequestOperation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("IssuedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AuthRequests");
                });

            modelBuilder.Entity("Iris.Database.Letter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SenderId");

                    b.ToTable("Letters");
                });

            modelBuilder.Entity("Iris.Database.MailServer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MailServers");
                });

            modelBuilder.Entity("Iris.Database.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Iris.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LetterPerson", b =>
                {
                    b.Property<int>("ReceivedLettersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReceiversId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReceivedLettersId", "ReceiversId");

                    b.HasIndex("ReceiversId");

                    b.ToTable("LetterPerson");
                });

            modelBuilder.Entity("Iris.Database.Account", b =>
                {
                    b.HasOne("Iris.Database.MailServer", "MailServer")
                        .WithMany("Accounts")
                        .HasForeignKey("MailServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iris.Database.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MailServer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Iris.Database.Attachment", b =>
                {
                    b.HasOne("Iris.Database.Letter", "Letter")
                        .WithMany("Attachments")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Letter");
                });

            modelBuilder.Entity("Iris.Database.Letter", b =>
                {
                    b.HasOne("Iris.Database.Account", "Account")
                        .WithMany("Letters")
                        .HasForeignKey("AccountId");

                    b.HasOne("Iris.Database.Person", "Sender")
                        .WithMany("SentLetters")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("LetterPerson", b =>
                {
                    b.HasOne("Iris.Database.Letter", null)
                        .WithMany()
                        .HasForeignKey("ReceivedLettersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iris.Database.Person", null)
                        .WithMany()
                        .HasForeignKey("ReceiversId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Iris.Database.Account", b =>
                {
                    b.Navigation("Letters");
                });

            modelBuilder.Entity("Iris.Database.Letter", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Iris.Database.MailServer", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Iris.Database.Person", b =>
                {
                    b.Navigation("SentLetters");
                });

            modelBuilder.Entity("Iris.Database.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
