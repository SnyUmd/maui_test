namespace Hct101;

using System;
using System.Net;
using MauiCtrl;

public partial class MainPage : ContentPage
{
	int count = 0;
    WebClient wc = new WebClient();
    HtmlCtrl HC = new HtmlCtrl();
    ClsCommon common = new ClsCommon();

    public MainPage()
	{
		InitializeComponent();

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        lblDebug.Text = DeviceInfo.Current.Platform.ToString();
        if(DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            this.WidthRequest = 400;
        }
    }

    void writeLog(string val)
    {
        lblValue.Text += val;
        ToolCtrl TC = new ToolCtrl();
        TC.scroll_ScrollView(svLog, 0, lblValue.Height, true);
    }

    //************************************************************************************
    private void OnCounterClicked_btnTime(object sender, EventArgs e)
    {
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.time]);
        writeLog($"Time : {val}\n");
    }

    //************************************************************************************
    private void OnCounterClicked_btnTemp(object sender, EventArgs e)
    {
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.temp]);
        writeLog($"Temp : {val}\n");
    }

    //************************************************************************************
    private void OnCounterClicked_btnHumd(object sender, EventArgs e)
    {
        string val = HC.getHtml(wc, common.aryHtml[(int)enmHtml.humd]);
        writeLog($"Humd : {val}\n");
    }
}

