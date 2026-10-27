using LoanWorkflow.Core.Services;

namespace LoanWorkflow.Tests;

public class LoanCalculatorTests
{
    [Fact]
    public void MonthlyRepayment_StandardLoan_ReturnsExpectedAmount()
    {
        var result = LoanCalculator.MonthlyRepayment(10000m, 0.12m, 12);
        Assert.Equal(888.49m, result);
    }

    [Fact]
    public void MonthlyRepayment_ZeroInterest_DividesEvenly()
    {
        var result = LoanCalculator.MonthlyRepayment(12000m, 0m, 12);
        Assert.Equal(1000m, result);
    }

    [Theory]
    [InlineData(0, 12)]
    [InlineData(1000, 0)]
    public void MonthlyRepayment_InvalidInput_Throws(int principal, int months)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => LoanCalculator.MonthlyRepayment(principal, 0.12m, months));
    }
}