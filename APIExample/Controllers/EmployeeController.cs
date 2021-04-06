using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIExample.Models;

namespace APIExample.Controllers
{//As webapi does not have View() its contoller return primitive value
    [Route("api/EmployeeAPI")]
    public class EmployeeController : ApiController
    {
        LTIMVCEntities db = new LTIMVCEntities();
        [Route("api/EmployeeAPI/GetAllEmployees")]
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            //retireves all the employees from list
            try
            { 
            return db.Employees.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/EmployeeAPI/GetEmployeesByID/{id}")]
        [HttpGet]
        public Employee Get(int id)
        {
            try
            {
                var data = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (data == null)
                    throw new Exception("Invalid EMP ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/EmployeeAPI/Login/{name}/{pwd}")] //2 parameters in {}
        [HttpGet]
        public string Get(string name, string pwd)
        {
            string result = "";
            try
            {
                var data = db.Employees.Where(x => x.EmpName == name && x.password == pwd);
                if (data.Count() == 0)
                    result = "Invalid Crendetials";
                else
                    result = "Login Successful";
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            return result;
        }
        [Route("api/EmployeeAPI/InsertEmployee")]
        [HttpPost]
        public bool Post([FromBody]Employee emp)//frombody says that the input comes from the post
        {
            try
            {
                db.Employees.Add(emp);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        [Route("api/EmployeeAPI/UpdateEmployee/{id}")]
        [HttpPut]
        public bool Put(int id,[FromBody] Employee newemp)
        {
            try
            {
                var olddata = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (olddata == null)
                    throw new Exception("Invalid ID");
                else
                {
                    olddata.EmpName = newemp.EmpName;
                    olddata.Dept = newemp.Dept;
                    olddata.Desg = newemp.Dept;
                    olddata.Salary = newemp.Salary;
                    olddata.projid = newemp.projid;
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
        [Route("api/EmployeeAPI/DeleteEmployee/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                var del = db.Employees.Where(x => x.EmpID == id).SingleOrDefault();
                if (del == null)
                    throw new Exception("ID can not be null");
                else
                {
                    db.Employees.Remove(del);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
