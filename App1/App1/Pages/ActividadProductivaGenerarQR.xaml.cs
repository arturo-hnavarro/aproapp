using Approagro.Domain;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        
        private readonly string QRStringValue = "APROAGRO|";
        public ActividadProductivaGenerarQR()
        {
            InitializeComponent();
        }
        public ActividadProductivaGenerarQR(ActividadProductiva actividadProductiva)
        {
            InitializeComponent();
            QR.BarcodeValue = $"{QRStringValue}{actividadProductiva.NombreActividad}|{actividadProductiva.IdActividad}";
            //https://www.youtube.com/watch?v=Kkub8wWf6HA
        }
        /*protected override async void OnAppearing()
        {
            base.OnAppearing();
            /*
             * 
            QRCodeGenerator qRCode = new QRCodeGenerator();
            QRCodeData data = qRCode.CreateQrCode(Id.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode newCode = new QRCode(data);

            Bitmap bitmap = newCode.GetGraphic(5);
            //            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QRCode_temp.jpg");

            newCode.GetGraphic(5)
        }*/

        async void OnExportarClick(object sender, EventArgs e)
        {

        }
    }
}