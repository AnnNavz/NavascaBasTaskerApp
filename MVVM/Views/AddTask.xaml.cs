using NavascaBasTaskerApp.MVVM.ViewModels;

namespace NavascaBasTaskerApp.Views;

public partial class AddTask : ContentPage
{
	public AddTask(AddTaskViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
}