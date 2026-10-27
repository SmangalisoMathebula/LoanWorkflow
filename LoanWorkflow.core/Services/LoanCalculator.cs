namespace LoanWorkflow.Core.Services;

public static class LoanCalculator
{
    public static decimal MonthlyRepayment(decimal principal, decimal annualRate, int months)
    {
        if (principal <= 0) throw new ArgumentOutOfRangeException(nameof(principal));
        if (months <= 0) throw new ArgumentOutOfRangeException(nameof(months));
        if (annualRate < 0) throw new ArgumentOutOfRangeException(nameof(annualRate));

        var monthlyRate = annualRate / 12m;
        if (monthlyRate == 0) return Math.Round(principal / months, 2);

        var factor = (decimal)Math.Pow(1 + (double)monthlyRate, months);
        return Math.Round(principal * monthlyRate * factor / (factor - 1), 2);
    }
}