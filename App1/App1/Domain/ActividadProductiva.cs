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
        [NotNull, Unique]
        public string NombreActividad { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde
        public string Descripcion { get; set; }
        [NotNull]
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

        private List<LaborRealizada> mLabores = new List<LaborRealizada>();
        [Ignore]
        public List<LaborRealizada> LaboresRealizadas
        {
            get { return mLabores; }
            set { mLabores = value; }
        }

        private List<Enfermedades> mEnfermedades = new List<Enfermedades>();
        [Ignore]
        public List<Enfermedades> Enfermedades
        {
            get { return mEnfermedades; }
            set { mEnfermedades = value; }
        }
    }


}
