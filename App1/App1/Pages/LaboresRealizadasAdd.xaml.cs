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
    public partial class LaboresRealizadasAdd : ContentPage
    {
        ActividadProductiva actividadProductiva;
        public LaboresRealizadasAdd()
        {
            InitializeComponent();
        }

        public LaboresRealizadasAdd(ActividadProductiva actividad)
        {
            InitializeComponent();
            actividadProductiva = actividad;
        }

        async void RegistrarInsumos(object sender, EventArgs e)
        {
        }

        async void RegistrarLabor(object sender, EventArgs e)
        {
            LaborRealizada respuesta = App.AproagroDB.SaveLaborRealizadaAsync(await Mapper()).Result;
            if (respuesta != null)
            {
                actividadProductiva.LaboresRealizadas.Add(respuesta);
                bool add = await DisplayAlert("Labor registrada", "Labor registrada correctamente. ¿Desea registrar Insumos utilizados?", "Sí", "No");
                if (add)
                {
                    await Navigation.PushAsync(new InsumosAdd(respuesta), true);
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }

        private async Task<LaborRealizada> Mapper()
        {
            return new LaborRealizada
            {
                Observaciones = EntryObservacion.Text,
                FK_ActividadProductiva = actividadProductiva.IdActividad,
                Fecha = FechaDeActividad.Date
            };
        }
    }
}