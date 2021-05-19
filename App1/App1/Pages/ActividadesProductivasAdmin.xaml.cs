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
    public partial class ActividadesProductivasAdmin : ContentPage
    {
        public ActividadesProductivasAdmin()
        {
            InitializeComponent();
        }

        async void OnAddActivityClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActividadesProductivasAdd());
        }

    }
}