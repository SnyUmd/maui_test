//using Android.Views;

namespace Hct101;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

    }
}
