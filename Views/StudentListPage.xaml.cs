using SQLiteDemo.ViewModels;

namespace SQLiteDemo.Views;

public partial class StudentListPage : ContentPage
{
    private StudentListPageViewModel _viewMode;
    public StudentListPage(StudentListPageViewModel viewModel)
	{
		InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetStudentListCommand.Execute(null);
    }

    private async void OnOpenNewPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Intranet());
    }

}