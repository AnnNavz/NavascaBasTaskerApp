using NavascaBasTaskerApp.Views;
using System.Threading.Tasks;

namespace NavascaBasTaskerApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

		private async void AddTask_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTask());
		}
	}
}
