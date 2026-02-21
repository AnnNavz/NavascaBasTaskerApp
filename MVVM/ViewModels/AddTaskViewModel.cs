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

		public ObservableCollection<Category> Categories => _categoryService.Categories;

		public Category? SelectedCategory { get; set; }
		public string TaskName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public ICommand AddCategoryCommand { get; }
		public ICommand AddTaskCommand { get; }

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

				if (result != null)
				{
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
				await Application.Current.MainPage.DisplayAlert("Error", "Missing Info", "OK");
				return;
			}

			// 1. Increment count
			SelectedCategory.PendingTasks++;

			// 2. Create object
			var newTask = new MyTask
			{
				TaskName = this.TaskName,
				Completed = false,
				CategoryId = SelectedCategory.Id,
				TaskColor = SelectedCategory.Color
			};

			// 3. IMPORTANT: Add it to the shared service list!
			_categoryService.AllTasks.Add(newTask);

			await Application.Current.MainPage.Navigation.PopAsync();
		}

	}
}

