using SQLite;
using System;
using System.Collections.Generic;


namespace Approagro.Domain
{
    public class LaborRealizada
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int FK_ActividadProductiva { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

        private List<Insumos> mInsumos = new List<Insumos>();
        [Ignore]
        public List<Insumos> Insumos
        {
            get { return mInsumos; }
            set { mInsumos = value; }
        }
    }
}
