using BackEndCoreAgencia.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BackEndCoreAgencia.Controllers
{
    public class CompararDatosController : ApiController
    {
        //FUNCIONA ALGO
        //    [HttpGet]
        //    [Route("api/comparacion")]
        //    public IHttpActionResult GetColumnData()
        //    {
        //        Console.WriteLine("Hola");
        //        List<string> columnData = new List<string>();
        //        string connectionString = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            //Aqui me quede, ahora toca agregar el parametro para que revise por email
        //            string query = "SELECT c.nombre, p.nombre FROM Viaje v INNER JOIN Categoria c ON v.idViaje = c.idCategoria_Viaje INNER JOIN Agencia a ON v.idViaje = a.idAgencia_Viaje INNER JOIN Cliente cl ON a.idAgencia_Cliente = cl.idCliente INNER JOIN Preferencia p ON cl.idCliente = p.idPreferencia_Cliente";
        //            SqlCommand command = new SqlCommand(query, connection);

        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                string data1 = reader.GetString(0); // Obtén los datos de la primera columna en la posición 0
        //                string data2 = reader.GetString(1); // Obtén los datos de la segunda columna en la posición 1

        //                if (data1 == data2)
        //                {
        //                    columnData.Add(data1);
        //                    Console.WriteLine("Similitud encontrada: " + data1);
        //                }
        //            }

        //            reader.Close();
        //        }

        //        return Ok(columnData);
        //    }
        //}



        //    string connectionString = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

        //    [HttpGet]
        //    [Route("api/comparacion")]
        //    public IHttpActionResult GetColumnData()
        //    {
        //        List<string> columnData = new List<string>();
        //        List<Viaje> viajeData = new List<Viaje>();

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            string query = "SELECT c.nombre, p.nombre, v.* FROM Viaje v INNER JOIN Categoria c ON v.idViaje = c.idCategoria_Viaje INNER JOIN Agencia a ON v.idViaje = a.idAgencia_Viaje INNER JOIN Cliente cl ON a.idAgencia_Cliente = cl.idCliente INNER JOIN Preferencia p ON cl.idCliente = p.idPreferencia_Cliente";
        //            SqlCommand command = new SqlCommand(query, connection);

        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                // Obtener los datos de la primera columna en la posición 0
        //                string data1 = reader.GetString(0);
        //                // Obtener los datos de la segunda columna en la posición 1
        //                string data2 = reader.GetString(1);

        //                string viajeData1 = reader.GetString(2); // Obtener los datos de la tabla "Viaje" en la posición 2 (o ajusta el índice según la estructura de tu consulta)

        //                foreach (char c1 in data1)
        //                {
        //                    foreach (char c2 in data2)
        //                    {
        //                        // Buscar similitud entre los caracteres de ambas columnas
        //                        if (c1 == c2)
        //                        {
        //                            columnData.Add(data1);
        //                            Console.WriteLine("Similitud encontrada: " + data1);
        //                        }
        //                    }
        //                }

        //                // Agregar los datos de la tabla "Viaje" a la lista viajeData
        //                viajeData.Add(viajeData);
        //            }

        //            reader.Close();
        //        }

        //        // Crear un objeto anónimo que contenga los datos de comparación y los datos de la tabla "Viaje"
        //        var result = new { Comparacion = columnData, Viaje = viajeData };

        //        // Devolver el resultado como una respuesta HTTP 200 (OK) en formato JSON
        //        return Ok(result);
        //    }
        //}



        //    [HttpPut]
        //    [Route("api/comparacion")]
        //    public async Task<IHttpActionResult> GetColumnDataByEmail(string email)
        //    {
        //        string emailContent = await Request.Content.ReadAsStringAsync();

        //        List<string> columnData = new List<string>();
        //        List<Viaje> viajeData = new List<Viaje>(); // Lista para almacenar los datos de la tabla "Viaje"

        //        // Establecer la conexión con la base de datos
        //        string connectionString = ConfigurationManager.ConnectionStrings["BDLocal"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            // Consulta SQL para obtener los datos de comparación y de la tabla "Viaje" según el email
        //            string query = @"SELECT c.nombre, p.nombre, v.*
        //                FROM Viaje v
        //                INNER JOIN Categoria c ON v.idViaje = c.idCategoria_Viaje
        //                INNER JOIN Agencia a ON v.idViaje = a.idAgencia_Viaje
        //                INNER JOIN Cliente cl ON a.idAgencia_Cliente = cl.idCliente
        //                INNER JOIN Preferencia p ON cl.idCliente = p.idPreferencia_Cliente
        //                WHERE cl.email = @Email";
        //            SqlCommand command = new SqlCommand(query, connection);
        //            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

        //            await connection.OpenAsync();

        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    // Obtener los datos de la primera columna en la posición 0
        //                    string data1 = reader.GetString(0);
        //                    // Obtener los datos de la segunda columna en la posición 1
        //                    string data2 = reader.GetString(1);

        //                    // Buscar similitud entre los caracteres de ambas columnas
        //                    if (HasSimilarity(data1, data2))
        //                    {
        //                        columnData.Add(data1);
        //                        Console.WriteLine("Similitud encontrada: " + data1);
        //                    }


        //                    string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

        //                    using (SqlConnection conn = new SqlConnection(strConn))
        //                    {
        //                        conn.Open();

        //                        SqlCommand cmd = conn.CreateCommand();
        //                        cmd.CommandText = "Viajes_all";
        //                        cmd.CommandType = CommandType.StoredProcedure;

        //                        SqlDataReader dr = cmd.ExecuteReader();

        //                        while (dr.Read())
        //                        {
        //                            int id = dr.GetInt32(0);
        //                            string nombre = dr.GetString(1).Trim();
        //                            int duracion = dr.GetInt32(2);
        //                            string descripcion = dr.GetString(3).Trim();
        //                            decimal precio = dr.GetDecimal(4);
        //                            string imagen = dr.GetString(5).Trim();

        //                            Viaje viaje = new Viaje(id, nombre, duracion, descripcion, precio, imagen);

        //                            viajeData.Add(viaje);
        //                        }

        //                        dr.Close();
        //                        conn.Close();
        //                    }

        //                    return viajeData;
        //                }

        //            }
        //        }

        //        // Crear un objeto anónimo que contenga los datos de comparación y los datos de la tabla "Viaje"
        //        var result = new { Comparacion = columnData, Viaje = viajeData };

        //        // Enviar el correo electrónico usando el contenido proporcionado en el parámetro emailContent
        //        // Puedes implementar el código para enviar el correo aquí

        //        // Devolver el resultado como una respuesta HTTP 200 (OK) en formato JSON
        //        return Ok(result);
        //    }

        //    private bool HasSimilarity(string data1, string data2)
        //    {
        //        foreach (char c1 in data1)
        //        {
        //            foreach (char c2 in data2)
        //            {
        //                // Buscar similitud entre los caracteres de ambas columnas
        //                if (c1 == c2)
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //        return false;
        //    }

        /////////////////////////////////ESTE ES EL CORE
        [HttpPost]
        [Route("api/comparacion")]
        public IHttpActionResult AutenticarUsuario(Cliente cliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Cliente WHERE email = @Usuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = cliente.email;

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int count) && count > 0)
                        {
                            // Autenticación exitosa, ahora obtén los datos relacionados del cliente
                            query = @"SELECT p.nombre, c.nombre, v.idViaje, v.nombre, v.duracion, v.descripcion, v.precio, v.imagen
                               FROM Viaje v
                               INNER JOIN Categoria c ON v.idViaje = c.idCategoria_Viaje
                               INNER JOIN Agencia a ON v.idViaje = a.idAgencia_Viaje
                               INNER JOIN Cliente cl ON a.idAgencia_Cliente = cl.idCliente
                               INNER JOIN Preferencia p ON cl.idCliente = p.idPreferencia_Cliente
                               WHERE cl.idCliente = @ClienteId";
                            using (SqlCommand relatedCommand = new SqlCommand(query, connection))
                            {
                                relatedCommand.Parameters.Add("@ClienteId", SqlDbType.Int).Value = cliente.idCliente;
                                List<string> listaPreferencia = new List<string>();
                                List<string> listaCategoria = new List<string>();
                                List<int> listaIdViaje = new List<int>();
                                List<string> listaNombreViaje = new List<string>();
                                List<int> listaDuracion = new List<int>();
                                List<string> listaDescripcion = new List<string>();
                                List<decimal> listaPrecio = new List<decimal>();
                                List<string> listaImagen = new List<string>();

                                using (SqlDataReader reader = relatedCommand.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string preferenciaNombre = reader.GetString(0).Trim();
                                        string categoriaNombre = reader.GetString(1).Trim();
                                        int idViaje = reader.GetInt32(2);
                                        string nombreViaje = reader.GetString(3).Trim();
                                        int duracion = reader.GetInt32(4);
                                        string descripcion = reader.GetString(5).Trim();
                                        decimal precio = reader.GetDecimal(6);
                                        string imagen = reader.GetString(7).Trim();

                                        listaPreferencia.Add(preferenciaNombre);
                                        listaCategoria.Add(categoriaNombre);
                                        listaIdViaje.Add(idViaje);
                                        listaNombreViaje.Add(nombreViaje);
                                        listaDuracion.Add(duracion);
                                        listaDescripcion.Add(descripcion);
                                        listaPrecio.Add(precio);
                                        listaImagen.Add(imagen);
                                    }
                                }

                                List<string> nombresSimilares = listaPreferencia.Intersect(listaCategoria).ToList();

                                List<Viaje> listaViajes = new List<Viaje>();
                                for (int i = 0; i < listaIdViaje.Count; i++)
                                {
                                    Viaje viaje = new Viaje(listaIdViaje[i], listaNombreViaje[i], listaDuracion[i], listaDescripcion[i], listaPrecio[i], listaImagen[i]);
                                    listaViajes.Add(viaje);
                                }

                                return Ok(new { message = "Autenticación exitosa", listaPreferencia, listaCategoria, nombresSimilares, listaViajes });
                            }
                        }
                    }
                }

                return BadRequest("Usuario o contraseña incorrectos");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        ///////////////////////
    }
}

