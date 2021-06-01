using Approagro.Pages;
using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Approagro.Domain;
using Approagro.Dao;

namespace Approagro
{
    public partial class MainPage : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;

        public MainPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void OnSendClick(object sender, EventArgs e)
        {
            string title = "Fecha de mantenimiento se apróxima";
            string message = "Actividad [[NOMBRE]] que requiere su atención.";
            notificationManager.SendNotification(title, message);
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            string title = "Fecha de mantenimiento se apróxima";
            string message = "Actividad [[NOMBRE]] que requiere su atención.";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(600));
        }

        void ScheduleMessage(string title, string message)
        {
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProcessNotification(title, message);
            });

        }

        private async void ProcessNotification(string title, string message)
        {
            try
            {
                bool checkActividad = await DisplayAlert("Atención", $"{title}\n{message}", "Ir a actividad productiva", "Cancelar");
                if (checkActividad)
                {
                    await Navigation.PushAsync(new ActividadProductivaDetail(GetActividadProductiva(GetNombreActividadFromNotification(message))));
                }
            }
            catch
            {
                await DisplayAlert("Ups", "No fue posible obtener el detalle de la actividad productiva", "Aceptar");
            }
        }

        async void OnScanClick(Object sender, EventArgs e)
        {
            try
            {
                Scanner();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "OK");
            }

        }

        async void OnSeeActivityClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageListaActividadesProductivas());
        }

        async void GoToSubMenuAdmin(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageAdministration());
        }

        private async void Scanner()
        {
            var scannerPage = new ZXingScannerPage();

            scannerPage.Title = "Lector de QR";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    GetInfoFromQR(result.Text);
                });
            };

            await Navigation.PushAsync(scannerPage);
        }

        async void GetInfoFromQR(string value)
        {
            try
            {
                CodigoDao codigos = new CodigoDao();
                await Navigation.PushAsync(new ActividadProductivaDetail(codigos.GetInfo(value)));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        private string GetNombreActividadFromNotification(string notificationMessage)
        {
            return notificationMessage.Split(':')[1].Split(',')[0].Trim();
        }

        private ActividadProductiva GetActividadProductiva(string nombre)
        {
            return App.AproagroDB.GetActividadProductivaByNombreAsync(nombre).Result;
        }
        private void GoToActividad(string nombre)
        {
            Navigation.PushAsync(new ActividadProductivaDetail());
        }
        async void OnSignIn(object sender, EventArgs e)
        {
            try
            {
                await (Application.Current as App).SignIn();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Authentication Error", ex.Message, "OK");
            }
        }
        async void OnSignOut(object sender, EventArgs e)
        {
            var signout = await DisplayAlert("Salir?", "Desea cerrar la sesión?", "Sí", "No");
            if (signout)
            {
                await (Application.Current as App).SignOut();
            }
        }
    }
}
