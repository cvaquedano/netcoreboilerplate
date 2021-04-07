﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreWebApiBoilerPlate;

namespace NetCoreWebApiBoilerPlate.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.ExampleMasterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MasterStatusEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MasterStatusEntityId");

                    b.ToTable("ExampleMasterEntity");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.MasterDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExampleMasterEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExampleMasterEntityId");

                    b.ToTable("MasterDetailEntity");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.MasterStatusEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MasterStatusEntity");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.ExampleMasterEntity", b =>
                {
                    b.HasOne("NetCoreWebApiBoilerPlate.Entities.MasterStatusEntity", "MasterStatusEntity")
                        .WithMany()
                        .HasForeignKey("MasterStatusEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterStatusEntity");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.MasterDetailEntity", b =>
                {
                    b.HasOne("NetCoreWebApiBoilerPlate.Entities.ExampleMasterEntity", "ExampleMasterEntity")
                        .WithMany("MasterDetailEntities")
                        .HasForeignKey("ExampleMasterEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExampleMasterEntity");
                });

            modelBuilder.Entity("NetCoreWebApiBoilerPlate.Entities.ExampleMasterEntity", b =>
                {
                    b.Navigation("MasterDetailEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
