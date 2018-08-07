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
    public class addrecipeController : ApiController
    {
         dbAddRecipe objdbcall = new dbAddRecipe();

        // GET api/recipe
        [HttpGet]
        [Route("api/addrecipe/Index")]
        public IEnumerable<attributecolval> Get()
        {
            return objdbcall.GetColNamesForRecipe();
        }

        // GET api/addrecipe/5
        [HttpGet]
        [Route("api/addrecipe/Index")]
        public IEnumerable<string> Get(string colname)
        {
            return objdbcall.GetColValuesForRecipe(colname); ;
        }
      
        [HttpPost]
        [Route("api/addrecipe/Save")]
        public int Post([FromBody]AddRecipe[] value)
        {
            return objdbcall.SaveReceipeDetails(value);
        }

        // PUT api/addrecipe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/addrecipe/5
        public void Delete(int id)
        {
        }
    }
}
