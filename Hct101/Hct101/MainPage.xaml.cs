//#define INTERVAL

using Microsoft.Maui.Hosting;

namespace Hct101;

using System;
using System.Net;
using MauiCtrl;

using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using taskTimers = System.Timers;

public partial class MainPage : ContentPage
{
	int count = 0;
    WebClient wc = new WebClient();
    HtmlCtrl HC = new HtmlCtrl();
    ClsCommon common = new ClsCommon();

    public enum enmTest
    {
        test0,
        test1
    };

    public MainPage()
	{
		InitializeComponent();

        if(DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            this.WidthRequest = 400;
        }

        common.current_time = DateTime.Now;
#if INTERVAL
        Task.Run(() => runTask(this));
#endif
    }

    void writeLog(string val)
    {
        lblValue.Text += val;
        ToolCtrl TC = new ToolCtrl();
        TC.scroll_ScrollView(svLog, 0, lblValue.Height, true);
    }

    async void runTask(Page pg)
    {
        common.current_time = DateTime.Now;
        var intervalTime = new TimeSpan(0, 0, 0, 10);
        string val = "";

        while (true)
        {
            if (DateTime.Now - common.current_time > intervalTime)
            {
                common.current_time = DateTime.Now;

                while (common.blAccessing) ;
                common.blAccessing = true;
                System.Threading.Thread.Sleep(100);
                val = $"{common.current_time.ToString()}\n";

                val += HC.getHtml(wc, common.aryHtml[(int)enmHtml.temp]) + "\n";
                val += HC.getHtml(wc, common.aryHtml[(int)enmHtml.humd]) + "\n";

                common.intervalLog += $"{val}\n";
                common.blAccessing = false;
            }
        }
    }

    void timeAction(object state)
    {
        
    }
 

    //************************************************************************************
    private void OnCounterClicked_btnTime(object sender, EventArgs e)
    {
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        writeLog($"Tx : {common.aryHtml[(int)enmHtml.time]}\n");
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.time]);
        writeLog($"Rx : {val}\n\n");
        common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnTemp(object sender, EventArgs e)
    {
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        writeLog($"Tx : {common.aryHtml[(int)enmHtml.temp]}\n");
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.temp]);
        writeLog($"Rx : {val}\n\n");
        common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnHumd(object sender, EventArgs e)
    {
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        writeLog($"Tx : {common.aryHtml[(int)enmHtml.humd]}\n");
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.humd]);
        writeLog($"Rx : {val}\n\n");
        common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnMotor(object sender, EventArgs e)
    {
        int motorHttpNum;
        string log;
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        if (common.motorOn)
        {
            motorHttpNum = (int)enmHtml.motor_off;
            common.motorOn = false;
            btnMotor.BackgroundColor = Colors.Gray;
            btnMotor.TextColor = Colors.White;
        }
        else
        {
            motorHttpNum = (int)enmHtml.motor_on;
            common.motorOn = true;
            btnMotor.BackgroundColor = Colors.Yellow;
            btnMotor.TextColor = Colors.Black;
        }

        writeLog($"Tx : {common.aryHtml[motorHttpNum]}\n");
        string val = HC.getHtml(wc, common.aryHtml[motorHttpNum]);
        writeLog($"Rx : {val}\n\n");

        common.blAccessing = false;
    }


    private void OnCounterClicked_btnBuzzer(object sender, EventArgs e)
    {
        int buzzerHttpNum;
        string log;
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        if (common.buzzerOn)
        {
            buzzerHttpNum = (int)enmHtml.buzzer_off;
            common.buzzerOn = false;
            btnBuzzer.BackgroundColor = Colors.Gray;
            btnBuzzer.TextColor = Colors.White;
        }
        else
        {
            buzzerHttpNum = (int)enmHtml.buzzer_on;
            common.buzzerOn = true;
            btnBuzzer.BackgroundColor = Colors.Yellow;
            btnBuzzer.TextColor = Colors.Black;
        }

        writeLog($"Tx : {common.aryHtml[buzzerHttpNum]}\n");
        string val = HC.getHtml(wc, common.aryHtml[buzzerHttpNum]);
        writeLog($"Rx : {val}\n\n");

        common.blAccessing = false;
    }


    //************************************************************************************
    private async void OnCounterClicked_btnDebug(object sender, EventArgs e)
    {
        //common.blAccessing = !common.blAccessing;

        //if (common.blAccessing) btnDebug.BackgroundColor = Colors.Yellow;
        //else btnDebug.BackgroundColor = Colors.Gray;
        lblValue.Text = "";
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        writeLog($"Tx : {common.aryHtml[(int)enmHtml.a_file]}\n");
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.a_file]);
        writeLog($"Rx : {val}\n\n");
        common.blAccessing = false;
    }


}




