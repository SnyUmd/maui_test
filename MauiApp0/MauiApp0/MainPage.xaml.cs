namespace MauiApp0;

using MauiCtrl;

public partial class MainPage : ContentPage
{
    FileCtrl FC = new FileCtrl();
    MessageCtrl MC = new MessageCtrl();

	int count = 0;

	public MainPage()
	{
		InitializeComponent();
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

	//private async Task<FileResult> ReadFile(PickOptions options)
	//{
 //       try
 //       {
 //           var result = await FilePicker.Default.PickAsync(options);
 //           if (result != null)
 //           {
 //               if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
 //                   result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
 //               {
 //                   using var stream = await result.OpenReadAsync();
 //                   var image = ImageSource.FromStream(() => stream);
 //               }
 //           }

 //           return result;
 //       }
 //       catch (Exception ex)
 //       {
 //           // The user canceled or something went wrong
 //       }

 //       return null;
 //   }



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
        //var customFileType = new FilePickerFileType(
        //        new Dictionary<DevicePlatform, IEnumerable<string>>
        //        {
        //            { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
        //            { DevicePlatform.Android, new[] { "application/comics" } },
        //            { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } },
        //            { DevicePlatform.Tizen, new[] { "*/*" } },
        //            { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
        //        });

        //PickOptions options = new()
        //{
        //    PickerTitle = "Please select a comic file",
        //    FileTypes = customFileType,
        //};

        var select_file = await FC.ReadFile(null);
        if(select_file != null)
            await DisplayAlert("Select file", select_file.FullPath, "OK");
    }

    private void Click_btnOpenPage(object sender, EventArgs e)
    {
        //NewPage1 np1 = new NewPage1();
        Window secondWindow = new Window(new NewPage1());
        Application.Current.OpenWindow(secondWindow);
    }
}

