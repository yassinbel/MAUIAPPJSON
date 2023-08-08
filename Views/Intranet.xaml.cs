namespace SQLiteDemo.Views;

public partial class Intranet : ContentPage
{
	public Intranet()
	{
		InitializeComponent();

        Appearing += MainPage_Appearing;
    }

    private void MainPage_Appearing(object sender, System.EventArgs e)
    {
        MyWebView.Source = new UrlWebViewSource { Url = "https://intranet.sews-e.com/intranetmenu/" };
    }
}