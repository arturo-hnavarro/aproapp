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
    public partial class LaboresRealizadas : ContentPage
    {
        ActividadProductiva actividadProductiva;
        public LaboresRealizadas()
        {
            InitializeComponent();
        }

        public LaboresRealizadas(ActividadProductiva actividad)
        {
            InitializeComponent();
            actividadProductiva = actividad;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                MyListView.ItemsSource = await GetLaboresRealizadas();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", ex.Message, "Aceptar");
            }
        }

        async void OnListViewItemSelected(object sender, EventArgs e)
        {
            if (MyListView.SelectedItem != null)
            {
                LaborRealizada labor = (LaborRealizada)MyListView.SelectedItem;
                await DisplayAlert("Descripción", labor.Observaciones, "Aceptar");
            }
        }

        async void OnRegisterLaborClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LaboresRealizadasAdd(actividadProductiva));
        }

        private Task<List<LaborRealizada>> GetLaboresRealizadas()
        {
            return App.AproagroDB.GetLaboresRealizadasByActividadProductiva(actividadProductiva.IdActividad);
        }

        
    }
}