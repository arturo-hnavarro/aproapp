using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTiposActividadesAdmin : ContentPage
    {
        public PageTiposActividadesAdmin()
        {
            InitializeComponent();
        }

        async void OnAddTipoActivityClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRegistrarTipo());
        }

        async void OnShowTipoActivityClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TipoActividadListar());
        }
    }
}