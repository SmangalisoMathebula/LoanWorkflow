namespace LoanWorkflow.Core.Entities;

public class LoanApplication
{
    public int Id { get; set; }

    public int ApplicantId { get; set; }
    public User Applicant { get; set; } = null!;

    public string FullName { get; set; } = "";
    public string IdNumber { get; set; } = "";
    public decimal MonthlyIncome { get; set; }
    public decimal MonthlyExpenses { get; set; }
    public decimal ExistingDebtPayments { get; set; }
    public int CreditScore { get; set; }
    public decimal RequestedAmount { get; set; }
    public int TermMonths { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public DateTime CreatedAt { get; set; }

    public List<AuditEntry> AuditTrail { get; set; } = new();
}