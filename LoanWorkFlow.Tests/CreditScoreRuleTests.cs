using LoanWorkflow.Core.Entities;
using LoanWorkflow.Core.Rules;

namespace LoanWorkflow.Tests;

public class CreditScoreRuleTests
{
    [Theory]
    [InlineData(720, RuleOutcome.Pass)]
    [InlineData(650, RuleOutcome.Pass)]
    [InlineData(600, RuleOutcome.Refer)]
    [InlineData(550, RuleOutcome.Refer)]
    [InlineData(400, RuleOutcome.Fail)]
    public void Evaluate_ReturnsExpectedOutcome(int score, RuleOutcome expected)
    {
        var app = new LoanApplication { CreditScore = score };
        var result = new CreditScoreRule().Evaluate(app);
        Assert.Equal(expected, result.Outcome);
    }
}