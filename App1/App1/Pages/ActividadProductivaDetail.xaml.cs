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
        public ActividadProductivaDetail()
        {
            InitializeComponent();
        }

        public ActividadProductivaDetail(ActividadProductiva actividad)
        {
            InitializeComponent();
            actividadProductiva = actividad;
            InitializeValues();
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

            if (actividadProductiva.UltimaActualizacion.TimeOfDay.ToString() !=   "00:00:00")
            {
                ActivityUltimaActualizacion.Date = actividadProductiva.UltimaActualizacion;
            }

            if (actividadProductiva.ProximaAplicacion.TimeOfDay.ToString() != "00:00:00")
            {
                ActivityProximaActualizacion.Date = actividadProductiva.ProximaAplicacion;
            }
        }

    }
}