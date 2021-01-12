using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testmgtapp.Models;


namespace testmgtapp.Controllers
{
    /// <summary>
    /// This API for Login and Registration 
    /// </summary>
    public class regController : ApiController
    {
        testmgt_newEntities objEntity = new testmgt_newEntities();
        /// <summary>
        /// This registration method is for admin and teacher 
        /// </summary>
       
        [HttpPost]
        [Route("api/Reg/CreateUsers")]
        public object PostCreateUsers(userTab user)
        {
            var result = objEntity.userTabs.Where(s => s.email == user.email).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (result == null)
                {
                    objEntity.userTabs.Add(new userTab()
                    {
                        fullName = user.fullName,
                        email = user.email,
                        password = user.password,
                        roleId = user.roleId,
                        isActive = user.isActive,
                        cId = user.cId

                    });
                    objEntity.SaveChanges();
                }
                else
                {
                    return "Email Id already Exists";
                }

            }
            catch (Exception)
            {
                throw;
            }

            return "Data Inserted Successfully";
        }
        /// <summary>
        /// This method is for showing course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Reg/GetCourse")]
        public IEnumerable<courseTab> GetCourse()
        {
            try
            {
                return objEntity.courseTabs.Where(x=> x.isActive==true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is for Login
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Reg/LoginUsers")]
        public object PostLoginUsers(userTab userLogin)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = from x in objEntity.userTabs.Where(x => x.email == userLogin.email && x.password == userLogin.password && x.isActive == true) select x;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
          
        }
        
    }
}
