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

            /*Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;*/
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
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

        async void OnShowTipoActivityClick(object sender, EventArgs e)
        {
            try
            {

                
                
                 /*   await App.AproagroDB.SaveTipoActividadAsync(new TipoActividad
                    {
                        Nombre = Name.Trim(),
                        Descripcion = Description.Trim()
                    });
                
                */
            }
            catch
            {
            }
        }

        
    }
}
