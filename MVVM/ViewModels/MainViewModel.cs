using NavascaBasTaskerApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace NavascaBasTaskerApp.MVVM.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class MainViewModel
	{
		private readonly CategoryService _categoryService;

		// Binds to the shared singleton list
		public ObservableCollection<Category> Categories => _categoryService.Categories;
		public ObservableCollection<MyTask> Tasks => _categoryService.AllTasks;

		// Any new properties you add here will automatically notify the UI
		public string UserName { get; set; } = "Marven James Bas";

		public MainViewModel(CategoryService categoryService)
		{
			_categoryService = categoryService;
			Tasks.Add(new MyTask { TaskName = "Complete UI Design", TaskColor = "#ff6f87", Completed = false });
		}




	}
}
