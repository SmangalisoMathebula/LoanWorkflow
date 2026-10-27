using LoanWorkflow.Core.Entities;
using LoanWorkflow.Core.Rules;

namespace LoanWorkflow.Tests;

public class LoanDecisionEngineTests
{
    private class FakeRule : ILoanRule
    {
        private readonly RuleOutcome _outcome;
        public FakeRule(RuleOutcome outcome) => _outcome = outcome;
        public RuleResult Evaluate(LoanApplication application) =>
            new("Fake", _outcome, "test");
    }

    [Fact]
    public void Evaluate_AnyFail_OverallFail()
    {
        var engine = new LoanDecisionEngine(new[]
            { new FakeRule(RuleOutcome.Pass), new FakeRule(RuleOutcome.Refer), new FakeRule(RuleOutcome.Fail) });

        Assert.Equal(RuleOutcome.Fail, engine.Evaluate(new LoanApplication()).Overall);
    }

    [Fact]
    public void Evaluate_ReferWithoutFail_OverallRefer()
    {
        var engine = new LoanDecisionEngine(new[]
            { new FakeRule(RuleOutcome.Pass), new FakeRule(RuleOutcome.Refer) });

        Assert.Equal(RuleOutcome.Refer, engine.Evaluate(new LoanApplication()).Overall);
    }

    [Fact]
    public void Evaluate_AllPass_OverallPass()
    {
        var engine = new LoanDecisionEngine(new[]
            { new FakeRule(RuleOutcome.Pass), new FakeRule(RuleOutcome.Pass) });

        var (overall, results) = engine.Evaluate(new LoanApplication());

        Assert.Equal(RuleOutcome.Pass, overall);
        Assert.Equal(2, results.Count);
    }
}