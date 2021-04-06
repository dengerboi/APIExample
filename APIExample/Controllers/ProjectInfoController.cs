using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;//namespace for WEB API
using APIExample.Models;
using System.Web.Http.Cors;

namespace APIExample.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/ProjectInfo")]
    public class ProjectInfoController : ApiController // inherits from ApiContoller
    {
        LTIMVCEntities db = new LTIMVCEntities();
        [Route("api/ProjectInfo/GetProjects")]
        [HttpGet]
        public IEnumerable<ProjectInfo> Get()
        {
            try
            {
                return db.ProjectInfoes.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
