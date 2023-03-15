using Android.Content.Res;

namespace Call;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		
		MainPage = new NavigationPage(new MainPage());

        //await Navigation.PushModalAsync(modalArrivalInspection);//???


    }
}
