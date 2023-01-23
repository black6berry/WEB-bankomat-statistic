using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEB_bankomat_statistic_api.Models;

public partial class BankomatContext : DbContext
{
    public BankomatContext()
    {
    }

    public BankomatContext(DbContextOptions<BankomatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccountClient> BankAccountClients { get; set; }

    public virtual DbSet<BankHaveBankomat> BankHaveBankomats { get; set; }

    public virtual DbSet<Banknote> Banknotes { get; set; }

    public virtual DbSet<Bankomat> Bankomats { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Cassette> Cassettes { get; set; }

    public virtual DbSet<CassetteBanknote> CassetteBanknotes { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TypeAccount> TypeAccounts { get; set; }

    public virtual DbSet<TypeCard> TypeCards { get; set; }

    public virtual DbSet<TypeCassette> TypeCassettes { get; set; }

    public virtual DbSet<TypeOperation> TypeOperations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=master;Database=Bankomat;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.ToTable("Bank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bik)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("BIK");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Denomination).HasMaxLength(50);
            entity.Property(e => e.Home).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<BankAccountClient>(entity =>
        {
            entity.ToTable("BankAccountClient");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountNumber).HasMaxLength(20);
            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.DateClose).HasColumnType("datetime");
            entity.Property(e => e.DateOpen).HasColumnType("datetime");
            entity.Property(e => e.OperationId).HasColumnName("OperationID");
            entity.Property(e => e.TypeAccountId).HasColumnName("TypeAccountID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ValueMoney).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Card).WithMany(p => p.BankAccountClients)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankAccountClient_Card");

            entity.HasOne(d => d.Operation).WithMany(p => p.BankAccountClients)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankAccountClient_Operation");

            entity.HasOne(d => d.TypeAccount).WithMany(p => p.BankAccountClients)
                .HasForeignKey(d => d.TypeAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankAccountClient_TypeAccount");

            entity.HasOne(d => d.User).WithMany(p => p.BankAccountClients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankAccountClient_User");
        });

        modelBuilder.Entity<BankHaveBankomat>(entity =>
        {
            entity.ToTable("BankHaveBankomat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.BankomatId).HasColumnName("BankomatID");

            entity.HasOne(d => d.Bank).WithMany(p => p.BankHaveBankomats)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankHaveBankomat_Bank");

            entity.HasOne(d => d.Bankomat).WithMany(p => p.BankHaveBankomats)
                .HasForeignKey(d => d.BankomatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankHaveBankomat_Bankomat");
        });

        modelBuilder.Entity<Banknote>(entity =>
        {
            entity.ToTable("Banknote");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BanknoteSeries).HasMaxLength(11);
            entity.Property(e => e.Currency).HasMaxLength(15);
            entity.Property(e => e.Nominal).HasMaxLength(5);
        });

        modelBuilder.Entity<Bankomat>(entity =>
        {
            entity.HasKey(e => e.Number);

            entity.ToTable("Bankomat");

            entity.Property(e => e.Number).ValueGeneratedNever();
            entity.Property(e => e.CasseteBanknoteId).HasColumnName("CasseteBanknoteID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Home).HasMaxLength(5);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.CasseteBanknote).WithMany(p => p.Bankomats)
                .HasForeignKey(d => d.CasseteBanknoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bankomat_CassetteBanknote");

            entity.HasOne(d => d.Status).WithMany(p => p.Bankomats)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bankomat_Status");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("CVV");
            entity.Property(e => e.Day)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Month)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Number)
                .HasMaxLength(21)
                .IsFixedLength();
            entity.Property(e => e.TypeCardId).HasColumnName("TypeCardID");
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .IsFixedLength();

            entity.HasOne(d => d.TypeCard).WithMany(p => p.Cards)
                .HasForeignKey(d => d.TypeCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_TypeCard");
        });

        modelBuilder.Entity<Cassette>(entity =>
        {
            entity.ToTable("Cassette");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Number)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.TypeCassetteId).HasColumnName("TypeCassetteID");

            entity.HasOne(d => d.TypeCassette).WithMany(p => p.Cassettes)
                .HasForeignKey(d => d.TypeCassetteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cassette_TypeCassette");
        });

        modelBuilder.Entity<CassetteBanknote>(entity =>
        {
            entity.ToTable("CassetteBanknote");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BanknoteId).HasColumnName("BanknoteID");
            entity.Property(e => e.CassetteId).HasColumnName("CassetteID");

            entity.HasOne(d => d.Banknote).WithMany(p => p.CassetteBanknotes)
                .HasForeignKey(d => d.BanknoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CassetteBanknote_Banknote");

            entity.HasOne(d => d.Cassette).WithMany(p => p.CassetteBanknotes)
                .HasForeignKey(d => d.CassetteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CassetteBanknote_Cassette");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.ToTable("Operation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateOperation).HasColumnType("datetime");
            entity.Property(e => e.TypeOperationId).HasColumnName("TypeOperationID");
            entity.Property(e => e.ValueMoney).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.BankomatNumberNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.BankomatNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_Bankomat");

            entity.HasOne(d => d.TypeOperation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.TypeOperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_TypeOperation");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeAccount>(entity =>
        {
            entity.ToTable("TypeAccount");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeCard>(entity =>
        {
            entity.ToTable("TypeCard");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.WithdrawalLimit).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<TypeCassette>(entity =>
        {
            entity.ToTable("TypeCassette");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeOperation>(entity =>
        {
            entity.ToTable("TypeOperation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Dateborn).HasColumnType("date");
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Home).HasMaxLength(5);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.PassportIssueDate).HasColumnType("date");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.PassportSeries)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronomic).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.RegistrationAddress).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
