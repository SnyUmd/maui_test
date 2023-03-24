using MortgageCalculator.Classes;
using System.Diagnostics;

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
        Init();

        MainPage = new NavigationPage(new Pages.MainPage());

        
        //MainPage = new AppShell();
		//MainPage = new NavigationPage(new Pages.ResultPage());

    }


    //*******************************************************************
    public static void Init()
    {
        //DBファイルをチェック
        if (!System.IO.File.Exists(ClsCommon.DbFilePath))
        {
            MauiCtrl.SqliteCtrl.CreateDbFile(ClsCommon.DbFileName, ClsCommon.CurrentDir + ClsCommon.DirSeparator);
            MauiCtrl.ClsDebug.DebugWriteLine(System.IO.File.Exists(ClsCommon.DbFilePath).ToString());
        }

        //テーブルの存在をチェック
        string tableName;
        string query;
        string[] values;

        //tbl_saved_status-------------------------
        tableName = ClsCommon.TablesName[(int)EnmTable_num.tbl_saved_status];
        values = new string[]
        {
            "0",
            "\'sample\'",
            "5000",
            "0.5",
            "35",
            "0",
            "41",
            "50",
            "0",
        };

        if (!MauiCtrl.SqliteCtrl.FindTable(ClsCommon.DbFilePath, tableName))
        {
            MauiCtrl.ClsDebug.DebugWriteLine($"{tableName} false");
            MauiCtrl.SqliteCtrl.CreateTable(ClsCommon.DbFilePath, tableName, Tables.CreateTableStr.tbl_saved_status);

        }
        else
        {
            MauiCtrl.ClsDebug.DebugWriteLine($"{tableName} true");
        }

        MauiCtrl.SqliteCtrl.InsertRecord(ClsCommon.DbFilePath, tableName, values);
        query = $"PRAGMA table_info('{tableName}');";
        Debug.WriteLine(MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query));
        query = $"select * from {tableName};";
        Debug.WriteLine(MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query));

        //tbl_history_status-------------------------
        tableName = ClsCommon.TablesName[(int)EnmTable_num.tbl_history_status];
        values = new string[]
        {
            "0",
            "5000",
            "0.5",
            "35",
            "0",
            "41",
            "50",
            "0",
            "\'2023-01-01\'",
            "\'23-59-59\'"
        };
        if (!MauiCtrl.SqliteCtrl.FindTable(ClsCommon.DbFilePath, tableName))
        {
            MauiCtrl.ClsDebug.DebugWriteLine($"{tableName} false");
            MauiCtrl.SqliteCtrl.CreateTable(ClsCommon.DbFilePath, tableName, Tables.CreateTableStr.tbl_history_status);
        }
        else
        {
            MauiCtrl.ClsDebug.DebugWriteLine($"{tableName} true");
        }

        MauiCtrl.SqliteCtrl.InsertRecord(ClsCommon.DbFilePath, tableName, values);
        query = $"PRAGMA table_info('{tableName}');";
        Debug.WriteLine(MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query));
        query = $"select * from {tableName};";
        Debug.WriteLine(MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query));
    }
}
