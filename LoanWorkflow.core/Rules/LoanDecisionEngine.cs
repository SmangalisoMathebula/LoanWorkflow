using LoanWorkflow.Core.Entities;

namespace LoanWorkflow.Core.Rules;

public class LoanDecisionEngine
{
    private readonly IEnumerable<ILoanRule> _rules;

    public LoanDecisionEngine(IEnumerable<ILoanRule> rules) => _rules = rules;

    public (RuleOutcome Overall, List<RuleResult> Results) Evaluate(LoanApplication application)
    {
        var results = _rules.Select(r => r.Evaluate(application)).ToList();

        var overall =
            results.Any(r => r.Outcome == RuleOutcome.Fail) ? RuleOutcome.Fail :
            results.Any(r => r.Outcome == RuleOutcome.Refer) ? RuleOutcome.Refer :
            RuleOutcome.Pass;

        return (overall, results);
    }
}