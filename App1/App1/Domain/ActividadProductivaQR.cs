using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public class ActividadProductivaQR
    {
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; } 
        public string Descripcion { get; set; }
        public int Fk_TipoActividad { get; set; }
    }
}
