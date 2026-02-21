using NavascaBasTaskerApp.MVVM.ViewModels;
using NavascaBasTaskerApp.Views;

namespace NavascaBasTaskerApp.MVVM.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void AddTask_Clicked(object sender, EventArgs e)
	{
		var addTaskPage = Handler.MauiContext.Services.GetService<NavascaBasTaskerApp.Views.AddTask>();

		await Navigation.PushAsync(addTaskPage);
	}
}