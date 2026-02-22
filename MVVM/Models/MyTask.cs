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
	public class MyTask : INotifyPropertyChanged
	{
		public string TaskName { get; set; }
		public bool Completed { get; set; }
		public int CategoryId { get; set; }
		public string TaskColor { get; set; }

		public string Description { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

	}
}
