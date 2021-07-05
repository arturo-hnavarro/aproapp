using Approagro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadProductivaDetail : ContentPage
    {
        private ActividadProductiva actividadProductiva;
        private INotificationManager notificationManager;
        public ActividadProductivaDetail()
        {
            InitializeComponent();
            notificationManager = DependencyService.Get<INotificationManager>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.actividadProductiva = App.AproagroDB.GetActividadProductivaAsync(actividadProductiva.IdActividad).Result;
            InitializeValues();
        }
        public ActividadProductivaDetail(ActividadProductiva actividad)
        {
            InitializeComponent();
            this.actividadProductiva = actividad;
            InitializeValues();
            notificationManager = DependencyService.Get<INotificationManager>();
        }

        async void ToListLabores(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new LaboresRealizadas(actividadProductiva));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        async void ToListEnfermedades(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new EnfermedadesRegistradas(actividadProductiva));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        async void ProgrmarNotificacion(object sender, EventArgs e)
        {
            try
            {
                actividadProductiva.ProximaAplicacion = ActivityProximaActualizacion.Date;
                await App.AproagroDB.SaveActividadProductivaAsync(actividadProductiva);
                if(GenerateNotification(ActivityProximaActualizacion.Date))
                    await DisplayAlert("Notificaciónes", "Alerta programada correctamente.", "Aceptar.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }


        async void ToQRCode(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActividadProductivaGenerarQR(actividadProductiva));
        }
        private void InitializeValues()
        {
            EntryActivityName.Text = actividadProductiva.NombreActividad;
            EntryActivityDescription.Text = actividadProductiva.Descripcion;
            EntryActivityLocation.Text = actividadProductiva.Ubicacion;
            EntryTipoActividad.Text = actividadProductiva.TipoActividad.Descripcion;

            if (actividadProductiva.UltimaActualizacion.Year.ToString() != "1")
            {
                ActivityUltimaActualizacion.Date = actividadProductiva.UltimaActualizacion;
            }

            if (actividadProductiva.ProximaAplicacion.Year.ToString() != "1")
            {
                ActivityProximaActualizacion.Date = actividadProductiva.ProximaAplicacion;
            }
        }

        private bool GenerateNotification(DateTime date)
        {
            try
            {
                string title = "Fecha de mantenimiento se apróxima";
                string message = $"Actividad: {actividadProductiva.NombreActividad}, requiere su atención.";
                //notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
                notificationManager.SendNotification(title, message, DateTime.Parse(date.ToString()).AddDays(-1));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}