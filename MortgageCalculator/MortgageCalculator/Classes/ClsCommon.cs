using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
    //==============================================================================================
    //==============================================================================================
    //==============================================================================================
    internal class ClsCommon
    {
        public static readonly string CURRENT_DIRECTRY = AppDomain.CurrentDomain.BaseDirectory;
#if IOS
        // iOSの場合
        public static string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string localAppData = Path.Combine(documents, "..", "Library");
#elif ANDROID
        // Androidの場合
        public static readonly string LOCAL_APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif

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

        //*******************************************************************
        public void Init()
        {
        }

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
        public int? AgeA;
        public int? AgeB;
        public int? AgeC;

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
