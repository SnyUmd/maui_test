using MortgageCalculator.Classes;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;

namespace MortgageCalculator.Pages.Modal;

//*******************************************************************
public class RecordSelectModalVM
{
    public ObservableCollection<ClsValue> Values { get; set; } = new ObservableCollection<ClsValue>();

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
    public void SetValueContextView(ClsValue cls_val)
    {
        Values.Add(new ClsValue(cls_val));
    }
}

//*******************************************************************
public partial class RecordSelectModal : ContentPage
{
	List<string> lstRecords;
    RecordSelectModalVM vmRecordSelectModal = new RecordSelectModalVM();


    public ObservableCollection<ClsValue> Values { get; set; } = new ObservableCollection<ClsValue>();

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
        List<ClsValue> values = new List<ClsValue>();

        ClsValue buf = new ClsValue();
        buf.Price = 100;
        buf.Interest = 100;
        buf.YearOfRepayment = 100;
        buf.Type = 100;
        buf.Saving = 100;
        buf.AgeA = 100;
        buf.AgeB = 100;
        buf.AgeC = 100;
        vmRecordSelectModal.SetValueContextView(buf);

        foreach (string s in lstRecords)
        {
            var js = JsonNode.Parse(s);

        }

        //foreach (var val in values)
        //    vmRecordSelectModal.SetValueContextView(val);
    }
}