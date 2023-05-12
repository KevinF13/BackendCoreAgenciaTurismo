using BackEndCoreAgencia.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
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
        public IEnumerable<Viaje> Get()
        {
            GestorViajes gViajes = new GestorViajes();
            return gViajes.GetViajes();
        }

        // GET: api/Viaje/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Viaje
        public bool Post([FromBody]Viaje viaje)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.addViajes(viaje);

            return res;
        }

        // PUT: api/Agencia/5
        public bool Put(int id, [FromBody] Viaje viaje)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.updateViajes(id, viaje);

            return res;
        }

        // DELETE: api/Agencia/5
        public bool Delete(int id)
        {
            GestorViajes gViajes = new GestorViajes();
            bool res = gViajes.deleteViajes(id);

            return res;
        }
    }
}
