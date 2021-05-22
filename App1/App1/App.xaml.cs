using Approagro.Dao;
using System;
using System.IO;
using Xamarin.Forms;

namespace Approagro
{
    public partial class App : Application
    {
        static AproagroContextService AproagroDBContext;

        public static AproagroContextService AproagroDB
        {
            get
            {
                if (AproagroDBContext == null)
                {
                    AproagroDBContext = new AproagroContextService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "APROAGRO.db3"));
                }
                return AproagroDBContext;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
