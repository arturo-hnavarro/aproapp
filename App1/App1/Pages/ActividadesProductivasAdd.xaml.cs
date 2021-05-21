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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            PickerActivity.ItemsSource = await ActivityList();
        }
        private async Task<List<string>> ActivityList()
        {
            List<TipoActividad> list = await GetListAsync();
            List<string> listReturn = new List<string>();
            foreach (TipoActividad actividad in list)
            {
                listReturn.Add(actividad.Nombre);
            }
            return listReturn;
        }

        async void OnRegisterActivityClick(object sender, EventArgs e)
        {
            try
            {
                ActividadProductiva result = await App.AproagroDB.SaveActividadProductivaAsync(await Mapper());
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

        private async Task<ActividadProductiva> Mapper()
        {
            TipoActividad tipoActividad = await SelectedActivity();
            ActividadProductiva actividadProductiva = new ActividadProductiva
            {
                NombreActividad = EntryActivityName.Text,
                Ubicacion = EntryActivityLocation.Text,
                Descripcion = EntryActivityDescription.Text,
                Fk_TipoActividad = tipoActividad.IdActividad
            };

            if (string.IsNullOrWhiteSpace(actividadProductiva.NombreActividad))
            {
                throw new ArgumentException("Nombre no puede ser nulo o vacío.");
            }

            if (string.IsNullOrWhiteSpace(actividadProductiva.Ubicacion))
            {
                throw new ArgumentException("Debe definir la ubicación donde realiza la actividad");
            }
            if (PickerActivity.SelectedIndex == -1)
            {
                throw new ArgumentException("Debe definir el tipo de actividad relacionada");
            }

            return actividadProductiva;
        }

        private async Task<TipoActividad> SelectedActivity()
        {
            try
            {
                string eleccion = (string)PickerActivity.SelectedItem;
                return await App.AproagroDB.GetTipoActividadAsync(eleccion);
            }
            catch
            {
                await DisplayAlert("Ups", "Ocurrió un error al consultar los tipos de actividades registradas. Por favor intente de nuevo", "Aceptar");

                return null;
            }

        }
        private async Task<List<TipoActividad>> GetListAsync()
        {
            try
            {
                return await App.AproagroDB.GetTipoActividadesAsync();
            }
            catch
            {
                await DisplayAlert("Ups", "Ocurrió un error al consultar los tipos de actividades registradas. Por favor intente de nuevo", "Aceptar");

                return null;
            }
        }
    }
}