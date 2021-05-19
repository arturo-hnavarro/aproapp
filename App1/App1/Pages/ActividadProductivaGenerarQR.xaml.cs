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
    public partial class ActividadProductivaGenerarQR : ContentPage
    {
        private int Id;
        public ActividadProductivaGenerarQR()
        {
            InitializeComponent();
        }
        public ActividadProductivaGenerarQR(int id)
        {
            Id = id;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}