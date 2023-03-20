using MortgageCalculator.Classes;
using System.Text.RegularExpressions;
using MauiCtrl;
using System.Diagnostics;

namespace MortgageCalculator.Pages;

public partial class MainPage : ContentPage
{
	int count = 0;
    Action<int> actionChangeTypeImage;

    public MainPage()
	{
		InitializeComponent();
        BindingContext = new VM.ResultPageVM();
		ClsDebug.DebugWriteLine(ClsCommon.CURRENT_DIRECTRY);

        //標準ナビバー非表示
        NavigationPage.SetHasNavigationBar(this, false);
#if ANDROID
        ClsDebug.DebugWriteLine(ClsCommon.LOCAL_APP_DATA);
#endif

        actionChangeTypeImage = (a) => ImageType.Source = ClsCommon.ImageDataSources[a];

        //Frame0.HeightRequest = Grid0.Height;
        //Frame1.HeightRequest = Grid1.Height;
        //Frame2.HeightRequest = Grid2.Height;

        SetInit();
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

        await Navigation.PushAsync(new Pages.ResultPage());
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

        //umd仕様
        EntryLoanPrice.Text = "4000";
        EntryInterestRate.Text = "1.5";
        EntryYearsOfRepayment.Text = "35";
        PickerType.SelectedIndex = 0;
        EntrySaving.Text = "17";
        EntryAgeA.Text = "41";
        EntryAgeB.Text = "50";
        EntryAgeC.Text = "";
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

