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
    public class addattributeController : ApiController
    {
        AddNewAttribute db = new AddNewAttribute();
        [HttpGet]
        [Route("api/addattribute/Index")]
        public IEnumerable<attributecolval> Get()
        {
            return db.GetColumnValues();
        }

        // GET api/recipe
        [HttpGet]
        [Route("api/addattribute/Index")]
        public IEnumerable<attributecolval> Get(string colname)
        {
            return db.GetColumnValues(colname);
        }

        // GET api/addattribute/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/recipe
        [HttpPost]
        [Route("api/addattribute/Create")]
        public int Post([FromBody]attributecolval value)
        {
            return 1;
        }

        [HttpPost]
        [Route("api/addattribute/Save")]
        public int Post([FromBody]attributecolval[] value, [FromUri()]string colname)
        {
            return db.SaveAttributeValue(value, colname);
        }
       
        // PUT api/addattribute/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/addattribute/5
        public void Delete(int id)
        {
        }
    }
}
