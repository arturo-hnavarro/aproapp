using App1.Dao;
using App1.Domain;
using App1.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace App1
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
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
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

        async void OnAddActivityClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRegistrar());
        }

        async void OnAddTipoActivityClick(Object sender, EventArgs e)
        {
            //TODO: Add logic
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
                    ActividadProductiva a = GetInfoFromQR(result.Text);
                    DisplayAlert("Valor Obtenido", CreateMessage(a), "OK");
                    ScheduleMessage("APROAGRO", $"Proximo mantenimiento se acerca:\nNombreActividadRaiz: {a.NombreActividadRaiz}\nActividad: {a.IdActividad}");
                });
            };

            await Navigation.PushAsync(scannerPage);
        }

        private ActividadProductiva GetInfoFromQR(string value)
        {
            CodigoDao codigos = new CodigoDao();
            return codigos.GetInfo(value);
        }

        private string CreateMessage(ActividadProductiva a)
        {
            return "Actividad raiz: " + a.NombreActividadRaiz + ". Actividad: " + a.IdActividad + ". Ultima actualizacion: " + a.UltimaActualizacion;
        }

    }
}
