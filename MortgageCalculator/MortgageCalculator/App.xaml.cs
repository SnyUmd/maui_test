using MortgageCalculator.Classes;

namespace MortgageCalculator;

public partial class App : Application
{
	string separator = "\\";

    public App()
	{
#if ANDROID
		separator = "/";
#endif
		InitializeComponent();
        ClsCommon.Init();

        MainPage = new NavigationPage(new Pages.MainPage());

        
        //MainPage = new AppShell();
		//MainPage = new NavigationPage(new Pages.ResultPage());

    }
}
