using MortgageCalculator.Classes;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;

namespace MortgageCalculator.Pages.Modal;

//*******************************************************************
public class RecordSelectModalVM
{
    public ObservableCollection<ClsValue> Values { get; set; } = new ObservableCollection<ClsValue>();

    public string LOAN_PRICE { get; } = "�ؓ����i";
    public string INTEREST_RATE { get; } = "����";
    public string YEAR_OF_REPAYMENT1 { get; } = "�ԍϔN��";
    public string PROGRESS_YEAR { get; } = "�ԍόo�ߔN��";
    public string LOAN_TYPE { get; } = "�ԍσ^�C�v";
    public string SAVING { get; } = "�������z";
    public string AGE_A { get; } = "�J�n�N�� A";
    public string AGE_B { get; } = "�J�n�N�� B";
    public string AGE_C { get; } = "�J�n�N�� C";

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