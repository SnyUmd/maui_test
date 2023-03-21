using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
    class Tables
    {
        public string[] tbl_saved_status = new string[]
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

        public string[] tbl_history_status = new string[]
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
