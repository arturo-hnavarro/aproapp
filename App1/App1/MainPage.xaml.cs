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
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message);
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
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
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
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
                    //ScheduleMessage("APROAGRO", $"Proximo mantenimiento se acerca:\nNombreActividad: {a.NombreActividad}\nActividad: {a.IdActividad}");
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

        private string CreateMessage(ActividadProductiva a)
        {
            return "Actividad raiz: " + a.NombreActividad + ". Actividad: " + a.IdActividad + ". Ultima actualizacion: " + a.UltimaActualizacion;
        }

    }
}
