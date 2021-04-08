using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;//namespace for WEB API
using APIExample.Models;
using System.Web.Http.Cors;
/// <summary>
/// http get ,post,put etc are called action verbs
/// </summary>
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
        //to Call stored procedure
        [Route("api/ProjectInfo/UpdateProject/{pid}")]
        [HttpPut]
        public bool Put(int pid, [FromBody]ProjectInfo pinfo)//using from body when the object comes from body
         //pid is passed through url but projectinfo value got from body  
        {
            //calling a stored Procedure
            try
            {
                int res = db.sp_UpdateProject(pid, pinfo.projname, pinfo.domain);
                if (res > 0)
                    return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
        [Route("api/ProjectInfo/SelectProjByID/{id}")]//<-- this variable
        [HttpGet]
        public sp_SelectProjectbyId_Result Get(int ? id)//<-- and this variable should be same
        {//sp..._result returns the value of sp
            try
            {
                var res = db.sp_SelectProjectbyId(id).SingleOrDefault();
                if (res == null)
                    throw new Exception("Invalid Project ID");
                else
                    return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
