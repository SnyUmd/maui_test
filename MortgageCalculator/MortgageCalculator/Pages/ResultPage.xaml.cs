using MauiCtrl;
using MortgageCalculator.Classes;
using MortgageCalculator.Pages.VM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace MortgageCalculator.Pages;

public partial class ResultPage : ContentPage
{

    ResultPageVM vmResultPage = new ResultPageVM();
    public ResultPage()
	{
		InitializeComponent();

        LabelLoanPrice.Text = ClsCommon.LoanStatus.LoanPrice.ToString();
		LabelInterestRate.Text = ClsCommon.LoanStatus.InterestRate.ToString();
        LabelYearOfRepayment.Text = ClsCommon.LoanStatus.YearsOfRepayment.ToString();
        LabelLoanType.Text = ClsCommon.LoanText[ClsCommon.LoanStatus.RepaymentType];
        LabelSaving.Text = ClsCommon.LoanStatus.Saving.ToString();
        LabelAgeA.Text = ClsCommon.LoanStatus.AgeA.ToString();
        LabelAgeB.Text = ClsCommon.LoanStatus.AgeB.ToString();
        LabelAgeC.Text = ClsCommon.LoanStatus.AgeC.ToString();

        //標準ナビバー非表示
        NavigationPage.SetHasNavigationBar(this, false);
		BindingContext = vmResultPage;

        updateCorectionView();
    }

    //*******************************************************************
    private List<ClsValue> CalLoan_Values(bool is_repayment_amount)
    {
        List<ClsValue> lstResult = new();
        ClsValue clsValue = new();
        //int LoopNum = 50 * 12;
        int LoopNum = ClsCommon.LoanStatus.YearsOfRepayment * 12 + 1;
        double previous_month_repayment = 0;


        Func<double, double, int, double> fnCalAmount_Interest = (debt, interest, year) =>
        {
            double a = debt * (interest / 100) / 12 * Math.Pow(1 + (interest / 100) / 12, year * 12);
            double b = Math.Pow(1 + (interest / 100) / 12, year * 12) - 1;
            return a / b;
        };

        clsValue.RemainingDebt = ClsCommon.LoanStatus.LoanPrice;//残債初期値

        if (is_repayment_amount)
            clsValue.RepaymentAmount =
                    Math.Round(fnCalAmount_Interest(ClsCommon.LoanStatus.LoanPrice,
                                         ClsCommon.LoanStatus.InterestRate,
                                         ClsCommon.LoanStatus.YearsOfRepayment), 4);
        else
            clsValue.RepaymentPrincipal =
                Math.Round(ClsCommon.LoanStatus.LoanPrice / (ClsCommon.LoanStatus.YearsOfRepayment * 12), 4);


        for (int i = 0; i < LoopNum; i++)
        {
            clsValue.RemainingDebt = Math.Round(clsValue.RemainingDebt - previous_month_repayment, 4);//前月分を引く
            clsValue.Saving = Math.Round(clsValue.Saving + ClsCommon.LoanStatus.Saving, 4);
            if (is_repayment_amount)
            {
                clsValue.RepaymentInterest = Math.Round(clsValue.RemainingDebt * (ClsCommon.LoanStatus.InterestRate / 100 / 12), 4);
                clsValue.RepaymentPrincipal = Math.Round(clsValue.RepaymentAmount - clsValue.RepaymentInterest, 4);
            }
            else
            {
                clsValue.RepaymentInterest = Math.Round(clsValue.RemainingDebt * ClsCommon.LoanStatus.InterestRate / 100 / 12, 4);
                clsValue.RepaymentAmount = Math.Round(clsValue.RepaymentInterest + clsValue.RepaymentPrincipal, 4);
            }

            //経過
            clsValue.Month = (i + 1) % 12;

            if (clsValue.Month == 0)
                clsValue.Month = 12;
            if (clsValue.Month == 1 && i != 0)
                clsValue.Year++;

            //年齢
            if (ClsCommon.LoanStatus.AgeA != 0)
                clsValue.AgeA = ClsCommon.LoanStatus.AgeA + clsValue.Year;
            if (ClsCommon.LoanStatus.AgeB != 0)
                clsValue.AgeB = ClsCommon.LoanStatus.AgeB + clsValue.Year;
            if (ClsCommon.LoanStatus.AgeC != 0)
                clsValue.AgeC = ClsCommon.LoanStatus.AgeC + clsValue.Year;

            lstResult.Add( new ClsValue(clsValue));
            previous_month_repayment = clsValue.RepaymentPrincipal;//先月分の元金返済をセット
        }

        //小数点を任意の位置で丸める(銀行丸め)
        //Math.Round(1.15, 1);

        return lstResult;
    }

    //*******************************************************************
    private void updateCorectionView()
    {
        int y = 0;
        bool isRepayment = false;

        if (ClsCommon.LoanStatus.RepaymentType == 0) isRepayment = true;
        var resultBuf = CalLoan_Values(isRepayment);

        Debug.WriteLine("-------------------");
        foreach (var val in resultBuf)
        {
            //if ((val.Year == 0 && val.Month == 1) || y != val.Year)
            if (val.Year != 0 && val.Month == 1)
                {
                vmResultPage.SetValueContextView(val);
                y = val.Year;
            }
        }
    }

    //*******************************************************************
    private void ClickedDebug(object sender, EventArgs e)
    {
        //int y = 0;
        //var resultBuf = CalLoan_Values(true);

        //Debug.WriteLine("-------------------");
        //foreach (var val in resultBuf)
        //{
        //    if ((val.Year == 0 && val.Month == 1) || y != val.Year)
        //    {
        //        vmResultPage.SetValueContextView(val);
        //        y = val.Year;
        //    }
        //}
        //Debug.WriteLine("-------------------");
        Navigation.PopAsync(true);
    }
}