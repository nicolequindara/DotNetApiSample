﻿// <auto-generated />
using DotNetApiSample.Database;
using DotNetApiSample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DotNetApiSample.Migrations
{
    [DbContext(typeof(TransactionDbContext))]
    [Migration("20180215232735_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetApiSample.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Line1")
                        .IsRequired();

                    b.Property<string>("Line2");

                    b.Property<int>("State");

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DotNetApiSample.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int?>("HomeAddressId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("HomeAddressId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("DotNetApiSample.Domain.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int?>("PayeeId");

                    b.Property<int?>("PayerId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("PayeeId");

                    b.HasIndex("PayerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DotNetApiSample.Domain.Person", b =>
                {
                    b.HasOne("DotNetApiSample.Domain.Address", "HomeAddress")
                        .WithMany()
                        .HasForeignKey("HomeAddressId");
                });

            modelBuilder.Entity("DotNetApiSample.Domain.Transaction", b =>
                {
                    b.HasOne("DotNetApiSample.Domain.Person", "Payee")
                        .WithMany()
                        .HasForeignKey("PayeeId");

                    b.HasOne("DotNetApiSample.Domain.Person", "Payer")
                        .WithMany()
                        .HasForeignKey("PayerId");
                });
#pragma warning restore 612, 618
        }
    }
}
