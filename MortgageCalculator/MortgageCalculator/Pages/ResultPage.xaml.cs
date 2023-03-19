using MauiCtrl;
using MortgageCalculator.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace MortgageCalculator.Pages;

public class ResultPageVM: ContentPage
{
    public ObservableCollection<ClsValue> Values { get; set; } = new ObservableCollection<ClsValue>();

    public ResultPageVM()
    {
        Values.Add(new ClsValue
        {
            Year = 0,
            Month =0,
            RemainingDebt = 0,
            RepaymentInterest = 0,
            RepaymentPrincipal = 0,
            RepaymentAmount = 0,
            Saving = 0,
            AgeA = 0,
            AgeB = 0,
            AgeC = 0
        }) ;
    }
    public static void SetValueContextView()
    {

    }
}

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

        BindingContext = new ResultPageVM();

    }

    //*******************************************************************
    private List<string> CalLoan(bool is_repayment_amount)
    {
        List<string> lstResult = new();
        ClsValue clsValue = new();
        int LoopNum = 50 * 12;
        double previous_month_repayment = 0;

        Func<double, double, int, double> fnCalAmount_Interest = (debt, interest, year) =>
        {
            double a = debt * (interest / 100) / 12 * Math.Pow(1 + (interest / 100) / 12, year * 12);
            double b = Math.Pow(1 + (interest / 100) / 12, year * 12) - 1;
            return a / b;
        };

        clsValue.RemainingDebt = ClsCommon.LoanStatus.LoanPrice;//残債初期値

        if(is_repayment_amount)
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

            previous_month_repayment = clsValue.RepaymentPrincipal;//先月分の元金返済をセット
            var jsonBuf = JsonSerializer.Serialize(clsValue);
            lstResult.Add(jsonBuf);
        }

        //小数点を任意の位置で丸める(銀行丸め)
        //Math.Round(1.15, 1);

        return lstResult;
    }

    //*******************************************************************
    private void ClickedDebug(object sender, EventArgs e)
    {
        var resultBuf = CalLoan(true);

        Debug.WriteLine("-------------------");
        foreach (string st in resultBuf)
            Debug.WriteLine(st);
        Debug.WriteLine("-------------------");

    }
}