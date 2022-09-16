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

    List<DeviceIdiom> device = new List<DeviceIdiom>();


	//**********************************************************************************
    public MainPage()
    {
		InitializeComponent();

        setDeviceList(ref device);
        checkDevice();
        this.lblLog.Text += "\n";

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

    private void addLog(string add_val)
    {
        this.lblLog.Text += add_val;
        this.lblLog.Text += "\n";
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
    private void checkDevice()
    {
        SystemCtrl SC = new SystemCtrl();
        DeviceIdiom executionDevice = SC.PrintIdiom();

        if (executionDevice == device[(int)enmDevice.desktop] ||
            executionDevice == device[(int)enmDevice.tablet])
        {
            this.WidthRequest = 400;
        }
    }


    //**********************************************************************************
    private void OnCounterClicked(object sender, EventArgs e)
    {
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	//**********************************************************************************
	private async void Click_btnPopUp(object sender, EventArgs e)
	{
        addLog("-----Click po up-----");
        MC.MessageAlert("0", "1", "2");

        //messageView();
        await DisplayAlert("Alert", "You have been alerted", "OK");

        bool answer = await DisplayAlert("Question?", "which ?", "Yes", "No");
        addLog($"answer : {answer.ToString()}");

        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
        addLog($"selected : {action}");
    }

	//**********************************************************************************
	private async void Click_btnFileRead(object sender, EventArgs e)
    {
        addLog("-----Click file read-----");
        var select_file = await FC.ReadFile(null);
        if (select_file != null)
            await DisplayAlert("Select file", select_file.FullPath, "OK");
    }

	//**********************************************************************************
    private void Click_btnOpenPage(object sender, EventArgs e)
    {
        addLog("-----Click open page-----");

        Navigation.PushAsync(new NewPage1(), true);
    }

	//**********************************************************************************
    private void Click_btnOpenPage_modal(object sender, EventArgs e)
    {
        addLog("-----Click open page moal-----");
        Navigation.PushModalAsync(new NewPage1(), true);
    }

    //**********************************************************************************
    private async void Click_btnReadGPIO(object sender, EventArgs e)
    {
        addLog("-----Click read GPIO-----");
        TcpCtrl TC = new TcpCtrl();
        string ipAdd = "192.168.0.18";
        int port = 10000;

        string rcv;
        int portNum = 14;
        string end_mess = "end";

        TcpClient tcp = TC.connectTcp(ipAdd, port);
        NetworkStream ns = TC.getNetworkStream(tcp);

        try
        {
            rcv = TC.Recieve_TCP(ns);
            this.lblLog.Text += $"reciev : {rcv}\n";
            TC.Send_TCP(ns, portNum.ToString());
            this.lblLog.Text += $"send : {portNum.ToString()}\n";
            await Task.Delay(500);

            rcv = "";
            int loopCnt = 0;

            for (loopCnt = 0; loopCnt < 10; loopCnt++)
            {
                rcv += TC.Recieve_TCP(ns);
                if (rcv.Contains(end_mess))
                    break;
                await Task.Delay(500);
            }

            this.lblLog.Text += $"reciev : {rcv}\n";
            this.lblLog.Text += $"Loop : {loopCnt}\n";
        }
        catch
        {
            await DisplayAlert("err", "Error", "OK");
        }

        ns.Close();
        tcp.Close();
    }

    //**********************************************************************************
    private async void Click_btnDebug(object sender, EventArgs e)
    {
        addLog("-----Click read debug-----");
        await Navigation.PushAsync(new NewPage2(), true);


    }


}

