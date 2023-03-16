using MortgageCalculator.Classes;
using System.Text.RegularExpressions;
using MauiCtrl;
using System.Diagnostics;

namespace MortgageCalculator;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		//this.WidthRequest = 500;
		ClsDebug.DebugWriteLine(ClsCommon.CURRENT_DIRECTRY);

        //標準ナビバー非表示
        NavigationPage.SetHasNavigationBar(this, false);
#if ANDROID
        ClsDebug.DebugWriteLine(ClsCommon.LOCAL_APP_DATA);
#endif

        Frame0.HeightRequest = Grid0.Height;
        Frame1.HeightRequest = Grid1.Height;
        Frame2.HeightRequest = Grid2.Height;

        SetInit();
    }

    //*******************************************************************
    private void ClickedBtnCal(object sender, EventArgs e)
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
        selectTypeImage(PickerType.SelectedIndex);

    }

    //*******************************************************************
    private void SetInit()
    {
        //ピッカーの選択項目をセット
        var listLoanTypes = new List<string>();
        listLoanTypes.Add("元金均等返済");
        listLoanTypes.Add("元利均等返済");
        PickerType.ItemsSource = listLoanTypes;
        PickerType.SelectedIndex = 0;
        selectTypeImage(PickerType.SelectedIndex);

        //umd仕様
        EntryLoanPrice.Text = "4000";
        EntryInterestRate.Text = "1";
        EntryYearsOfRepayment.Text = "30";
        PickerType.SelectedIndex = 0;
        EntrySaving.Text = "10";
        EntryAgeA.Text = "42";
        EntryAgeB.Text = "51";
        EntryAgeC.Text = "";
    }

    //*******************************************************************
    private void selectTypeImage(int num)
    {
        ImageType.Source = ClsCommon.ImageDataSources[num];
    }

    //*******************************************************************
    private ClsCommon.Status GetLoanStatus()
    {
        ClsCommon.Status resultStatus = new();

        resultStatus.LoanPrice = int.Parse(EntryLoanPrice.Text);
        resultStatus.InterestRate = int.Parse(EntryInterestRate.Text);
        resultStatus.YearsOfRepayment = int.Parse(EntryYearsOfRepayment.Text);
        resultStatus.RepaymentType = PickerType.SelectedIndex;
        resultStatus.Saving = int.Parse(EntrySaving.Text);
        resultStatus.AgeA = int.Parse(EntryAgeA.Text);
        resultStatus.AgeB = int.Parse(EntryAgeB.Text);
        resultStatus.AgeC = int.Parse(EntryAgeC.Text);
        
        return resultStatus;
    }

}

