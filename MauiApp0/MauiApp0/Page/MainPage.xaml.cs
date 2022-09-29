#define Deb

namespace MauiApp0;

using MauiApp0.Page;
using MauiCtrl;
using Microsoft.VisualBasic;
using System;
using System.Net.Sockets;

using System.Threading.Tasks;


public enum enmDevice
{
    desktop = 0,
    phone,
    tablet,
    tv,
    watch
}

public partial class MainPage : ContentPage
{
    FileCtrl FC = new FileCtrl();
    MessageCtrl MC = new MessageCtrl();

	int count = 0;
    double pageWidth = 0;

    List<DeviceIdiom> device = new List<DeviceIdiom>();


	//**********************************************************************************
    public MainPage()
    {
		InitializeComponent();

        setDeviceList(ref device);
        setWidth();

    }

	//**********************************************************************************
	private async void messageView()
    {
        await DisplayAlert("Alert", "You have been alerted", "OK");

        bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        await DisplayAlert("Alert", answer.ToString(), "OK");

        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
        await DisplayAlert("Alert", action, "OK");
    }


	//**********************************************************************************
    private void addLog(string add_val, bool new_line)
    {
        this.lblLog.Text += add_val;
        if (new_line)
            this.lblLog.Text += "\n";

        ToolCtrl TC = new ToolCtrl();
        TC.scroll_ScrollView(sv_log, 0, lblLog.Height, true);
    }

	//**********************************************************************************
    private void setDeviceList(ref List<DeviceIdiom> lst)
    {
        lst.Add(DeviceIdiom.Desktop);
        lst.Add(DeviceIdiom.Phone);
        lst.Add(DeviceIdiom.Tablet);
        lst.Add(DeviceIdiom.TV);
        lst.Add(DeviceIdiom.Watch);
    }

    //**********************************************************************************
    private void setWidth()
    {
        SystemCtrl SC = new SystemCtrl();
        DeviceIdiom executionDevice = SC.PrintIdiom();

        if (executionDevice == device[(int)enmDevice.desktop] ||
            executionDevice == device[(int)enmDevice.tablet])
        {
            pageWidth = 400;
            this.WidthRequest = pageWidth;
        }
    }


    //**********************************************************************************
    private void OnCounterClicked(object sender, EventArgs e)
    {
		count++;

        CounterBtn.Text = (count == 1) ? $"Clicked {count} time" : $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
	}

	//**********************************************************************************
	private async void Click_btnPopUp(object sender, EventArgs e)
	{
        addLog("-----Click po up-----", true);
        MC.MessageAlert("0", "1", "2");

        //messageView();
        await DisplayAlert("Alert", "You have been alerted", "OK");

        bool answer = await DisplayAlert("Question?", "which ?", "Yes", "No");
        addLog($"answer : {answer.ToString()}", true);

        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
        addLog($"selected : {action}", true);
    }

	//**********************************************************************************
	private async void Click_btnFileRead(object sender, EventArgs e)
    {
        addLog("-----Click file read-----", true);
        var select_file = await FC.ReadFile(null);
        if (select_file != null)
            addLog($"Selected file : {select_file.FullPath}", true);
        else
            addLog($"Selected file : null", true);
    }

    //**********************************************************************************
    private void Click_btnOpenPage(object sender, EventArgs e)
    {
        addLog("-----Click open page-----", true);

        Navigation.PushAsync(new NewPage1(pageWidth), true);
    }

	//**********************************************************************************
    private void Click_btnOpenPage_modal(object sender, EventArgs e)
    {
        addLog("-----Click open page moal-----", true);
        Navigation.PushModalAsync(new NewPage1(pageWidth), true);
    }

    //**********************************************************************************
    private async void Click_btnReadGPIO(object sender, EventArgs e)
    {
        addLog("-----Click read GPIO-----", true);

        

        TcpCtrl cls_tcpCtrl = new TcpCtrl();
        TextCtrl cls_textCtrl = new TextCtrl();
        string ipAdd = "raspberrypi.local";
        int port = 10000;

        TcpClient tcp;
        NetworkStream ns;

        string rcv;
        int portNum = cls_textCtrl.extractNum(btnReadGPIO.Text);
        string end_word = "\n";

        try
        {
            tcp = cls_tcpCtrl.connectTcp(ipAdd, port);
            ns = cls_tcpCtrl.getNetworkStream(tcp);

            rcv = cls_tcpCtrl.Recieve_TCP(ns);
            addLog($"reciev : {rcv}", true);

            cls_tcpCtrl.Send_TCP(ns, portNum.ToString());
            addLog($"send : {portNum.ToString()}", true);
            await Task.Delay(500);

            rcv = "";
            int loopCnt = 0;

            for (loopCnt = 0; loopCnt < 10; loopCnt++)
            {
                rcv += cls_tcpCtrl.Recieve_TCP(ns);
                if (rcv.Contains(end_word))
                    break;
                await Task.Delay(500);
            }

            addLog($"reciev : {rcv}", false);
            addLog($"Loop : {loopCnt}", true);

            ns.Close();
            tcp.Close();
        }
        catch
        {
            await DisplayAlert("Error", "Connection error", "OK");
            addLog("Connection error", true);
        }
    }

    //**********************************************************************************
    private void clicked_btnDown(object sender, EventArgs e)
    {
        TextCtrl cls_textCtrl = new TextCtrl();
        int num = cls_textCtrl.extractNum(btnReadGPIO.Text);
        if (num > 0)
            btnReadGPIO.Text = $"Port {num - 1}";
    }

    //**********************************************************************************
    private void clicked_btnUp(object sender, EventArgs e)
    {
        TextCtrl cls_textCtrl = new TextCtrl();
        int num = cls_textCtrl.extractNum(btnReadGPIO.Text);
        if (num < 40)
            btnReadGPIO.Text = $"Port {num + 1}";
    }


    //**********************************************************************************
    private void Click_btnCheckDevice(object sender, EventArgs e)
    {
        addLog("-----Click read debug-----", true);
        SystemCtrl SC = new SystemCtrl();
        //addLog($"device : {SC.PrintIdiom().ToString()}", true);
        addLog($"device : {DeviceInfo.Current.Platform}", true);
        addLog($"device : {DeviceInfo.Current.Version}", true);
    }

    //**********************************************************************************
    private async void Click_btnDebug(object sender, EventArgs e)
    {
        addLog("-----Click read debug-----", true);
        await Navigation.PushAsync(new NewPage2(), true);
    }




//**********************************************************************************


}
//**********************************************************************************
