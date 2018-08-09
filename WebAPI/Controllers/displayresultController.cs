using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
     [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class displayresultController : ApiController
    {
         SaveDisplayResultDB db = new SaveDisplayResultDB();

         // GET api/recipe
        [HttpGet]
        [Route("api/displayrecipe/Index")]
        public IEnumerable<Displaydata> Get(int Id)
        {
            return db.GetDisplayResult(Id);
                  
        }        

        // POST api/displayresult
        [HttpPost]
        [Route("api/displayrecipe/Create")]
        public int Post([FromBody]string query, [FromUri()]int Id)
        {
            return db.SaveQuery(query, Id);
        }

        // PUT api/displayresult/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/displayresult/5
        public void Delete(int id)
        {
        }
    }
}
