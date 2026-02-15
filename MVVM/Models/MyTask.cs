using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavascaBasTaskerApp.MVVM.Models
{
	public class MyTask
	{
		public string TaskName { get; set; }

		public string TaskDescription { get; set; }
		public bool Completed { get; set; }
		public int CategoryId { get; set; }
		public string TaskColor { get; set; }
	}
}
