using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavascaBasTaskerApp.MVVM.Models;
using NavascaBasTaskerApp.MVVM.Views;
using PropertyChanged;

namespace NavascaBasTaskerApp.MVVM.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class AddTaskViewModel
	{
		private readonly CategoryService _categoryService;

		// Binds to the shared singleton list
		public ObservableCollection<Category> Categories => _categoryService.Categories;

		// Auto-properties are now sufficient! Fody handles the rest.
		public Category? SelectedCategory { get; set; }
		public string TaskName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public ICommand AddCategoryCommand { get; }
		public ICommand AddTaskCommand { get; }

		// Constructor name matches class name
		public AddTaskViewModel(CategoryService categoryService)
		{
			_categoryService = categoryService;

			AddCategoryCommand = new Command(OnAddCategory);
			AddTaskCommand = new Command(OnAddTask);
		}

		private async void OnAddCategory()
		{
			var popup = new NavascaBasTaskerApp.MVVM.Views.CategoryPopup();

			if (Application.Current?.MainPage != null)
			{
				var result = await Application.Current.MainPage.ShowPopupAsync(popup);

				// Success: result is the anonymous object { Name, Color }
				if (result != null)
				{
					// Cast to dynamic to access the properties easily
					dynamic data = result;

					string name = data.Name;
					string color = data.Color;

					if (!string.IsNullOrWhiteSpace(name))
					{
						_categoryService.AddCategory(name, color);
					}
				}
			}
		}
		private async void OnAddTask()
		{
			if (string.IsNullOrWhiteSpace(TaskName) || SelectedCategory == null)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Missing information", "OK");
				return;
			}

			// Your logic to save the task to the service would go here
			await Shell.Current.GoToAsync("..");
		}

	}
}

