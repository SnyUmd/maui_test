using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
    public class ClsValue
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int AgeA { get; set; }
        public int AgeB { get; set; }
        public int AgeC { get; set; }

        public double RemainingDebt { get; set; }      //残債
        public double RepaymentInterest { get; set; }  //利息
        public double RepaymentPrincipal { get; set; } //返済元金
        public double RepaymentAmount { get; set; }    //返済額
        public double Saving { get; set; }              //貯金



        public double Price { get; set; }   //借入価格
        public double Interest { get; set; }   //利息
        public double YearOfRepayment { get; set; }   //借入価格
        public double Type { get; set; }   //借入価格




        public ClsValue(ClsValue cv = null)
        {
            if (cv != null)
            {
                RemainingDebt = cv.RemainingDebt;
                RepaymentInterest = cv.RepaymentInterest;
                RepaymentPrincipal = cv.RepaymentPrincipal;
                RepaymentAmount = cv.RepaymentAmount;
                Saving = cv.Saving;

                Year = cv.Year;
                Month = cv.Month;

                AgeA = cv.AgeA; AgeB = cv.AgeB; AgeC = cv.AgeC;

                Price = cv.Price;
                Interest = cv.Interest;
                YearOfRepayment = cv.YearOfRepayment;
                Type = cv.Type;
            }
        }
    }
}
