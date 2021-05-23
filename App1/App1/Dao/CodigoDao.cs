using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ZXing;
//using Android.Graphics;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using System.Linq;
using Approagro.Domain;
using Newtonsoft.Json;

namespace Approagro.Dao
{
    public class CodigoDao
    {

        public ActividadProductiva GetInfo(string value)
        {
            ActividadProductiva actividadProductiva;
            try
            {
                ActividadProductivaQR qrObject = JsonConvert.DeserializeObject<ActividadProductivaQR>(value);
                actividadProductiva = App.AproagroDB.GetActividadProductivaAsyncByIdByNombre(qrObject.IdActividad, qrObject.NombreActividad).Result;
                if (actividadProductiva == null)
                    throw new Exception();
            }
            catch
            {
                throw new Exception("No fue posible obtener información de la actividad productiva");
            }
            return actividadProductiva;
        }
    }
}
