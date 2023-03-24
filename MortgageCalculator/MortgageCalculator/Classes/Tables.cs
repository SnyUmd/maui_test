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
            "saving",
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
            "saving",
            "age_a",
            "age_b",
            "age_c",
            "used_date",
            "used_time"
        };


        public struct CreateTablesString
        {
            public string[] tbl_saved_status = new string[]
            {
                "id integer primary key",
                "status_name text",
                "loan_price int",
                "interest_rate real",
                "years_of_repayment int",
                "loan_type int",
                "saving",
                "age_a int default null",
                "age_b int default null",
                "age_c int default null"
            };

            public string[] tbl_history_status = new string[]
            {
                "id integer primary key",
                "loan_price int",
                "interest_rate real",
                "years_of_repayment int",
                "loan_type int",
                "saving",
                "age_a int default null",
                "age_b int default null",
                "age_c int default null",
                "used_date text",
                "used_time text"
            };

            public CreateTablesString()
            {

            }
            
        };
        public static CreateTablesString CreateTableStr = new();

    }


}
