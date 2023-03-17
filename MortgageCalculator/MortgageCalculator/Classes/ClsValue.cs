using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
    class ClsValue
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public double RemainingDebt { get; set; }      //残債
        public double RepaymentInterest { get; set; }  //利息
        public double RepaymentPrincipal { get; set; } //返済元金
        public double RepaymentAmount { get; set; }    //返済額
        public double Saving { get; set; }              //貯金

        public int AgeA { get; set; }
        public int AgeB { get; set; }
        public int AgeC { get; set; }

        public ClsValue()
        {
            RemainingDebt = 0;
            RepaymentInterest = 0;
            RepaymentPrincipal = 0;
            RepaymentAmount = 0;
            Saving = 0;

            Year = 0;
            Month = 0;

            AgeA = 0; AgeB = 0; AgeC = 0;
        }
    }
}
