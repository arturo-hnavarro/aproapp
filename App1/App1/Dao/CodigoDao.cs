﻿using System;
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
                NombreActividadRaiz = "Siembra cebolla",
                IdActividad = "1",
                UltimaActualizacion = DateTime.Now.ToString()
            };
            actividades.Add(a1);

            actividades.Add(new ActividadProductiva
            {
                NombreActividadRaiz = "Siembra lechuga",
                IdActividad = "2",
                UltimaActualizacion = DateTime.Now.ToString()
            });

            actividades.Add(new ActividadProductiva
            {
                NombreActividadRaiz = "Siembra papas",
                IdActividad = "3",
                UltimaActualizacion = DateTime.Now.ToString()
            });

        }
       
        public ActividadProductiva GetInfo(string value)
        {
            actividades.Where(a => a.NombreActividadRaiz == value.Split('|')[0] && a.IdActividad == value.Split('|')[1]).FirstOrDefault().
                UltimaActualizacion = DateTime.Now.ToString();

            return actividades.Where(a => a.NombreActividadRaiz == value.Split('|')[0] && a.IdActividad == value.Split('|')[1]).FirstOrDefault();
        }
    }
}
