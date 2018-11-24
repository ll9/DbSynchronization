﻿// <auto-generated />
using System;
using DbSyncAlgorithm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbSyncAlgorithm.Migrations
{
    [DbContext(typeof(DbSyncAlgorithmContext))]
    partial class DbSyncAlgorithmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DbSyncAlgorithm.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
