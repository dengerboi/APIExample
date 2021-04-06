using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIExample.Models;

namespace APIExample.Controllers
{//As webapi does not have View() its contoller return primitive value
    public class EmployeeController : ApiController
    {
        LTIMVCEntities db = new LTIMVCEntities();
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
        [HttpPost]
        public bool Post(Employee emp)
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
        [HttpPut]
        public bool Put(int id, Employee newemp)
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
    }
}
