using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Pages.VM
{
    public class VM_StatusName
    {

        public string LOAN_PRICE {get;} = "借入価格";
        public string INTEREST_RATE { get; } = "金利";
        public string YEAR_OF_REPAYMENT { get; } = "返済年数";
        public string LOAN_TYPE { get; } = "返済タイプ";
        public string SAVING { get; } = "貯金月額";
        public string AGE_A { get; } = "開始年齢 A";
        public string AGE_B { get; } = "開始年齢 B";
        public string AGE_C { get; } = "開始年齢 C";

        public VM_StatusName()
        {

        }
    }
}
