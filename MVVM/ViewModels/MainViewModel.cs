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



		public ObservableCollection<Category> Categories => _categoryService.Categories;
		public ObservableCollection<MyTask> Tasks => _categoryService.AllTasks;

		public ObservableCollection<MyTask> FilteredTasks { get; set; } = new();

		public string FilterHeader { get; set; } = "Pending Tasks"; 

		public string StatusFilter { get; set; } = "All";

		public ICommand ToggleStatusFilterCommand => new Command(() =>
		{
			if (StatusFilter == "All") StatusFilter = "Pending";
			else if (StatusFilter == "Pending") StatusFilter = "Done";
			else StatusFilter = "All";

			RefreshFilteredList();
		});

		public ICommand FilterTasksCommand => new Command<Category>((category) =>
		{
			if (category == null) return;

			var query = _categoryService.AllTasks.Where(t => t.CategoryId == category.Id);

			if (StatusFilter == "Pending")
				query = query.Where(t => !t.Completed);
			else if (StatusFilter == "Done")
				query = query.Where(t => t.Completed);

			FilteredTasks.Clear();
			foreach (var task in query)
			{
				FilteredTasks.Add(task);
			}
		});

		public void RefreshFilteredList()
		{
			var query = Tasks.AsEnumerable();

			if (StatusFilter == "Pending")
			{
				query = query.Where(t => !t.Completed);
				FilterHeader = "Pending Tasks";
			}
			else if (StatusFilter == "Done")
			{
				query = query.Where(t => t.Completed);
				FilterHeader = "Completed Tasks";
			}
			else
			{
				FilterHeader = "All Tasks";
			}

			FilteredTasks.Clear();
			foreach (var task in query)
			{
				FilteredTasks.Add(task);
			}
		}

		public string UserName { get; set; } = "Marven James Bas";

		public MainViewModel(CategoryService categoryService)
		{
			_categoryService = categoryService;


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

			Tasks.CollectionChanged += (s, e) => RefreshFilteredList();

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
				category.UpdateProgress(Tasks);

				category.PendingTasks = Tasks.Count(t => t.CategoryId == category.Id && !t.Completed);
			}
		}

		public void UpdateCategoryProgress(Category category)
		{
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

			var category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
			string catName = category?.CategoryName ?? "General";

			var popup = new TaskDetailPopup(task, catName);

			await Application.Current.MainPage.ShowPopupAsync(popup);

			RecalculateAll();
		});

		public ICommand EditCategoryCommand => new Command<Category>(async (category) =>
		{
			if (category == null) return;

			string result = await Application.Current.MainPage.DisplayPromptAsync(
				"Edit Category",
				"Enter new category name:",
				initialValue: category.CategoryName);

			if (!string.IsNullOrWhiteSpace(result))
			{
				category.CategoryName = result;
			}
		});


	}
}
