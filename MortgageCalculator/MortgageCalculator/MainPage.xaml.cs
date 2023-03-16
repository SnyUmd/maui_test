using MortgageCalculator.Classes;
using System.Text.RegularExpressions;
using MauiCtrl;

namespace MortgageCalculator;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		//this.WidthRequest = 500;
		ClsDebug.DebugWriteLine(ClsCommon.CURRENT_DIRECTRY);
#if ANDROID
        ClsDebug.DebugWriteLine(ClsCommon.LOCAL_APP_DATA);
#endif
        SetPickerItems();
        Frame0.HeightRequest = Grid0.Height;

    }

	//*******************************************************************
	private void OnCounterClicked(object sender, EventArgs e)
    {
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
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
    private void SetPickerItems()
    {
        var listLoanTypes = new List<string>();
        listLoanTypes.Add("元金均等返済");
        listLoanTypes.Add("元利均等返済");
        PickerType.ItemsSource = listLoanTypes;
        PickerType.SelectedIndex = 0;
        selectTypeImage(PickerType.SelectedIndex);
    }

    private void selectTypeImage(int num)
    {
        ImageType.Source = ClsCommon.ImageDataSources[num];
    }
}

