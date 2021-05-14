using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public class LaboresRealizadas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        
        private List<Insumos> mInsumos = new List<Insumos>();
        public List<Insumos> Insumos
        {
            get { return mInsumos; }
            set { mInsumos = value; }
        }
    }
}
