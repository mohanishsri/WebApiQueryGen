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
    public class attributeController : ApiController
    {
        attributedbcalls objdbcall = new attributedbcalls();

        // GET api/recipe
        [HttpGet]
        [Route("api/attribute/Index")]
        public IEnumerable<string> Get()
        {
            return objdbcall.GetTablesName();
        
        }             

        public IEnumerable<ColName> Get(string tablename, int ID)
        {
            return objdbcall.GetColNames(tablename, ID);
        }
    }
}
