using LoanWorkflow.Core.Entities;

namespace LoanWorkflow.Core.Rules;

public class CreditScoreRule : ILoanRule
{
    public RuleResult Evaluate(LoanApplication application) => application.CreditScore switch
    {
        >= 650 => new(nameof(CreditScoreRule), RuleOutcome.Pass, "Good credit score."),
        >= 550 => new(nameof(CreditScoreRule), RuleOutcome.Refer, "Borderline credit score, manual review needed."),
        _ => new(nameof(CreditScoreRule), RuleOutcome.Fail, "Credit score too low.")
    };
}