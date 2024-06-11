using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ahorcado_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Ahorcado_Xamarin.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
