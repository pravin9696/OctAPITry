using OctAPITry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OctAPITry.Controllers
{
    public class GTHAPIController : ApiController
    {
        //reading GET
        [HttpGet]
        public IHttpActionResult getLoginDetails()
        {
            var dbo = new DB_authentication_authorisation_Oct_210725Entities();
            var lglist = dbo.tblLogins.ToList<tblLogin>();
            
            List<loginTemp> lgTempList= lglist.Select(x => new loginTemp() { id = x.id, UserName = x.UserName, Password = x.Password }).ToList<loginTemp>();

            return Ok(lgTempList);
        }
       
    }
}
