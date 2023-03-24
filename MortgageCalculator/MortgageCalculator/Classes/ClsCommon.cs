using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MortgageCalculator.Classes;
using MauiCtrl;
using System.Text.Json.Nodes;

namespace MortgageCalculator.Classes
{
    //==============================================================================================
    //==============================================================================================
    //==============================================================================================
    internal class ClsCommon
    {
        public static string CurrentDir = AppDomain.CurrentDomain.BaseDirectory;

#if ANDROID
        public static string DirSeparator = "/";
#else
        public static string DirSeparator = "\\";
#endif
        public static string DbFileName = "mCalc";

        public static string DbFilePath = CurrentDir + DirSeparator + DbFileName;

        public static ClsStatus LoanStatus = new();

        public static string[] ImageDataSources =
        {
            "equal_principal_and_interest.png",
            "equal_amount_of_principal.png"
        };
        public static string[] LoanText =
        {
            "元利均等返済", 
            "元金均等返済"
        };

        //class Tablesの情報---------------------------------
        public static string[] TablesName = Tables.tables_name;
        //public static Dictionary<string, bool> TblSavedStatucs = Tables.tbl_saved_status;
        //public static Dictionary<string, bool> TblesHistoryStatus = Tables.tbl_history_status;


        public static int GetRecordNum(string table_name)
        {
            string puery = $"SELECT COUNT(*) FROM {table_name}";
            List<string> records = SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, puery);
            JsonNode jn = JsonNode.Parse(records[0]);
            int result = int.Parse(jn["COUNT(*)"].ToString());
            return result;
        }

    }


    //==============================================================================================
    //==============================================================================================
    //==============================================================================================
    public class ClsStatus
    {
        public double LoanPrice { get; set; }       //借入価格
        public double InterestRate { get; set; }    //金利
        public int YearsOfRepayment { get; set; }//返済年数
        public int RepaymentType { get; set; }   //返済タイプ
        public double Saving { get; set; }          //貯金
        public int AgeA { get; set; }
        public int AgeB { get; set; }
        public int AgeC { get; set; }

        public string Num { get; set; } = "";

        public ClsStatus(ClsStatus cs = null)
        {
            if (cs != null)
            {
                LoanPrice = cs.LoanPrice;
                InterestRate = cs.InterestRate;
                YearsOfRepayment = cs.YearsOfRepayment;
                RepaymentType = cs.RepaymentType;
                Saving = cs.Saving;
                AgeA = cs.AgeA;
                AgeB = cs.AgeB;
                AgeC = cs.AgeC;
                Num = cs.Num;
            }
        }


    }
}
