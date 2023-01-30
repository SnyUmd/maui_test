//using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Fl3apis000;



public partial class MainPage : ContentPage
{

    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }


    [Route("api/{controller}")]
    [ApiController]
    public class TotoController : ControllerBase
    {
        // Todoアイテムの初期データ。本来はデータベースなどから取得する。
        private static List<TodoItem> items = new List<TodoItem>() {
            new TodoItem() { Id = 1, Name = @"犬の散歩", IsDone = false, },
            new TodoItem() { Id = 2, Name = @"買い物", IsDone = true, },
            new TodoItem() { Id = 3, Name = @"本棚の修理", IsDone = false },
        };

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
            => items;

        [HttpGet("{id}", Name = "Todo")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null)
                return NotFound();
            return item;
        }
    }

    //*********************************************************************************
    //*********************************************************************************
    //*********************************************************************************


    public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{

    }
}

