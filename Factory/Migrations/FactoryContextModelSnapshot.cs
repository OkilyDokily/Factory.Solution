﻿// <auto-generated />
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Factory.Migrations
{
    [DbContext(typeof(FactoryContext))]
    partial class FactoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("EngineerId");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.Models.MachineEngineers", b =>
                {
                    b.Property<int>("MachineEngineersId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EngineerId");

                    b.Property<int>("MachineId");

                    b.HasKey("MachineEngineersId");

                    b.HasIndex("EngineerId");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineEngineers");
                });

            modelBuilder.Entity("Factory.Models.MachineEngineers", b =>
                {
                    b.HasOne("Factory.Models.Engineer", "Engineer")
                        .WithMany("Machines")
                        .HasForeignKey("EngineerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Factory.Models.Machine", "Machine")
                        .WithMany("Engineers")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
