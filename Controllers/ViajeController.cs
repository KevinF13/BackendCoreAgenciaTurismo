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
    public class ViajeController : ApiController
    {

        // GET: api/Viaje
        [HttpGet]
        [Route("api/Viaje")]
        public IEnumerable<Viaje> Get()
        {
            GestorViajes gViajes = new GestorViajes();
            return gViajes.GetViajes();
        }

        // GET: api/Viaje/5
        [HttpGet]
        [Route("api/Viaje/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Viaje
        [HttpPost]
        [Route("api/Viaje/insertar")]
        public bool Post([FromBody]Viaje viaje)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.addViajes(viaje);

            return res;
        }

        // PUT: api/Agencia/5
        [HttpPut]
        [Route("api/Viaje/actualizar/{id}")]
        public bool Put(int id, [FromBody] Viaje viaje)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.updateViajes(id, viaje);

            return res;
        }

        // DELETE: api/Agencia/5
        [HttpDelete]
        [Route("api/Viaje/eliminar/{id}")]
        public bool Delete(int id)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.deleteViajes(id);

            return res;
        }

        ///     TABLA CATEGORIA
        ///     INGRESO
        ///     GET



        [HttpPost]
        [Route("api/Viaje/categoria/insertar")]
        public bool RegistrarCategoria(Categoria categoria)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = "IF EXISTS (SELECT * FROM Viaje WHERE idViaje = @Id) " +
                               "BEGIN " +
                               "   INSERT INTO Categoria VALUES (@Id, @Nombre) " +
                               "   SELECT 1 " +
                               "END " +
                               "ELSE " +
                               "   SELECT 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", categoria.idCategoria_Viaje);
                    command.Parameters.AddWithValue("@Nombre", categoria.nombre);

                    connection.Open();

                    int result = (int)command.ExecuteScalar();

                    return result > 0;
                }
            }
        }







        [HttpGet]
        [Route("api/Viaje/categoria")]
        public List<Categoria> GetCategorias()
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = "SELECT * FROM Categoria";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCategoria = reader.GetInt32(0);
                            int idCategoria_viaje = reader.GetInt32(1);
                            string nombre = reader.GetString(2).Trim();

                            Categoria categoria = new Categoria(idCategoria, idCategoria_viaje, nombre);

                            categorias.Add(categoria);
                        }
                    }
                }
            }

            return categorias;
        }
    }
}
