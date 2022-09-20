namespace MauiApp0;

public partial class NewPage1 : ContentPage
{
	//**********************************************************************************
	public NewPage1(double width_)
    {
		InitializeComponent();
		if (width_ > 0)
			this.WidthRequest = width_;
	}

	//**********************************************************************************
	private void clicked_btnBack(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}