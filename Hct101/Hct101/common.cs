using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hct101
{
    
    //internal class clsCommon : ContentPage
    public class ClsCommon
    {
        public bool taskSts = false;
        public DateTime current_time;
        public string intervalLog = "";

        public bool motorOn = false;
        public bool buzzerOn = false;

        public bool blAccessing = false;

        //public string deviceIP = "192.168.11.96";
        public string deviceIP = "petoasis";

        public string[] aryHtml =
        {
            "http://petoasis.airport/get?item=temperture",
            "http://petoasis.airport/get?item=humidity",
            "http://petoasis.airport/get?sts=all",
            "http://petoasis.airport/set",
            "http://petoasis.airport/adjust",
            "http://petoasis.airport/now",

        };
    }
    public enum enmHtml
    {
        temp,
        humd,
        all,
        set,
        adjust,
        now,
        file
    };
}
