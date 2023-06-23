using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndCoreAgencia.Models
{
    public class Viaje
    {
        public int idViaje { get; set; }
        public string nombre { get; set; }
        public int duracion { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }

        public Viaje() { }

        public Viaje(int id, string Nombre, int Duracion, string Descripcion, decimal Precio, string Imagen)
        {
            idViaje = id;
            nombre = Nombre;
            duracion = Duracion;
            descripcion = Descripcion;
            precio = Precio;
            imagen = Imagen;
        }

        public Viaje(string Nombre, int Duracion, string Descripcion, decimal Precio, string Imagen)
        {
            nombre = Nombre;
            duracion = Duracion;
            descripcion = Descripcion;
            precio = Precio;
            imagen = imagen;
        }
        public Viaje(string Nombre)
        {
            nombre = Nombre;
        }
    }
}