using Proyecto.Pages;
using Proyecto.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using MasterDetailPage = Xamarin.Forms.MasterDetailPage;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace Proyecto
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());//Habilitar la Navegacion
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
