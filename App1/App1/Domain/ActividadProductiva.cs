using Approagro.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Domain
{
    public class ActividadProductiva
    {
        public string NombreActividadRaiz { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde
        public int IdActividad { get; set; }
        public TipoActividad TipoActividad { get; set; }
        public string Ubicacion { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public DateTime ProximaAplicacion { get; set; }

        private List<LaboresRealizadas> mLabores = new List<LaboresRealizadas>();
        public List<LaboresRealizadas> LaboresRealizadas
        {
            get { return mLabores; }
            set { mLabores = value; }
        }
    }
}
