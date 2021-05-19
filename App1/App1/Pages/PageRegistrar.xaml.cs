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
    public partial class PageRegistrar : ContentPage
    {
        public PageRegistrar()
        {
            InitializeComponent();
        }

        async void OnRegisterActivityClick(object sender, EventArgs e)
        {
            string valueText = EntryActivityName.Text;
        }

        private void GenerateQRCode(string valueText)
        {

        }


    }
}