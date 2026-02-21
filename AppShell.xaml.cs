using NavascaBasTaskerApp.Views;

namespace NavascaBasTaskerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
			Routing.RegisterRoute(nameof(AddTask), typeof(AddTask));
		}
    }
}
