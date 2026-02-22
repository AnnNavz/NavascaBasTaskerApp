using CommunityToolkit.Maui.Views;
using System.Collections.Generic;

namespace NavascaBasTaskerApp.MVVM.Views
{
	public partial class CategoryPopup : Popup
	{
		
		public CategoryPopup()
		{
			InitializeComponent();

			var colors = new List<string>
	{
		"#ff6f87", "#76ceff", "#61da49", "#ffbd00", "#f38029", "#923be1"
	};

			ColorCollectionView.ItemsSource = colors;
			ColorCollectionView.SelectedItem = colors[0];

			CategoryEntry.TextChanged += (s, e) =>
			{
				if (e.NewTextValue.Length > 20)
				{
					ErrorLabel.IsVisible = true;
				}
				else
				{
					ErrorLabel.IsVisible = false;
				}
			};
		}

		void OnSaveClicked(object sender, EventArgs e)
		{
			Close(CategoryEntry.Text);
		}

		private void OnAdd_Clicked(object sender, EventArgs e)
		{
			var selectedColor = ColorCollectionView.SelectedItem as string ?? "#5E66FF";

			if (!string.IsNullOrWhiteSpace(CategoryEntry.Text) && CategoryEntry.Text.Length <= 20)
			{
				Close(new { Name = CategoryEntry.Text, Color = selectedColor });
			}
			else
			{
				ErrorLabel.IsVisible = true;
				if (string.IsNullOrWhiteSpace(CategoryEntry.Text))
				{
					ErrorLabel.Text = "Category name cannot be empty.";
				}
			}
		}

		
	}
}