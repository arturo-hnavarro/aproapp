using Approagro.Domain;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRegistrarTipo : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public PageRegistrarTipo()
        {
            InitializeComponent();
        }

        async void OnRegisterTypeActivityClick(object sender, EventArgs e)
        {
            try
            {
                string Name = EntryActivityName.Text;
                string Description = EntryActivityDescription.Text;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    await App.AproagroDB.SaveTipoActividadAsync(new TipoActividad
                    {
                        Nombre = Name,
                        Descripcion = Description
                    });
                }
                else
                {
                    await DisplayAlert("Registrar tipo de actividad", "El nombre del tipo de actividad es requerido", "Aceptar");
                }
            }
            catch
            {
                await DisplayAlert("Error al registrar tipo de actividad", "Ocurrió un error al registrar el tipo de actividad. Por favor intente de nuevo", "Intentar de nuevo");
            }
        }
    }
}
