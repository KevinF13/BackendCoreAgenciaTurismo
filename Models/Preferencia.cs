using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndCoreAgencia.Models
{
    public class Preferencia
    {
            public int idPreferencia { get; set; }

            public int idPreferencia_Cliente { get; set; }

            public String nombre { get; set; }

            public Preferencia()
            {

            }

            public Preferencia(int IdPreferencia, int IdPreferencia_Cliente, String Nombre)
            {
                IdPreferencia = idPreferencia;
                IdPreferencia_Cliente = idPreferencia_Cliente;
                nombre = Nombre;
            }

            public Preferencia(int IdPreferencia_Cliente, String Nombre)
            {
                idPreferencia_Cliente = IdPreferencia_Cliente;
                nombre = Nombre;
            }

            public Preferencia(String Nombre)
            {
                nombre = Nombre;
            }
    }
}