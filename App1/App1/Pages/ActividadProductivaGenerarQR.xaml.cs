using Approagro.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ImageFromXamarinUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadProductivaGenerarQR : ContentPage
    {

        private readonly string QRStringValue = "APROAGRO|";
        private readonly string FileTmpName = "APROAGRO_QRCode.jpg";
        private ActividadProductiva actividad;
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
            QR.BarcodeValue = $"{QRStringValue}{actividad.NombreActividad}|{actividad.IdActividad}";
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

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FileTmpName);
            using (var fileStream = File.Create(path))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }


        /// <summary>
        /// Based on the implementation of https://www.youtube.com/watch?v=O9D3NSYh1t0
        /// uses ImageFromXamarinUI nuget
        /// </summary>
        async void PartialCaptureScreenshot()
        {
            try
            {
                var screenshotStream = await QR.CaptureImageAsync();
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FileTmpName);
                using (var fileStream = File.Create(path))
                {
                    screenshotStream.Seek(0, SeekOrigin.Begin);
                    screenshotStream.CopyTo(fileStream);
                }
            }
            catch
            {
                throw new Exception("No fue posible generar el código QR");
            }
        }

        /// <summary>
        /// Share the QR code generated
        /// </summary>
        private async void ShareQR()
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), FileTmpName);
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Código QR",
                    File = new ShareFile(path)
                });
            }catch
            {
                throw new Exception("No fue posible exportar el código QR");
            }
        }

        private async void CreateAndShareQR()
        {
            try
            {
                PartialCaptureScreenshot();
                ShareQR();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}