﻿// <auto-generated />
using System;
using CulturalEventsData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CulturalEventsData.Migrations
{
    [DbContext(typeof(CulturalEventsContext))]
    partial class CulturalEventsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CulturalEventsData.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Surname");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CulturalEventsData.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentNumber");

                    b.Property<string>("City");

                    b.Property<int>("CreatorId");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<int>("HouseNumber");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("PostalCode");

                    b.Property<int?>("StaticCategoryId");

                    b.Property<string>("Street");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("StaticCategoryId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CulturalEventsData.Models.EventAccount", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("EventId");

                    b.Property<int>("Id");

                    b.HasKey("AccountId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventsAccounts");
                });

            modelBuilder.Entity("CulturalEventsData.Models.PaidEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BottomText");

                    b.Property<int>("EventId");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("TopText");

                    b.HasKey("Id");

                    b.ToTable("PaidEvents");
                });

            modelBuilder.Entity("CulturalEventsData.Models.RestrictedPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int>("ApartmentNumber");

                    b.Property<string>("City");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("PostalCode");

                    b.HasKey("Id");

                    b.ToTable("RestrictedPlaces");
                });

            modelBuilder.Entity("CulturalEventsData.Models.StaticCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CulturalEventsData.Models.StaticContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CulturalEventsData.Models.Event", b =>
                {
                    b.HasOne("CulturalEventsData.Models.StaticCategory", "StaticCategory")
                        .WithMany("Events")
                        .HasForeignKey("StaticCategoryId");
                });

            modelBuilder.Entity("CulturalEventsData.Models.EventAccount", b =>
                {
                    b.HasOne("CulturalEventsData.Models.Account", "Account")
                        .WithMany("EventsAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CulturalEventsData.Models.Event", "Event")
                        .WithMany("EventsAccounts")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
