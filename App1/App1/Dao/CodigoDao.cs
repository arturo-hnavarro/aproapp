using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ZXing;
//using Android.Graphics;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using App1.Domain;
using System.Linq;

namespace App1.Dao
{
    public class CodigoDao
    {
        public static List<ActividadProductiva> actividades = new List<ActividadProductiva>();

        //public static ScanQRCode(string valueText)
        //{
        //    //BarcodeWriter br = new BarcodeWriter();
        //    //Bitmap bm = new Bitmap(br.Encode(valueText));
        //    var ScannerPage = new ZXingScannerPage();

        //    ScannerPage.Title = "Lector de QR";
        //    ScannerPage.OnScanResult += (result) =>
        //    {
        //        ScannerPage.IsScanning = false;
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            Navigation.PopAsyn
        //        });
        //    }
        //}

        public void AgregarValoresDePrueba()
        {
            ActividadProductiva a1 = new ActividadProductiva
            {
                NombreActividad = "Siembra cebolla",
                IdActividad = 1,
                UltimaActualizacion = DateTime.Now
            };
            actividades.Add(a1);

            actividades.Add(new ActividadProductiva
            {
                NombreActividad = "Siembra lechuga",
                IdActividad = 2,
                UltimaActualizacion = DateTime.Now
            });

            actividades.Add(new ActividadProductiva
            {
                NombreActividad = "Siembra papas",
                IdActividad = 3,
                UltimaActualizacion = DateTime.Now
            });

        }
       
        public ActividadProductiva GetInfo(string value)
        {
            actividades.Where(a => a.IdActividad == int.Parse(value)).FirstOrDefault().UltimaActualizacion = DateTime.Now;

            return actividades.Where(a => a.IdActividad == int.Parse(value)).FirstOrDefault();
        }
    }
}
