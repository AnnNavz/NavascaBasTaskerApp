using CommunityToolkit.Maui.Views;
namespace NavascaBasTaskerApp.MVVM.Views;

public partial class ManageCategoryPopup : Popup
{
	public ManageCategoryPopup()
	{
		InitializeComponent();
	}

	private void OnEditClicked(object sender, EventArgs e) => Close("edit");
	private void OnDeleteClicked(object sender, EventArgs e) => Close("delete");
	private void OnCancelClicked(object sender, EventArgs e) => Close(null);
}