using MauiCtrl;
using MortgageCalculator.Classes;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;

namespace MortgageCalculator.Pages.Modal;

//*******************************************************************
public class RecordSelectModalVM
{
    public ObservableCollection<ClsStatus> sts { get; set; } = new ObservableCollection<ClsStatus>();
    

    public string LOAN_PRICE { get; } = "借入価格";
    public string INTEREST_RATE { get; } = "金利";
    public string YEAR_OF_REPAYMENT1 { get; } = "返済年数";
    public string PROGRESS_YEAR { get; } = "返済経過年数";
    public string LOAN_TYPE { get; } = "返済タイプ";
    public string SAVING { get; } = "貯金月額";
    public string AGE_A { get; } = "開始年齢 A";
    public string AGE_B { get; } = "開始年齢 B";
    public string AGE_C { get; } = "開始年齢 C";

    //*******************************************************************
    public RecordSelectModalVM()
    {

    }

    //*******************************************************************
    public void SetValueContextView(ClsStatus cls_val)
    {
        sts.Add(new ClsStatus(cls_val));
    }
}

//*******************************************************************
//*******************************************************************
//*******************************************************************
public partial class RecordSelectModal : ContentPage
{
	List<string> lstRecords;
    RecordSelectModalVM vmRecordSelectModal = new RecordSelectModalVM();


    //public ObservableCollection<ClsStatus> sts { get; set; } = new ObservableCollection<ClsStatus>();

    //*******************************************************************
    public RecordSelectModal(List<string> lst_records)
	{
		InitializeComponent();
        lstRecords = lst_records;
        BindingContext = vmRecordSelectModal;

        setHistory();


    }

    //*******************************************************************
    private void setHistory()
    {
        int RecordNum= 1;
        List<ClsStatus> values = new List<ClsStatus>();
        ClsStatus buf = new ClsStatus();
        //buf.LoanPrice = 100;
        //buf.InterestRate = 100;
        //buf.YearsOfRepayment = 100;
        //buf.RepaymentType = 100;
        //buf.Saving = 100;
        //buf.AgeA = 100;
        //buf.AgeB = 100;
        //buf.AgeC = 100;
        //vmRecordSelectModal.SetValueContextView(buf);

        //foreach (string s in lstRecords)
        for(int i = lstRecords.Count - 1; i >= 0; i--)
        {
            var js = JsonNode.Parse(lstRecords[i]);

            buf.LoanPrice = double.Parse(js[Tables.tbl_history_status[1]].ToString());
            buf.InterestRate = double.Parse(js[Tables.tbl_history_status[2]].ToString());
            buf.YearsOfRepayment = int.Parse(js[Tables.tbl_history_status[3]].ToString());
            buf.RepaymentType = int.Parse(js[Tables.tbl_history_status[4]].ToString());
            buf.Saving = int.Parse(js[Tables.tbl_history_status[5]].ToString());
            if(js[Tables.tbl_history_status[6]].ToString() != "") buf.AgeA = int.Parse(js[Tables.tbl_history_status[6]].ToString());
            if(js[Tables.tbl_history_status[7]].ToString() != "") buf.AgeB = int.Parse(js[Tables.tbl_history_status[7]].ToString());
            if(js[Tables.tbl_history_status[8]].ToString() != "") buf.AgeC = int.Parse(js[Tables.tbl_history_status[8]].ToString());
            buf.Num = RecordNum.ToString(); RecordNum++;
            vmRecordSelectModal.SetValueContextView(buf);
        }

        //foreach (var val in values)
        //    vmRecordSelectModal.SetValueContextView(val);
    }

    //*******************************************************************
    private async void Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int recNum = lstRecords.Count - int.Parse(btn.Text);

        JsonNode jn = JsonNode.Parse(lstRecords[recNum]);
        ClsCommon.LoanStatus.LoanPrice = double.Parse(jn[Tables.tbl_history_status[1]].ToString());
        ClsCommon.LoanStatus.InterestRate = double.Parse(jn[Tables.tbl_history_status[2]].ToString());
        ClsCommon.LoanStatus.YearsOfRepayment = int.Parse(jn[Tables.tbl_history_status[3]].ToString());
        ClsCommon.LoanStatus.RepaymentType = int.Parse(jn[Tables.tbl_history_status[4]].ToString());
        ClsCommon.LoanStatus.Saving = int.Parse(jn[Tables.tbl_history_status[5]].ToString());
        ClsCommon.LoanStatus.AgeA = jn[Tables.tbl_history_status[6]].ToString() != "" ? int.Parse(jn[Tables.tbl_history_status[6]].ToString()) : 0;
        ClsCommon.LoanStatus.AgeB = jn[Tables.tbl_history_status[7]].ToString() != "" ? int.Parse(jn[Tables.tbl_history_status[7]].ToString()) : 0;
        ClsCommon.LoanStatus.AgeC = jn[Tables.tbl_history_status[8]].ToString() != "" ? int.Parse(jn[Tables.tbl_history_status[8]].ToString()) : 0;
        ClsCommon.LoanStatus.Num = jn.ToString();

        MainPage.UpdateRequest = true;
        await Navigation.PopModalAsync();
    }

    //*******************************************************************
    private async void ClickedBack(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    //*******************************************************************
    private async void ClickedDelete(object sender, EventArgs e)
    {

        if (await DisplayAlert("確認", "履歴を全て削除しても良いですか？", "Yes", "No"))
        {
            string que = $"delete from {Tables.tables_name[(int)EnmTable_num.tbl_history_status]};";
            SqliteCtrl.ReadQuery(ClsCommon.DbFilePath, que);
            await DisplayAlert("完了", "履歴を全て削除しました。", "OK");
            await Navigation.PopModalAsync();
        }


        
    }
}