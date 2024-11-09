using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int Codigo_producto{ set; get; }
        public string Nombre{ set; get; }
        public string Descripcion{ set; get; }
        public bool Unidad_medida{ set; get; }
        public decimal Costo{ set; get; }
        public decimal Precio_unitario{ set; get; }
        public DateTime FechaRegistro { set; get; }
        public DateTime FechaModificacion { set; get; }
        public DateTime FechaInicio { set; get; }
        public DateTime FechaFin { set; get; }
        public decimal Existencia{ set; get; }
        public decimal Vendido { set; get; }
        public decimal Merma { set; get; }
        public bool Stock{ set; get; }
        public decimal Punto_reorden{ set; get; }
        public decimal Descuento { set; get; }
        public Departamento oDepartamento{ set; get; }
        public int IdAdmin { set; get; }
        public string NombreAdminMod { set; get; }
    }
}
