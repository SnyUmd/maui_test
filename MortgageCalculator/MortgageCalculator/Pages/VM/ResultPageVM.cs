using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MortgageCalculator.Classes;

namespace MortgageCalculator.Pages.VM
{

    public class ResultPageVM
    {
        public ObservableCollection<ClsValue> Values { get; set; } = new ObservableCollection<ClsValue>();

        public string LOAN_PRICE {get;} = "借入価格";
        public string INTEREST_RATE { get; } = "金利";
        public string YEAR_OF_REPAYMENT1 { get; } = "返済年数";
        public string PROGRESS_YEAR { get; } = "返済経過年数";
        public string LOAN_TYPE { get; } = "返済タイプ";
        public string SAVING { get; } = "貯金月額";
        public string AGE_A { get; } = "開始年齢 A";
        public string AGE_B { get; } = "開始年齢 B";
        public string AGE_C { get; } = "開始年齢 C";

        public ResultPageVM()
        {
            //Values.Add(new ClsValue
            //{
            //    //Year = 0,
            //    //Month = 0,
            //    //RemainingDebt = 0,
            //    //RepaymentInterest = 0,
            //    //RepaymentPrincipal = 0,
            //    //RepaymentAmount = 0,
            //    //Saving = 0,
            //    //AgeA = 0,
            //    //AgeB = 0,
            //    //AgeC = 0
            //});
        }

        public void SetValueContextView(ClsValue cls_val)
        {
            Values.Add(new ClsValue(cls_val));
        }
    }
}
