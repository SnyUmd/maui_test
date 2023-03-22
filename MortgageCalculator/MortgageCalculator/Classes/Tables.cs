using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
    public enum EnmTable_num
    {
        tbl_saved_status = 0,
        tbl_history_status
    }

    internal class Tables
    {


        public static string[] tables_name = new string[]
        {
            "tbl_saved_status",
            "tbl_history_status"
        };

        public static string[] tbl_saved_status = new string[]
        {
            "id",
            "status_name",
            "loan_price",
            "interest_rate",
            "years_of_repayment",
            "loan_type",
            "age_a",
            "age_b",
            "age_c"
        };

        public static string[] tbl_history_status = new string[]
        {
            "id",
            "loan_price",
            "interest_rate",
            "years_of_repayment",
            "loan_type",
            "age_a",
            "age_b",
            "age_c",
            "used_date",
            "used_time"
        };
    }
}
