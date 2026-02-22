using CommunityToolkit.Maui.Views;
using NavascaBasTaskerApp.MVVM.Models;
namespace NavascaBasTaskerApp.MVVM.Views;


public partial class TaskDetailPopup : Popup
{
	public bool IsEditMode { get; set; } = false;
	public string DisplayCategoryName { get; set; }

	private string _oldName;
	private string _oldDescription;
	public TaskDetailPopup(MyTask task, string categoryName)
	{
		InitializeComponent();

		DisplayCategoryName = categoryName;
		BindingContext = task;
		OnPropertyChanged(nameof(DisplayCategoryName));
	}

	private void OnEditSaveClicked(object sender, EventArgs e)
	{
		var task = BindingContext as MyTask;

		if (!IsEditMode)
		{
			_oldName = task.TaskName;
			_oldDescription = task.Description;

			IsEditMode = true;
			EditSaveBtn.Text = "Save";
			EditSaveBtn.BackgroundColor = Colors.Orange;
		}
		else
		{
			Close(task);
		}
		OnPropertyChanged(nameof(IsEditMode));
	}

	private void OnCancelClicked(object sender, EventArgs e)
	{
		var task = BindingContext as MyTask;

		task.TaskName = _oldName;
		task.Description = _oldDescription;

		IsEditMode = false;
		EditSaveBtn.Text = "Edit";
		EditSaveBtn.BackgroundColor = Color.FromArgb("#5E66FF");

		OnPropertyChanged(nameof(IsEditMode));
	}

	private void OnCloseClicked(object sender, EventArgs e) => Close();

}