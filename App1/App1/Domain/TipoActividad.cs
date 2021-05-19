using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Approagro.Domain
{
    public class TipoActividad
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int IdActividad { get; set; } //lote de cafe, lote de lechugas 1
        [NotNull]
        public string Nombre { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde
        public string Descripcion { get; set; } //ej siembra de cafe, siembra zanahorias, siembra de lechugas, Cria de cerdos, Gallinas de engorde

    }
}
