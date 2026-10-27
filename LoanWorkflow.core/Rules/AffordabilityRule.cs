using LoanWorkflow.Core.Entities;
using LoanWorkflow.Core.Services;

namespace LoanWorkflow.Core.Rules;

public class AffordabilityRule : ILoanRule
{
    private readonly decimal _annualRate;
    private readonly decimal _maxRepaymentShare;

    public AffordabilityRule(decimal annualRate = 0.12m, decimal maxRepaymentShare = 0.5m)
    {
        _annualRate = annualRate;
        _maxRepaymentShare = maxRepaymentShare;
    }

    public RuleResult Evaluate(LoanApplication application)
    {
        var disposable = application.MonthlyIncome
                       - application.MonthlyExpenses
                       - application.ExistingDebtPayments;

        if (disposable <= 0)
            return new(nameof(AffordabilityRule), RuleOutcome.Fail, "No disposable income.");

        var repayment = LoanCalculator.MonthlyRepayment(
            application.RequestedAmount, _annualRate, application.TermMonths);

        return repayment <= disposable * _maxRepaymentShare
            ? new(nameof(AffordabilityRule), RuleOutcome.Pass, "Repayment is affordable.")
            : new(nameof(AffordabilityRule), RuleOutcome.Fail,
                  $"Repayment of {repayment:N2} exceeds {_maxRepaymentShare:P0} of disposable income.");
    }
}