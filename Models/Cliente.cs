using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndCoreAgencia.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }

        public string contrasena { get; set; }

        public Cliente()
        {

        }
        public Cliente(int IdCliente, string Nombre, string Apellido, string Cedula, string Celular, string Direccion, string Email, string Contrasena)
        {
            idCliente = IdCliente;
            nombre = Nombre;
            apellido = Apellido;
            cedula = Cedula;
            celular = Celular;
            direccion = Direccion;
            email = Email;
            contrasena = Contrasena;
        }
        public Cliente( string Email, string Contrasena)
        {
            email = Email;
            contrasena = Contrasena;
        }
    }
}