using LoanWorkflow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanWorkflow.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(u => u.Username).IsUnique();
            e.Property(u => u.Username).HasMaxLength(100).IsRequired();
            e.Property(u => u.Role).HasConversion<string>();
        });

        b.Entity<LoanApplication>(e =>
        {
            e.Property(a => a.Status).HasConversion<string>();
            e.Property(a => a.MonthlyIncome).HasPrecision(18, 2);
            e.Property(a => a.MonthlyExpenses).HasPrecision(18, 2);
            e.Property(a => a.ExistingDebtPayments).HasPrecision(18, 2);
            e.Property(a => a.RequestedAmount).HasPrecision(18, 2);

            e.HasOne(a => a.Applicant)
             .WithMany()
             .HasForeignKey(a => a.ApplicantId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(a => a.AuditTrail)
             .WithOne()
             .HasForeignKey(x => x.LoanApplicationId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<AuditEntry>(e =>
        {
            e.HasOne<User>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}