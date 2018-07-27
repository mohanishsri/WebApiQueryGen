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
    public class recipeController : ApiController
    {

        databasecalls objdbcall = new databasecalls();  

        // GET api/recipe
        [HttpGet]
        [Route("api/recipe/Index")]
        public IEnumerable<receipemaster> Get()
        {
            return objdbcall.GetReceipeMaster();
        }

        // GET api/recipe/5

        // GET api/recipe/5
        //[HttpGet]
        //[Route("api/recipe/Search/{searchvalues}/{id}")]
        //public IEnumerable<receipemaster> Get(string searchvalues, int id)
        //{
        //    return objdbcall.GetReceipeMaster();
        //}

       
        public IEnumerable<receipemaster> Get(string searchvalues, int id)
        {
            return objdbcall.SearchReceipe(searchvalues);
        
        }
        
        public string stringFindByName(string name)
        {
            return "value" + name;
        }

      

        // POST api/recipe
        [HttpPost]
        [Route("api/recipe/Create")] 
        public int Post([FromBody]receipemaster value)
        {
            return objdbcall.SaveReceipeMaster(value);
        }

        // PUT api/recipe/5
        [HttpPut]
        [Route("api/recipe/Edit")]
        public int Put([FromBody]receipemaster value)
        {
            return objdbcall.UpdateReceipeMaster(value);  
        }

        // DELETE api/recipe/5
        [HttpDelete]
        [Route("api/recipe/Delete/{id}")]
        public int Delete(int id)
        {
            return objdbcall.deleteReceipeMaster(id);
        }
    }
}
