namespace LoanWorkflow.Core.Rules;

public record RuleResult(string RuleName, RuleOutcome Outcome, string Message);