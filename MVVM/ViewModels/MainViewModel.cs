using CommunityToolkit.Maui.Views;
using NavascaBasTaskerApp.MVVM.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavascaBasTaskerApp.MVVM.Views;

namespace NavascaBasTaskerApp.MVVM.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class MainViewModel
	{
		private readonly CategoryService _categoryService;

		// Binds to the shared singleton list
		public ObservableCollection<Category> Categories => _categoryService.Categories;
		public ObservableCollection<MyTask> Tasks => _categoryService.AllTasks;

		public ObservableCollection<MyTask> FilteredTasks { get; set; } = new();

		public ICommand FilterTasksCommand => new Command<Category>((category) =>
		{
			if (category == null) return;

			var filtered = _categoryService.AllTasks
				.Where(t => t.CategoryId == category.Id)
				.ToList();

			FilteredTasks.Clear();
			foreach (var task in filtered)
			{
				FilteredTasks.Add(task);
			}
		});

		public void RefreshFilteredList()
		{
			FilteredTasks.Clear();
			foreach (var task in Tasks)
			{
				FilteredTasks.Add(task);
			}
		}

		// Any new properties you add here will automatically notify the UI
		public string UserName { get; set; } = "Marven James Bas";

		public MainViewModel(CategoryService categoryService)
		{
			_categoryService = categoryService;


			// 1. Listen for new tasks being added/removed
			Tasks.CollectionChanged += (s, e) =>
			{
				RecalculateAll();
				if (e.NewItems != null)
				{
					foreach (MyTask task in e.NewItems)
						task.PropertyChanged += (sender, args) => { if (args.PropertyName == "Completed") RecalculateAll(); };
				}
			};

			RefreshFilteredList();

			// Ensure the UI updates when tasks are added or removed
			Tasks.CollectionChanged += (s, e) => RefreshFilteredList();

			// 2. Initial hook for existing tasks
			foreach (var task in Tasks)
			{
				task.PropertyChanged += (s, e) => { if (e.PropertyName == "Completed") RecalculateAll(); };
			}

			RecalculateAll();
		}

		public void RecalculateAll()
		{
			foreach (var category in Categories)
			{
				// 1. Update the Progress Bar percentage
				category.UpdateProgress(Tasks);

				// 2. Update the Pending Tasks count
				// We count only tasks that belong to this category AND are NOT completed
				category.PendingTasks = Tasks.Count(t => t.CategoryId == category.Id && !t.Completed);
			}
		}

		public void UpdateCategoryProgress(Category category)
		{
			// Get all tasks belonging to this category from the shared service
			var categoryTasks = _categoryService.AllTasks.Where(t => t.CategoryId == category.Id).ToList();

			if (categoryTasks.Count == 0)
			{
				category.Percentage = 0;
				return;
			}

			float completedTasks = categoryTasks.Count(t => t.Completed);
			category.Percentage = completedTasks / categoryTasks.Count;
		}

		public ICommand EditTaskCommand => new Command<MyTask>(async (task) =>
		{
			if (task == null) return;

			// Get the category name for display
			var category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
			string catName = category?.CategoryName ?? "General";

			var popup = new TaskDetailPopup(task, catName);

			// Show the popup
			await Application.Current.MainPage.ShowPopupAsync(popup);

			// Recalculate progress in case the name/status changed
			RecalculateAll();
		});


	}
}
