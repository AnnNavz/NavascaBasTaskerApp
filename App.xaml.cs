using Microsoft.Extensions.DependencyInjection;
using NavascaBasTaskerApp.MVVM.Views;
using NavascaBasTaskerApp.Views;
using System;

namespace NavascaBasTaskerApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

			var mainPage = serviceProvider.GetService<NavascaBasTaskerApp.MVVM.Views.MainView>();

			MainPage = new NavigationPage(mainPage);
		}

        protected override Window CreateWindow(IActivationState? activationState)
        {
			Window window = base.CreateWindow(activationState);
			window.Width = 420;
			window.Height = 800;

			return window;
		}
    }
}