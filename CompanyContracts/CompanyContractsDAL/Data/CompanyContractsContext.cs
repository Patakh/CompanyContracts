using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyContracts.Data;

public partial class CompanyContractsContext : DbContext
{
    public CompanyContractsContext()
    {
    }

    public CompanyContractsContext(DbContextOptions<CompanyContractsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<LegalEntity> LegalEntities { get; set; }

    public virtual DbSet<PhysicalPerson> PhysicalPersons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;User Id=postgres;Password=123;Port=5432;Database=CompanyContracts;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contracts_pkey");

            entity.ToTable("contracts", tb => tb.HasComment("Договоры"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorizedPersonId)
                .HasComment("ID уполномоченного лица (физического лица)")
                .HasColumnName("authorized_person_id");
            entity.Property(e => e.ContractAmount)
                .HasPrecision(10, 2)
                .HasComment("Сумма договора")
                .HasColumnName("contract_amount");
            entity.Property(e => e.ContractorId)
                .HasComment("ID контрагента (юридического лица)")
                .HasColumnName("contractor_id");
            entity.Property(e => e.SigningDate)
                .HasComment("Дата подписания договора")
                .HasColumnName("signing_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasComment("Статус договора")
                .HasColumnName("status");

            entity.HasOne(d => d.AuthorizedPerson).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.AuthorizedPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contracts_authorizedperson_id_fkey");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ContractorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contracts_contractor_id_fkey");
        });

        modelBuilder.Entity<LegalEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("legalentities_pkey");

            entity.ToTable("legal_entities", tb => tb.HasComment("Юридическое лицо"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('legalentities_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasComment("Адрес")
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasComment("Город регистрации")
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasComment("Страна регистрации")
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasComment("Электронная почта")
                .HasColumnName("email");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasComment("ИНН")
                .HasColumnName("inn");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Наименование")
                .HasColumnName("name");
            entity.Property(e => e.Ogrn)
                .HasMaxLength(13)
                .HasComment("ОГРН")
                .HasColumnName("ogrn");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("Номер телефона")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<PhysicalPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("physicalpersons_pkey");

            entity.ToTable("physical_persons", tb => tb.HasComment("Физическое лицо"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('physicalpersons_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasComment("Адрес")
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasComment("Возраст")
                .HasColumnName("age");
            entity.Property(e => e.BirthDate)
                .HasComment("Дата рождения")
                .HasColumnName("birth_date");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasComment("Город проживания")
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasComment("Страна проживания")
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasComment("Электронная почта")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasComment("Имя")
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasComment("Пол")
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasComment("Фамилия")
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .HasComment("Отчество")
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("Номер телефона")
                .HasColumnName("phone");
            entity.Property(e => e.WorkPlace)
                .HasMaxLength(255)
                .HasComment("Место работы")
                .HasColumnName("work_place");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
