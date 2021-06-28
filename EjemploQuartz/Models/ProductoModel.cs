using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Models
{
    public class ProductoModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public ProductoModel(string nombre, string descripcion, double precio)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }
    }        
}
