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
    public partial class PageAdministration : ContentPage
    {
        public PageAdministration()
        {
            InitializeComponent();
        }

        async void OnAddTipoActivityClick(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageRegistrarTipo());
        }
    }
}