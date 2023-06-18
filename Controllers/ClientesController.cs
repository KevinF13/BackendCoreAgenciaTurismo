using BackEndCoreAgencia.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEndCoreAgencia.Controllers
{
    [EnableCors]
    public class ClientesController : ApiController
    {
        string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
        //private string connectionString = "TuCadenaDeConexion"; // Reemplaza con tu cadena de conexión a la base de datos

        [HttpPost]
        [Route("api/Cliente/insertar")]
        public bool RegisterUser(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string selectQuery = "SELECT COUNT(*) FROM Cliente WHERE email = @Email";
                string insertQuery = "INSERT INTO Cliente (nombre, apellido, cedula, celular, direccion, email, contrasena) VALUES (@Nombre, @Apellido, @Cedula, @Celular, @Direccion, @Email, @Contrasena)";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Email", cliente.email);

                    connection.Open();

                    int existingEmailCount = (int)selectCommand.ExecuteScalar();

                    if (existingEmailCount > 0)
                    {
                        // El correo ya existe, puedes mostrar un mensaje de error o realizar alguna acción adicional.
                        return false;
                    }
                }

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Nombre", cliente.nombre);
                    insertCommand.Parameters.AddWithValue("@Apellido", cliente.apellido);
                    insertCommand.Parameters.AddWithValue("@Cedula", cliente.cedula);
                    insertCommand.Parameters.AddWithValue("@Celular", cliente.celular);
                    insertCommand.Parameters.AddWithValue("@Direccion", cliente.direccion);
                    insertCommand.Parameters.AddWithValue("@Email", cliente.email);
                    insertCommand.Parameters.AddWithValue("@Contrasena", cliente.contrasena);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }



        [HttpGet]
        [Route("api/Cliente")]
        public List<Cliente> GetRegisteredUsers()
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            List<Cliente> registeredUsers = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = "SELECT * FROM Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCliente = reader.GetInt32(0);
                            string nombre = reader.GetString(1).Trim();
                            string apellido = reader.GetString(2).Trim();
                            string cedula = reader.GetString(3).Trim();
                            string celular = reader.GetString(4).Trim();
                            string direccion = reader.GetString(5).Trim();
                            string email = reader.GetString(6).Trim();
                            string contrasena = reader.GetString(7).Trim();

                            Cliente cliente = new Cliente(idCliente, nombre, apellido, cedula, celular, direccion, email, contrasena);

                            registeredUsers.Add(cliente);
                        }
                    }
                }
            }

            return registeredUsers;
        }


        [HttpPost]
        [Route("api/Cliente/preferencia/insertar")]
        public bool RegistrarPreferencia(Preferencia preferencia)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = "IF EXISTS (SELECT * FROM Cliente WHERE idCliente = @Id) " +
                               "BEGIN " +
                               "   INSERT INTO Preferencia VALUES (@Id, @Nombre) " +
                               "   SELECT 1 " +
                               "END " +
                               "ELSE " +
                               "   SELECT 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", preferencia.idPreferencia_Cliente);
                    command.Parameters.AddWithValue("@Nombre", preferencia.nombre);

                    connection.Open();

                    int result = (int)command.ExecuteScalar();

                    return result > 0;
                }
            }
        }


        [HttpGet]
        [Route("api/Cliente/preferencia")]
        public List<Preferencia> GetPreferencia()
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            List<Preferencia> preferencias = new List<Preferencia>();

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = "SELECT * FROM Preferencia";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPreferencia = reader.GetInt32(0);
                            int idPreferencia_Cliente = reader.GetInt32(1);
                            string nombre = reader.GetString(2).Trim();

                            Preferencia preferencia = new Preferencia(idPreferencia, idPreferencia_Cliente, nombre);

                            preferencias.Add(preferencia);
                        }
                    }
                }
            }

            return preferencias;
        }



        //-------------------------------------------------------------------//
        //CREACION DE LOGIN DE CLIENTES
        [HttpPost]
        [Route("api/Cliente/login")]
        public bool AuthenticateUser(Cliente cliente)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Cliente WHERE email = @Email AND contrasena = @Contrasena";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", cliente.email);
                    command.Parameters.AddWithValue("@Contrasena", cliente.contrasena);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }



}

