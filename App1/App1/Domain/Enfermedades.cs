using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public class Enfermedades
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Fk_ActividadProductiva { get; set; }
        public string Nombre { get; set; }
        public string Observacion { get; set; }
    }
}
