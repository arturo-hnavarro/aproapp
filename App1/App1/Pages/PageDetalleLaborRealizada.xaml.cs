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
    public partial class PageDetalleLaborRealizada : ContentPage
    {
        private readonly LaborRealizada labor;
        public PageDetalleLaborRealizada(LaborRealizada labor)
        {
            InitializeComponent();
            this.labor = labor;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            myListView.ItemsSource = labor.Insumos;
            descripcionLabel.Text = labor.Observaciones;
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Insumos insumo = (Insumos)e.SelectedItem;
            if (e.SelectedItem != null)
            {
                await DisplayAlert("Insumo", $"Nombre: {insumo.Nombre}\n\rObservación: {insumo.Observacion}\n\rPrecio: {insumo.PrecioTotal}", "Aceptar");
            }
        }
    }
}