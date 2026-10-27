using LoanWorkflow.Core.Entities;
using LoanWorkflow.Core.Rules;

namespace LoanWorkflow.Tests;

public class AffordabilityRuleTests
{
    private static LoanApplication MakeApp(decimal income, decimal expenses, decimal debt,
                                           decimal amount, int months) => new()
                                           {
                                               MonthlyIncome = income,
                                               MonthlyExpenses = expenses,
                                               ExistingDebtPayments = debt,
                                               RequestedAmount = amount,
                                               TermMonths = months
                                           };

    [Fact]
    public void Evaluate_AffordableLoan_Passes()
    {
        var app = MakeApp(30000m, 12000m, 2000m, 20000m, 24);
        Assert.Equal(RuleOutcome.Pass, new AffordabilityRule().Evaluate(app).Outcome);
    }

    [Fact]
    public void Evaluate_RepaymentTooHigh_Fails()
    {
        var app = MakeApp(12000m, 8000m, 2000m, 100000m, 12);
        Assert.Equal(RuleOutcome.Fail, new AffordabilityRule().Evaluate(app).Outcome);
    }

    [Fact]
    public void Evaluate_NoDisposableIncome_Fails()
    {
        var app = MakeApp(10000m, 9000m, 2000m, 5000m, 12);
        Assert.Equal(RuleOutcome.Fail, new AffordabilityRule().Evaluate(app).Outcome);
    }
}