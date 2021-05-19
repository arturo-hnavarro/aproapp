using Approagro.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipoActividadListar : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public TipoActividadListar()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                MyListView.ItemsSource = await GetListAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", ex.Message, "Aceptar");
            }
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void OnListViewItemSelected(object sender, EventArgs e)
        {
            if (MyListView.SelectedItem != null)
            {
                TipoActividad actividad = (TipoActividad) MyListView.SelectedItem;
                await DisplayAlert("Message", actividad.Descripcion, "Aceptar");
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
