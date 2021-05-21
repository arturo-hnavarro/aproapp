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
    public partial class ViewHTML : ContentPage
    {
        public ViewHTML()
        {
            InitializeComponent();


            webView.Source = "https://dotnet.microsoft.com/apps/xamarin";


        }

    }
}