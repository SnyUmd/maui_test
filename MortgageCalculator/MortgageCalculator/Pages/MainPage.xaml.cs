﻿using MortgageCalculator.Classes;
using System.Text.RegularExpressions;
using MauiCtrl;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Reflection;

namespace MortgageCalculator.Pages;

public partial class MainPage : ContentPage
{
    public static bool UpdateRequest = false;

    int count = 0;
    Action<int> actionChangeTypeImage;

    public MainPage()
	{
		InitializeComponent();
        BindingContext = new VM.ResultPageVM();
		ClsDebug.DebugWriteLine(ClsCommon.CurrentDir);

        //標準ナビバー非表示
        NavigationPage.SetHasNavigationBar(this, false);
#if ANDROID
        ClsDebug.DebugWriteLine(ClsCommon.CurrentDir);
#endif

        actionChangeTypeImage = (a) => ImageType.Source = ClsCommon.ImageDataSources[a];

        //Frame0.HeightRequest = Grid0.Height;
        //Frame1.HeightRequest = Grid1.Height;
        //Frame2.HeightRequest = Grid2.Height;

        SetInit();
    }

    //*******************************************************************
    protected override async void OnAppearing()
    {
        // 子画面をナビゲーションから削除
        var existingPages = Navigation.NavigationStack.ToList();
        Console.WriteLine(existingPages?.Count);
        foreach (var page in existingPages)
        {
            if (page != null && page != this)
            {
                Console.WriteLine("------------------RemovePage: " + page.Title);
                Navigation.RemovePage(page);
            }
        }

        if (UpdateRequest)
        {
            Debug.Write("--------------------");
            Debug.Write("update request : true");
            Debug.Write("--------------------");

            EntryLoanPrice.Text = ClsCommon.LoanStatus.LoanPrice.ToString();
            EntryInterestRate.Text = ClsCommon.LoanStatus.InterestRate.ToString();
            EntryYearsOfRepayment.Text = ClsCommon.LoanStatus.YearsOfRepayment.ToString();
            PickerType.SelectedIndex = ClsCommon.LoanStatus.RepaymentType;
            EntrySaving.Text = ClsCommon.LoanStatus.Saving.ToString();
            EntryAgeA.Text = ClsCommon.LoanStatus.AgeA.ToString();
            EntryAgeB.Text = ClsCommon.LoanStatus.AgeB.ToString();
            EntryAgeC.Text = ClsCommon.LoanStatus.AgeC.ToString();

            UpdateRequest = false;
        }

        int cnt = ClsCommon.GetRecordNum(Tables.tables_name[(int)EnmTable_num.tbl_history_status]);

        if (cnt > 0)
        {
            BtnHistory.IsEnabled = true;
            BtnHistory.BackgroundColor = Colors.Coral;
        }
        else
        {
            BtnHistory.IsEnabled = false;
            BtnHistory.BackgroundColor = Colors.SlateGray;
        }
    }

    //*******************************************************************
    private async void ClickedBtnCal(object sender, EventArgs e)
    {
        ClsCommon.LoanStatus = GetLoanStatus();

        Debug.WriteLine($"借入額：{ClsCommon.LoanStatus.LoanPrice} 万円");
        Debug.WriteLine($"金利：{ClsCommon.LoanStatus.InterestRate} %");
        Debug.WriteLine($"返済変数：{ClsCommon.LoanStatus.YearsOfRepayment} 年");
        Debug.WriteLine($"返済タイプ：{ClsCommon.LoanStatus.RepaymentType}");
        Debug.WriteLine($"貯金月額：{ClsCommon.LoanStatus.Saving} 万円 / 月");
        Debug.WriteLine($"年齢A：{ClsCommon.LoanStatus.AgeA} 歳");
        Debug.WriteLine($"年齢B：{ClsCommon.LoanStatus.AgeB} 歳");
        Debug.WriteLine($"年齢C：{ClsCommon.LoanStatus.AgeC} 歳");


        AddHistory();

        //await Navigation.PushAsync(new Pages.ResultPage());
        await Navigation.PushModalAsync(new Pages.ResultPage());
    }

    //*******************************************************************
    private async void ClickedHistory(object sender, EventArgs e)
    {
        List<string> lstHistiry = GetRecord(Tables.tables_name[(int)EnmTable_num.tbl_history_status]);

        await Navigation.PushModalAsync(new Pages.Modal.RecordSelectModal(lstHistiry));
        int a = 0;
    }

