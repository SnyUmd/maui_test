using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hct101
{
    public enum enmHtml
    {
        time,
        temp,
        humd,
        all
    };
    //internal class clsCommon : ContentPage
    public class ClsCommon
    {
        public string[] aryHtml =
        {
            "http://esp32.airport/get?sts=time",
            "http://esp32.airport/get?sts=temperture",
            "http://esp32.airport/get?sts=humidity",
            "http://esp32.airport/get?sts=all"
        };

        public enum enmTest
        {
            test0,
            test1
        }
    }
}
