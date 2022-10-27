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
        motor_on,
        motor_off,
        buzzer_on,
        buzzer_off,
        //led_r,
        //led_g,
        all
    };
    //internal class clsCommon : ContentPage
    public class ClsCommon
    {
        public bool taskSts = false;
        public DateTime current_time;
        public string intervalLog = "";

        public bool motorOn = false;
        public bool buzzerOn = false;

        public bool blAccessing = false;

        public string[] aryHtml =
        {
            "http://esp32.airport/get?sts=time",
            "http://esp32.airport/get?sts=temperture",
            "http://esp32.airport/get?sts=humidity",
            "http://esp32.airport/motor?sts=on",
            "http://esp32.airport/motor?sts=off",
            "http://esp32.airport/buzzer?sts=on",
            "http://esp32.airport/buzzer?sts=off",
            "http://esp32.airport/get?sts=all"
        };
    }
}