    //*******************************************************************
    private List<string> GetRecord(string table_name)
    {
        string query = $"select * from {table_name};";
        return MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query);

    }

    //*******************************************************************
    /// <summary>
    /// 入力イベント　入力制限
    /// </summary>
    /// <param name="entry"></param>
    public void InputLimit(Entry entry)
    {
        if (entry.Text != "" && entry.Text != null)
            entry.Text = Regex.Replace(entry.Text, @"[^!-~]", "");
    }

    //*******************************************************************
    private void TextChanged(object sender, TextChangedEventArgs e)
    {
		InputLimit((Entry)sender);
    }

    //*******************************************************************
    private void SelectedIndexChange_PickerType(object sender, EventArgs e)
    {
        //selectTypeImage(PickerType.SelectedIndex);
        actionChangeTypeImage(PickerType.SelectedIndex);

    }

    //*******************************************************************
    private void SetInit()
    {
        //ピッカーの選択項目をセット
        var listLoanTypes = new List<string>();
        listLoanTypes.Add(ClsCommon.LoanText[0]);
        listLoanTypes.Add(ClsCommon.LoanText[1]);
        PickerType.ItemsSource = listLoanTypes;
        PickerType.SelectedIndex = 0;
        //selectTypeImage(PickerType.SelectedIndex);
        actionChangeTypeImage(PickerType.SelectedIndex);

        /*
        string puery = $"SELECT COUNT(*) FROM {Tables.tables_name[(int)EnmTable_num.tbl_history_status]}";
        List<string> records = SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, puery);
        JsonNode jn = JsonNode.Parse(records[0]);
        int cnt = int.Parse(jn["COUNT(*)"].ToString());
        */

        /*
        string puery = $"select * from {Tables.tables_name[(int)EnmTable_num.tbl_history_status]}";
        List<string> records = SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, puery);
        */
        int cnt = ClsCommon.GetRecordNum(Tables.tables_name[(int)EnmTable_num.tbl_history_status]);

        if (cnt > 0)
        {
            SetStatus_EndOfHistory();
        }
        else
        {
            EntryLoanPrice.Text = "3000";
            EntryInterestRate.Text = "0.5";
            EntryYearsOfRepayment.Text = "35";
            PickerType.SelectedIndex = 0;
            EntrySaving.Text = "15";
            EntryAgeA.Text = "41";
            EntryAgeB.Text = "50";
            EntryAgeC.Text = "";

        }
    }

    //*******************************************************************
    private void AddHistory()
    {
        string[] columnValues = new string[]
        {
            "null",
            EntryLoanPrice.Text,
            EntryInterestRate.Text,
            EntryYearsOfRepayment.Text,
            PickerType.SelectedIndex.ToString(),
            EntrySaving.Text,
            EntryAgeA.Text,
            EntryAgeB.Text,
            EntryAgeC.Text != "" ? EntryAgeC.Text : "null",
            "null",
            "null"
        };

        string tableName = Tables.tables_name[(int)EnmTable_num.tbl_history_status];
        MauiCtrl.SqliteCtrl.InsertRecord(ClsCommon.DbFilePath, tableName, columnValues);
        
        string query = $"select * from {tableName};";
        Debug.WriteLine(MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query));

    }


    //*******************************************************************
    private void SetStatus_EndOfHistory()
    {
        //履歴の最後のテーブル情報に画面を更新
        //string query = $"select * from {Tables.tables_name[(int)EnmTable_num.tbl_history_status]};";
        //var history = MauiCtrl.SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, query);
        var history = GetRecord(Tables.tables_name[(int)EnmTable_num.tbl_history_status]);


        var record = JsonNode.Parse(history[history.Count - 1]);
        Debug.WriteLine(record[Tables.tbl_history_status[1]]);

        EntryLoanPrice.Text = record[Tables.tbl_history_status[1]].ToString();
        EntryInterestRate.Text = record[Tables.tbl_history_status[2]].ToString();
        EntryYearsOfRepayment.Text = record[Tables.tbl_history_status[3]].ToString();
        PickerType.SelectedIndex = int.Parse(record[Tables.tbl_history_status[4]].ToString());
        EntrySaving.Text = record[Tables.tbl_history_status[5]].ToString();
        EntryAgeA.Text = record[Tables.tbl_history_status[6]].ToString();
        EntryAgeB.Text = record[Tables.tbl_history_status[7]].ToString();
        EntryAgeC.Text = record[Tables.tbl_history_status[8]].ToString();
    }

    //*******************************************************************
    //private void selectTypeImage(int num)
    //{
    //    ImageType.Source = ClsCommon.ImageDataSources[num];
    //}

    //*******************************************************************
    private ClsStatus GetLoanStatus()
    {
        ClsStatus resultStatus = new();

        resultStatus.LoanPrice = double.Parse(EntryLoanPrice.Text);
        resultStatus.InterestRate = double.Parse(EntryInterestRate.Text);
        resultStatus.YearsOfRepayment = int.Parse(EntryYearsOfRepayment.Text);
        resultStatus.RepaymentType = PickerType.SelectedIndex;
        resultStatus.Saving = double.Parse(EntrySaving.Text);
        if(EntryAgeA.Text != "")
            resultStatus.AgeA = int.Parse(EntryAgeA.Text);
        if(EntryAgeB.Text != "")
            resultStatus.AgeB = int.Parse(EntryAgeB.Text);
        if(EntryAgeC.Text != "")
            resultStatus.AgeC = int.Parse(EntryAgeC.Text);
        
        return resultStatus;
    }


}

