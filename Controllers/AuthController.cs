using BackEndCoreAgencia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEndCoreAgencia.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/Auth/login")]
        public IHttpActionResult AutenticarUsuario(Cliente cliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Cliente WHERE email = @Usuario AND contrasena = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = cliente.email;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = cliente.contrasena;

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int count) && count > 0)
                        {
                            return Ok(new { message = "Autenticación exitosa" });
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
    }



}
