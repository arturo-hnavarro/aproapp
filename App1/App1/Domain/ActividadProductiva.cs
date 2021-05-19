using Approagro.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public class ActividadProductiva
    {
        [PrimaryKey, AutoIncrement]
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde
        public int Fk_TipoActividad { get; set; }

        private TipoActividad mTipoActividad = new TipoActividad();
        [Ignore]
        public TipoActividad TipoActividad
        {
            get { return mTipoActividad; }
            set { mTipoActividad = value; }
        }
        public string Ubicacion { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public DateTime ProximaAplicacion { get; set; }

        private List<LaboresRealizadas> mLabores = new List<LaboresRealizadas>();
        [Ignore]
        public List<LaboresRealizadas> LaboresRealizadas
        {
            get { return mLabores; }
            set { mLabores = value; }
        }
    }


}
