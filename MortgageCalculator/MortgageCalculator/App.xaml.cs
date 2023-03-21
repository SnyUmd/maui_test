using Android.OS;
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
        MainPage = new NavigationPage(new Pages.MainPage());

        if (!System.IO.File.Exists(ClsCommon.DbFilePath));
		{
			SqliteCtrl.CreateDbFile(ClsCommon.DbFileName, ClsCommon.CurrentDir + ClsCommon.DirSeparator);
			MauiCtrl.ClsDebug.DebugWriteLine(System.IO.File.Exists(ClsCommon.DbFilePath).ToString());
		}
		
		if(MauiCtrl.SqliteCtrl.FindTable(ClsCommon.DbFilePath, "tbl_history_status"))
		{
			MauiCtrl.ClsDebug.DebugWriteLine("tbl_history_status false");
		}
		else
		{
            MauiCtrl.ClsDebug.DebugWriteLine("tbl_history_status true");

        }
        //MainPage = new AppShell();
		//MainPage = new NavigationPage(new Pages.ResultPage());

    }
}
