using Approagro.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ImageFromXamarinUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadProductivaGenerarQR : ContentPage
    {

        private readonly ActividadProductiva actividad;
        public ActividadProductivaGenerarQR()
        {
            InitializeComponent();
        }
        public ActividadProductivaGenerarQR(ActividadProductiva actividadProductiva)
        {
            InitializeComponent();
            actividad = actividadProductiva;
            GenerateQR();
        }

        /// <summary>
        /// Generate QR Code using ZXwing library from Nuget
        /// https://www.youtube.com/watch?v=Kkub8wWf6HA
        /// </summary>
        private void GenerateQR()
        {
            QR.BarcodeValue = QRStringValue(actividad);
            Label_QR_Nombre.Text = $"Actividad: {actividad.NombreActividad}";
            Label_QR_Ubicacion.Text = $"Ubicación: {actividad.Ubicacion}";
        }
        void OnExportarClick(object sender, EventArgs e)
        {
            try
            {
                CreateAndShareQR();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }


        /// <summary>
        /// Method from xamarin docs available on https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid
        /// </summary>
        /// <returns></returns>
        async Task CaptureScreenshot()
        {
            var screenshot = await Screenshot.CaptureAsync();
            var stream = await screenshot.OpenReadAsync();

            //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FileTmpName);
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), actividad.NombreActividad + ".jpg");
                using (var fileStream = File.Create(path))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", $"Ha ocurrido un error: {ex.Message}", "Aceptar");
            }

        }


        /// <summary>
        /// Based on the implementation of https://www.youtube.com/watch?v=O9D3NSYh1t0
        /// uses ImageFromXamarinUI nuget
        /// </summary>
        async Task PartialCaptureScreenshot()
        {
            try
            {
                var screenshotStream = await QRWrapper.CaptureImageAsync();
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), actividad.NombreActividad + ".jpg");
                using (var fileStream = File.Create(path))
                {
                    screenshotStream.Seek(0, SeekOrigin.Begin);
                    screenshotStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", $"Ha ocurrido un error: {ex.Message}", "Aceptar");
            }
        }

        /// <summary>
        /// Share the QR code generated
        /// </summary>
        private async Task ShareQR()
        {
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), actividad.NombreActividad + ".jpg");
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Código QR",
                    File = new ShareFile(path)
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", $"Ha ocurrido un error: {ex.Message}", "Aceptar");
            }
        }

        private async void CreateAndShareQR()
        {
            try
            {
                await PartialCaptureScreenshot();
                await ShareQR();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string QRStringValue(ActividadProductiva actividadProductiva)
        {
            ActividadProductivaQR qr = new ActividadProductivaQR
            {
                IdActividad = actividad.IdActividad,
                NombreActividad = actividad.NombreActividad,
                Descripcion = actividad.Descripcion,
                Fk_TipoActividad = actividad.Fk_TipoActividad
            };
            return JsonConvert.SerializeObject(qr);
        }
    }
}