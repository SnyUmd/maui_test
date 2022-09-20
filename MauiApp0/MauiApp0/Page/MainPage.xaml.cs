#define Deb

namespace MauiApp0;

using MauiApp0.Page;
using MauiCtrl;

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        TcpCtrl TC = new TcpCtrl();
        string ipAdd = "192.168.0.18";
        int port = 10000;

        string rcv;
        int portNum = int.Parse(lblPortNum.Text);
        string end_word = "\n";

        TcpClient tcp = TC.connectTcp(ipAdd, port);
        NetworkStream ns = TC.getNetworkStream(tcp);

        try
        {
            rcv = TC.Recieve_TCP(ns);
            addLog($"reciev : {rcv}", true);

            TC.Send_TCP(ns, portNum.ToString());
            addLog($"send : {portNum.ToString()}", true);
            await Task.Delay(500);

            rcv = "";
            int loopCnt = 0;

            for (loopCnt = 0; loopCnt < 10; loopCnt++)
            {
                rcv += TC.Recieve_TCP(ns);
                if (rcv.Contains(end_word))
                    break;
                await Task.Delay(500);
            }


            addLog($"reciev : {rcv}", false);
            addLog($"Loop : {loopCnt}", true);

        }
        catch
        {
            await DisplayAlert("err", "Error", "OK");
        }

        ns.Close();
        tcp.Close();
    }

    //**********************************************************************************
    private void clicked_btnDown(object sender, EventArgs e)
    {
        int num = int.Parse(lblPortNum.Text);
        if (num > 0)
            lblPortNum.Text = (num - 1).ToString();
    }

    //**********************************************************************************
    private void clicked_btnUp(object sender, EventArgs e)
    {
        int num = int.Parse(lblPortNum.Text);
        if (num < 40)
            lblPortNum.Text = (num + 1).ToString();
    }


    //**********************************************************************************
    private void Click_btnCheckDevice(object sender, EventArgs e)
    {
        addLog("-----Click read debug-----", true);
        SystemCtrl SC = new SystemCtrl();
        addLog($"device : {SC.PrintIdiom().ToString()}", true);
    }

    //**********************************************************************************
    private async void Click_btnDebug(object sender, EventArgs e)
    {
        addLog("-----Click read debug-----", true);
        await Navigation.PushAsync(new NewPage2(), true);
    }


}
//**********************************************************************************
