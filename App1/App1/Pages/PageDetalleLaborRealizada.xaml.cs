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
            descripcionLabel.Text = "Observaciones: " +labor.Observaciones;
            fechaContent.Text = "Fecha: " + labor.Fecha.ToString("dd/MM/yyyy");
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Insumos insumo = (Insumos)e.SelectedItem;
            if (e.SelectedItem != null)
            {
                await DisplayAlert("Insumo", $"Nombre: {insumo.Nombre}\nCantidad: {insumo.CantidadUsada}\nPrecio: {insumo.PrecioTotal}\nObservaciones: {insumo.Observacion}", "Aceptar");
            }
        }
    }
}