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
    public partial class EnfermedadesAdd : ContentPage
    {
        private ActividadProductiva actividadProductiva;
        private List<Enfermedad> enfermedades;

        public EnfermedadesAdd()
        {
            InitializeComponent();
            if (enfermedades == null)
                enfermedades = new List<Enfermedad>();
        }
        public EnfermedadesAdd(ActividadProductiva actividad)
        {
            InitializeComponent();
            actividadProductiva = actividad;
            if (enfermedades == null)
                enfermedades = new List<Enfermedad>();
        }

        async void RegistrarEnfermedad(object sender, EventArgs e)
        {
            
            try
            {
                enfermedades.Add(Mapper());
                if (await DisplayAlert("Enfermedad agregada", "Enfermedad agregada ¿Desea registrar otra?", "Sí", "No"))
                {
                    CleanEntry();
                }
                else
                {
                    App.AproagroDB.SaveEnferdadesListAsync(enfermedades);
                    await DisplayAlert("Actualizado", "Enfermedades registradas", "Aceptar");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        private Enfermedad Mapper()
        {
            return new Enfermedad
            {
                Observacion = EntryObservacion.Text,
                Nombre = EntryNombre.Text,
                Fk_ActividadProductiva = actividadProductiva.IdActividad,
                Fecha = DateTime.Now
            };
        }
        private void CleanEntry()
        {
            EntryNombre.Text = ""; 
            EntryObservacion.Text= "";   
        }
    }
}