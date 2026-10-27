using LoanWorkflow.Core.Entities;

namespace LoanWorkflow.Core.Rules;

public interface ILoanRule
{
    RuleResult Evaluate(LoanApplication application);
}