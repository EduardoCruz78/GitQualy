﻿// <auto-generated />
using System;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Backend.Domain.Entities.Coleta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<int>("IndicadorId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IndicadorId");

                    b.ToTable("Coletas");
                });

            modelBuilder.Entity("Backend.Domain.Entities.Indicador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoCalculo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Indicadores");
                });

            modelBuilder.Entity("Backend.Domain.Entities.Coleta", b =>
                {
                    b.HasOne("Backend.Domain.Entities.Indicador", "Indicador")
                        .WithMany("Coletas")
                        .HasForeignKey("IndicadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Indicador");
                });

            modelBuilder.Entity("Backend.Domain.Entities.Indicador", b =>
                {
                    b.Navigation("Coletas");
                });
#pragma warning restore 612, 618
        }
    }
}
