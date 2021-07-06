using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Approagro.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListaActividadesProductivas : ContentPage
    {
        public PageListaActividadesProductivas()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            myListView.ItemsSource = await GetListAsync();
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(myListView.SelectedItem != null)
            {
                ActividadProductiva actividad = (ActividadProductiva)e.SelectedItem;
                await Navigation.PushAsync(new ActividadProductivaDetail(actividad));
            }
        }
        private async Task<List<ActividadProductiva>> GetListAsync()
        {
            try
            {
                return await App.AproagroDB.GetActividadesProductivasAsync();
            }
            catch
            {
                await DisplayAlert("Ups", "Ocurrió un error al consultar las actividades productivas registradas. Por favor intente de nuevo", "Aceptar");

                return null;
            }
        }
    }
}