using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Classes
{
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
        //#endif

        public static string[] ImageDataSources =
        {
            "equal_principal_and_interest.png",
            "equal_amount_of_principal.png"
        };

    }

}
