using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndCoreAgencia.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }

        public int idCategoria_Viaje { get; set; }

        public String nombre { get; set; }

        public Categoria()
        {

        }

        public Categoria(int IdCategoria, int IdCategoria_Viaje, String Nombre)
        {
            idCategoria = IdCategoria;
            idCategoria_Viaje = IdCategoria_Viaje;
            nombre = Nombre;
        }

        public Categoria(int IdCategoria_Viaje, String Nombre)
        {
            idCategoria_Viaje = IdCategoria_Viaje;
            nombre = Nombre;
        }
    }
}