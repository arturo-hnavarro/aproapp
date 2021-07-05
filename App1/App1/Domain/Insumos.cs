using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public class Insumos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Fk_LaborRealizada { get; set; }
        public string Nombre { get; set; }
        public double CantidadUsada { get; set; }
        public double PrecioTotal { get; set; }
        public string Observacion { get; set; }
    }
}
