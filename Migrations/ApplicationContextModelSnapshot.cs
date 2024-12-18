﻿// <auto-generated />
using KCIAOGS24.NET.Infraestructure.Data.AppData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace KCIAOGS24.NET.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnderecoEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("cep")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("economia")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("fk_usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("gastoMensal")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("tarifa")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("tipoResidencial")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id");

                    b.HasIndex("fk_usuario");

                    b.ToTable("tb_endereco");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnergiaEolicaEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<double>("alturaTorre")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<double>("diametroRotor")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<double>("energiaEstimadaGerada")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("fk_endereco")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("potencialNominal")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("id");

                    b.HasIndex("fk_endereco")
                        .IsUnique();

                    b.ToTable("tb_energia_eolica");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnergiaSolarEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("areaPlaca")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("energiaEstimadaGerada")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("fk_endereco")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("irradiacaoSolar")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("id");

                    b.HasIndex("fk_endereco")
                        .IsUnique();

                    b.ToTable("tb_energia_solar");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id");

                    b.ToTable("tb_usuario");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnderecoEntity", b =>
                {
                    b.HasOne("KCIAOGS24.NET.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany("Enderecos")
                        .HasForeignKey("fk_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnergiaEolicaEntity", b =>
                {
                    b.HasOne("KCIAOGS24.NET.Domain.Entities.EnderecoEntity", "Endereco")
                        .WithOne("EnergiaEolica")
                        .HasForeignKey("KCIAOGS24.NET.Domain.Entities.EnergiaEolicaEntity", "fk_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnergiaSolarEntity", b =>
                {
                    b.HasOne("KCIAOGS24.NET.Domain.Entities.EnderecoEntity", "Endereco")
                        .WithOne("EnergiaSolar")
                        .HasForeignKey("KCIAOGS24.NET.Domain.Entities.EnergiaSolarEntity", "fk_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.EnderecoEntity", b =>
                {
                    b.Navigation("EnergiaEolica")
                        .IsRequired();

                    b.Navigation("EnergiaSolar")
                        .IsRequired();
                });

            modelBuilder.Entity("KCIAOGS24.NET.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
