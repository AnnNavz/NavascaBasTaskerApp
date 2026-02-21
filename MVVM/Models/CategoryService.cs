using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavascaBasTaskerApp.MVVM.Models;

namespace NavascaBasTaskerApp.MVVM.Models
{
	public class CategoryService
	{
		public ObservableCollection<Category> Categories { get; } = new()
		{
			new Category { CategoryName = ".NET MAUI Course", Color = "#ff6f87" },
			new Category { CategoryName = "Tutorials", Color = "#76ceff" }
		};

		public void AddCategory(string name, string color)
		{
			Categories.Add(new Category
			{
				CategoryName = name,
				Color = color,
				PendingTasks = 0
			});
		}

		public ObservableCollection<MyTask> AllTasks { get; set; } = new();

	}
}
