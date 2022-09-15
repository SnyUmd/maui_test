namespace MauiApp0;

using MauiCtrl;

public enum enmDevice
{
    desktop = 0,
    phone,
    tablet,
    TV,
    Watch
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
        MC.MessageAlert("0", "1", "2");

        //messageView();
        await DisplayAlert("Alert", "You have been alerted", "OK");

        bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        await DisplayAlert("Alert", answer.ToString(), "OK");

        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
        await DisplayAlert("Alert", action, "OK");
    }

	//**********************************************************************************
	private async void Click_btnFileRead(object sender, EventArgs e)
    {
        var select_file = await FC.ReadFile(null);
        if(select_file != null)
            await DisplayAlert("Select file", select_file.FullPath, "OK");
    }

	//**********************************************************************************
    private void Click_btnOpenPage(object sender, EventArgs e)
    {
        //NewPage1 np1 = new NewPage1();
        //Window secondWindow = new Window(new NewPage1());
        //Application.Current.OpenWindow(secondWindow);
        Navigation.PushAsync(new NewPage1(), true);
    }

	//**********************************************************************************
    private void Click_btnReadGPIO(object sender, EventArgs e)
    {

    }

	//**********************************************************************************
    private async void Click_btnDebug(object sender, EventArgs e)
    {
        //int window_w = (int)this.Width;
        //await DisplayAlert("", window_w.ToString(), "OK");
        //if (window_w > 500)
        //    this.WidthRequest = 400;
        //await DisplayAlert("", ((int)enmDevice.phone).ToString(), "OK");
    }
}

