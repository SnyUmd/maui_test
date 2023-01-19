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
//using AndroidX.Emoji2.Text.FlatBuffer;

public partial class MainPage : ContentPage
{
	int count = 0;
    WebClient wc = new WebClient();
    HtmlCtrl HC = new HtmlCtrl();
    ClsCommon common = new ClsCommon();
    TextCtrl tc = new TextCtrl();

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
            //this.WidthRequest = 400;    
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
    private string accessHtml(string str_url)
    {
        while (common.blAccessing) ;
        common.blAccessing = true;
        System.Threading.Thread.Sleep(100);
        string val = HC.getHtml(wc, str_url);
        writeLog($"Tx : {str_url}\n");
        writeLog($"Rx : {val}\n\n");
        common.blAccessing = false;
        return val;
    }

    //************************************************************************************
    private void TextChange_entInterval(object sender, TextChangedEventArgs e)
    {
        entInterval.Text = tc.extractNum_string(entInterval.Text);
    }


    //************************************************************************************
    //************************************************************************************
    //************************************************************************************
    private void OnCounterClicked_btnTemp(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.temp]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.temp]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
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
    private async void OnCounterClicked_btnDebug(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnBoolean.Text == "Food") strArea = "f";
        string strUrl = "http://petoasis.airport/get" + $"?target={strArea}&item=all";
        while (common.blAccessing) ;
        accessHtml(strUrl);
    }


    //************************************************************************************
    private void OnCounterClicked_btnSetInterval10(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.set_interval10]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.set_interval10]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnSetInterval5(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.set_interval5]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.set_interval5]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnSetInterval20(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.set_interval20]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.set_interval20]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnSetInterval0(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = common.aryHtml[(int)enmHtml.set] + $"?target={strArea}&interval=0";

        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

    //************************************************************************************
    private void OnCounterClicked_btnAdjustW(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.adjust_w]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.adjust_w]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
    }

    //************************************************************************************
    private void OnCounterClicked_btnAdjustF(object sender, EventArgs e)
    {
        //while (common.blAccessing) ;
        //common.blAccessing = true;
        //System.Threading.Thread.Sleep(100);
        //writeLog($"Tx : {common.aryHtml[(int)enmHtml.adjust_f]}\n");
        //string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.adjust_f]);
        //writeLog($"Rx : {val}\n\n");
        //common.blAccessing = false;
    }

    //************************************************************************************
    private void Clicked_btnArea(object sender, EventArgs e)
    {
        if (btnArea.Text == "Water")
        {
            btnArea.Text = "Food";
            btnArea.BackgroundColor = Colors.Brown;
        }
        else if (btnArea.Text == "Food")
        {
            btnArea.Text = "Water";
            btnArea.BackgroundColor = Colors.MediumBlue;

        }
    }

    //************************************************************************************
    private void Clicked_btnNow(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = common.aryHtml[(int)enmHtml.now] + $"?target={strArea}";

        while (common.blAccessing) ;
        accessHtml(strUrl);
    }




    //************************************************************************************
    private async void Click_btnSetInterval(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = common.aryHtml[(int)enmHtml.set] + $"?target={strArea}&interval={entInterval.Text}";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing);
        accessHtml(strUrl);
    }

//************************************************************************************
    private void Click_btnAdjust(object sender, EventArgs e)
    {
        string strArea = "w";
        string strDirection = "r";
        string motion = "5";
        if (btnArea.Text == "Food") strArea = "f";
        if (btnDirection.Text == "Left") strDirection = "l";
        motion = entInterval.Text;
        string strUrl = common.aryHtml[(int)enmHtml.adjust] + $"?target={strArea}&direction={strDirection}&motion={motion}";

        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

//************************************************************************************
    private void Click_btnDirection(object sender, EventArgs e)
    {
        if (btnDirection.Text == "Right")
        {
            btnDirection.Text = "Left";
            btnDirection.BackgroundColor = Colors.OrangeRed;
        }
        else if (btnDirection.Text == "Left")
        {
            btnDirection.Text = "Right";
            btnDirection.BackgroundColor = Colors.Orange;

        }
    }

//************************************************************************************
    private void Click_btnBoolean(object sender, EventArgs e)
    {
        if (btnBoolean.Text == "false")
        {
            btnBoolean.Text = "true";
            btnBoolean.BackgroundColor = Colors.MediumVioletRed;
        }
        else if (btnBoolean.Text == "true")
        {
            btnBoolean.Text = "false";
            btnBoolean.BackgroundColor = Colors.SteelBlue;

        }
    }

//************************************************************************************
    private void Clicked_btnGetMelody(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = "http://petoasis.airport/get"+ $"?target={strArea}&item=melody";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

//************************************************************************************
    private void Clicked_btnGetInterval(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = "http://petoasis.airport/get" + $"?target={strArea}&item=interval";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

//************************************************************************************
    private void Clicked_btnSetRing(object sender, EventArgs e)
    {
        string strArea = "w";
        string strVal = "false";
        if (btnArea.Text == "Food") strArea = "f";
        if (btnBoolean.Text == "true") strVal = "true";
        string strUrl = common.aryHtml[(int)enmHtml.set] + $"?target={strArea}&ring={strVal}";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

//************************************************************************************
    private void Clicked_btnGetRing(object sender, EventArgs e)
    {
        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = "http://petoasis.airport/get" + $"?target={strArea}&item=ring";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing) ;
        accessHtml(strUrl);

    }

//************************************************************************************
    private void Clicked_btnGetNext(object sender, EventArgs e)
    {

        string strArea = "w";
        if (btnArea.Text == "Food") strArea = "f";
        string strUrl = "http://petoasis.airport/get" + $"?target={strArea}&item=next";
        //await DisplayAlert("", strUrl, "OK");
        while (common.blAccessing) ;
        accessHtml(strUrl);
    }

//************************************************************************************
    private void OnCounterClicked_btnLogClear(object sender, EventArgs e)
    {
        lblValue.Text = "";
    }

//************************************************************************************
    private void OnCounterClicked_btnLogCopy(object sender, EventArgs e)
    {
        Clipboard.SetTextAsync(lblValue.Text);
    }
}



//************************************************************************************



