using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Domain
{
    public class ActividadProductiva
    {
        public string NombreActividadRaiz { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde
        public string IdActividad { get; set; } //lote de cafe, lote de lechugas 1
        public string UltimaActualizacion { get; set; }
    }
}
