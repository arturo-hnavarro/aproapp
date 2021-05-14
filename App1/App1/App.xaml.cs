using App1.Dao;
using Approagro.Dao;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        static TipoActividadDao TipoActividadService;

        public static TipoActividadDao TiposActividades
        {
            get
            {
                if (TipoActividadService == null)
                {
                    TipoActividadService = new TipoActividadDao(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TiposActividades.db3"));
                }
                return TipoActividadService;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            CodigoDao codigo = new CodigoDao();
            codigo.AgregarValoresDePrueba();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
