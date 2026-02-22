using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavascaBasTaskerApp.MVVM.Models
{

	[AddINotifyPropertyChangedInterface]
	public class Category : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public string Color { get; set; }
		public int PendingTasks { get; set; }
		public float Percentage { get; set; }
		public bool IsSelected { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public void UpdateProgress(IEnumerable<MyTask> allTasks)
		{
			var categoryTasks = allTasks.Where(t => t.CategoryId == this.Id).ToList();

			if (categoryTasks.Count == 0)
			{
				Percentage = 0;
				return;
			}

			float completedCount = categoryTasks.Count(t => t.Completed);
			Percentage = completedCount / categoryTasks.Count;
		}
	}
}
