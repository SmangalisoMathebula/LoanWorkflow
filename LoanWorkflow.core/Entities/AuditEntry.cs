namespace LoanWorkflow.Core.Entities;

public class AuditEntry
{
    public int Id { get; set; }

    public int LoanApplicationId { get; set; }
    public int UserId { get; set; }

    public string Action { get; set; } = "";
    public string? Comment { get; set; }
    public DateTime Timestamp { get; set; }
}