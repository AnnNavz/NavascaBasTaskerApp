using NavascaBasTaskerApp.MVVM.ViewModels;
using NavascaBasTaskerApp.Views;

namespace NavascaBasTaskerApp.MVVM.Views;

public partial class MainView : ContentPage
{
	bool isMenuOpen = false;

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

	private async void OnMainFabClicked(object sender, EventArgs e)
	{
		if (!isMenuOpen)
		{
			isMenuOpen = true;
			await MainFab.RotateTo(45, 250, Easing.CubicInOut);

			// Pop out secondary buttons
			btnTask.IsVisible = true;
			btnCategory.IsVisible = true;

			await Task.WhenAll(
				btnTask.ScaleTo(1, 250, Easing.SpringOut),
				btnTask.FadeTo(1, 250),
				btnCategory.ScaleTo(1, 250, Easing.SpringOut),
				btnCategory.FadeTo(1, 250)
			);
		}
		else
		{
			isMenuOpen = false;
			await MainFab.RotateTo(0, 250, Easing.CubicInOut);

			await Task.WhenAll(
				btnTask.ScaleTo(0, 250, Easing.SpringIn),
				btnTask.FadeTo(0, 250),
				btnCategory.ScaleTo(0, 250, Easing.SpringIn),
				btnCategory.FadeTo(0, 250)
			);

			btnTask.IsVisible = false;
			btnCategory.IsVisible = false;
		}
	}
}