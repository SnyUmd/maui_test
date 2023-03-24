using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


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
            "元金均等返済",
            "元利均等返済"
        };

        //class Tablesの情報---------------------------------
        public static string[] TablesName = Tables.tables_name;
        //public static Dictionary<string, bool> TblSavedStatucs = Tables.tbl_saved_status;
        //public static Dictionary<string, bool> TblesHistoryStatus = Tables.tbl_history_status;

        

    }


    //==============================================================================================
    //==============================================================================================
    //==============================================================================================
    public class ClsStatus
    {
        public double LoanPrice;       //借入価格
        public double InterestRate;    //金利
        public int YearsOfRepayment;//返済年数
        public int RepaymentType;   //返済タイプ
        public double Saving;          //貯金
        public int AgeA;
        public int AgeB;
        public int AgeC;

        public ClsStatus()
        {
            LoanPrice = 0;
            InterestRate = 0;
            YearsOfRepayment = 0;
            RepaymentType = 0;
            Saving = 0;          //貯金
            AgeA = 0;
            AgeB = 0;
            AgeC = 0;
        }
    }
}
