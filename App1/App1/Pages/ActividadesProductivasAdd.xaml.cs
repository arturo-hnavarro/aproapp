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
    public partial class ActividadesProductivasAdd : ContentPage
    {
        public ActividadesProductivasAdd()
        {
            InitializeComponent();
        }

        async void OnRegisterActivityClick(object sender, EventArgs e)
        {
            try
            {
                ActividadProductiva result = await App.AproagroDB.SaveActividadProductivaAsync(Mapper());
                if (result != null)
                {
                    bool answer = await DisplayAlert("Actividad registrada", "¿Desea generar código QR?", "Sí", "Después");
                    if (answer)
                    {
                        await Navigation.PushAsync(new ActividadProductivaGenerarQR(result));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        private ActividadProductiva Mapper()
        {
            ActividadProductiva actividadProductiva = new ActividadProductiva
            {
                NombreActividad = EntryActivityName.Text,
                Ubicacion = EntryActivityLocation.Text,
                Descripcion = EntryActivityDescription.Text,
                Fk_TipoActividad = 1
            };

            if (string.IsNullOrWhiteSpace(actividadProductiva.NombreActividad))
            {
                throw new ArgumentException("Nombre no puede ser nulo o vacío.");
            }

            if (string.IsNullOrWhiteSpace(actividadProductiva.Ubicacion))
            {
                throw new ArgumentException("Debe definir la ubicación donde realiza la actividad");
            }

            return actividadProductiva;
        }
    }
}