using MauiCtrl;
using MortgageCalculator.Classes;
using System.Text.Json;

namespace MortgageCalculator.Pages;

public partial class ResultPage : ContentPage
{
	public ResultPage()
	{
		InitializeComponent();
		BindingContext = new VM.VM_StatusName();
        
		LabelLoanPrice.Text = ClsCommon.LoanStatus.LoanPrice.ToString();
		LabelInterestRate.Text = ClsCommon.LoanStatus.InterestRate.ToString();
        LabelYearOfRepayment.Text = ClsCommon.LoanStatus.YearsOfRepayment.ToString();
        LabelLoanType.Text = ClsCommon.LoanText[ClsCommon.LoanStatus.RepaymentType];
        LabelSaving.Text = ClsCommon.LoanStatus.Saving.ToString();
        LabelAgeA.Text = ClsCommon.LoanStatus.AgeA.ToString();
        LabelAgeB.Text = ClsCommon.LoanStatus.AgeB.ToString();
        LabelAgeC.Text = ClsCommon.LoanStatus.AgeC.ToString();
    }

    //*******************************************************************
    private List<string> CalLoan(bool is_interest)
    {
        List<string> lstResult = new();
        ClsValue clsValue = new();
        int LoopNum = 50;

        Func<double, double, int, double> fnCalAmount_Interest = (debt, interest, year) =>
        {
            double a = debt * (interest / 100) / 12 * Math.Pow((1 + (interest / 100) / 12), (year * 12));
            double b = Math.Pow((1 + (interest / 100) / 12), (year * 12));
            return a / b;
        };

        for

        clsValue.RemainingDebt = ClsCommon.LoanStatus.LoanPrice;
        clsValue.Saving += ClsCommon.LoanStatus.Saving;
        if (is_interest)
        {
            clsValue.RepaymentInterest = clsValue.RemainingDebt * ClsCommon.LoanStatus.InterestRate / 100;
            clsValue.RepaymentAmount =
                fnCalAmount_Interest(ClsCommon.LoanStatus.LoanPrice, 
                                     ClsCommon.LoanStatus.InterestRate,
                                     ClsCommon.LoanStatus.YearsOfRepayment);
            clsValue.RepaymentPrincipal = clsValue.RepaymentAmount - clsValue.RepaymentInterest;
        }
        else
        {
            clsValue.RepaymentInterest = clsValue.RemainingDebt * ClsCommon.LoanStatus.InterestRate / 100 / 12;
            clsValue.RepaymentPrincipal = clsValue.RemainingDebt / (ClsCommon.LoanStatus.YearsOfRepayment * 12);
            clsValue.RepaymentAmount = clsValue.RepaymentInterest + clsValue.RepaymentPrincipal;
        }

        var jsonBuf = JsonSerializer.Serialize(clsValue);
        lstResult.Add(jsonBuf);
        return lstResult;
    }

    //*******************************************************************
    private void ClickedDebug(object sender, EventArgs e)
    {
        var resultBuf = CalLoan(false);

        foreach (string st in resultBuf)
            ClsDebug.DebugWriteLine(st);
    }
}