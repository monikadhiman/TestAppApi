using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testmgtapp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace testmgtapp.Controllers
{
    /// <summary>
    /// This API is for Admin
    /// </summary>
    public class adminController : ApiController
    {
        testmgt_newEntities objEntity = new testmgt_newEntities();
        /// <summary>
        /// This method is for Course
        /// </summary>
        [HttpPost]
        [Route("api/Admin/AddCourse")]
        public object AddCourse(courseTab course)
        {
            var result = objEntity.courseTabs.Where(x => x.cName == course.cName).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (result == null)
                {
                    objEntity.courseTabs.Add(course);
                    int x = objEntity.SaveChanges();
                    return x;
                }
                else
                {
                    return "Exists";
                }
            }
            catch (Exception)
            {
                throw;
            }

         }
        /// <summary>
        /// This method is for View Active Course
        /// </summary>
       
        [HttpGet]
        [Route("api/Admin/GetActiveCourse")]
        public object GetActiveCourse()
        {
            try
            {
                var result = from courseTab in objEntity.courseTabs 
                             select new
                             {
                                 courseTab.cId,
                                 courseTab.cName,
                                 courseTab.isActive,
                                 
                             };
               
                    return result;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Admin/GetOnlyActiveCourse")]
        public object GetOnlyActiveCourse()
        {
            try
            {
                var result = from courseTab in objEntity.courseTabs where courseTab.isActive == true
                             select new
                             {
                                 courseTab.cId,
                                 courseTab.cName,
                                 courseTab.isActive,

                             };

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is Course for Deletion
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/DeleteCourse")]
        public object DeleteCourse(courseTab course)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                courseTab objCourse = objEntity.courseTabs.Find(course.cId);
                objCourse.cId = course.cId;
                //if (objCourse != null)
                //{
                //    objCourse.isActive = course.isActive;
                //}
                //objEntity.Entry(objCourse).Property("isActive").IsModified = true;
                //int i = objEntity.SaveChanges();
                //return i;
                if (objCourse.isActive == true)
                {
                    objCourse.isActive = course.isActive;
                }
                else
                {
                    objCourse.isActive = true;
                }
                objEntity.Entry(objCourse).Property("isActive").IsModified = true;
                int i= objEntity.SaveChanges();
                return i;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// This method is for course searching for specific id
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Admin/GetCourseById")]
        public object GetCourseById(courseTab course)
        {
            courseTab objCourse = new courseTab();
            int ID = Convert.ToInt32(course.cId);
            try
            {
                objCourse = objEntity.courseTabs.Where(x =>x.cId==course.cId).FirstOrDefault();
                if (objCourse == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objCourse);
        }
        /// <summary>
        /// This method is for Update Course 
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="cName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/UpdateCourse/{cId}/{cName}")]
        public object UpdateCourse(int cId,string cName)
        {
            var result = objEntity.courseTabs.Where(x => x.cName == cName).FirstOrDefault();
            int i = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                courseTab objCourse = new courseTab();
                objCourse = objEntity.courseTabs.Where(x => x.cId == cId).FirstOrDefault();
                //objCourse = objEntity.courseTabs.Find(course.cId);
                if (objCourse != null)
                {
                    {
                        objCourse.cName = cName;
                    }
                    if (result == null)
                        { 
                        objEntity.Entry(objCourse).Property("cName").IsModified = true;
                        i = objEntity.SaveChanges();
                        
                        }
                    else
                    {
                        return "Exists";
                    }
                   
                }
                return i;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        /// <summary>
        /// This method is for View all Subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Admin/GetActiveSubject")]
        public object GetActiveSubject()
        {

            try
            {
               
                var result = from subjectTabs in objEntity.subjectTabs
                             select new
                             {
                                 subjectTabs.subId,
                                 subjectTabs.subName,
                                 subjectTabs.isActive,
                                 subjectTabs.cId,
                                 subjectTabs.courseTab.cName
                             };
                
                    return result;
                
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is Course for Deletion
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/DeleteSubject")]
        public object DeleteSubject(subjectTab subject)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int res =0;
                subjectTab objSub = objEntity.subjectTabs.Find(subject.subId);
                objSub.subId = subject.subId;
                if (objSub.isActive == true)
                {
                    objSub.isActive = subject.isActive;
                    res = 1;
                }
                else
                {
                    objSub.isActive = true;
                    res = 0;
                }
                objEntity.Entry(objSub).Property("isActive").IsModified = true;
                int i = objEntity.SaveChanges();
                return i + res;
            }
            catch (Exception)
            {
                throw;
            }

        }
        ///// <summary>
        ///// This is delete method
        ///// </summary>
        ///// <param name="cId"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("api/Admin/DeleteCourse")]
        //public object DeleteCourse(int cId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        courseTab objCourse = new courseTab();
        //        objCourse = objEntity.courseTabs.Where(x => x.cId == cId).FirstOrDefault();
        //        if (objCourse != null)
        //        {
        //            objCourse.isActive = false;
        //        }
        //        int i = this.objEntity.SaveChanges();
        //        return i;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }


        /// <summary>
        /// This method is for Create Subject
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Admin/AddSubject")]
        public object AddSubject(subjectTab sub)
        {
            
            var result = objEntity.subjectTabs.Where(s => s.cId == sub.cId && s.subName == sub.subName).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (result == null)
                {

                    objEntity.subjectTabs.Add(sub);
                    int x = objEntity.SaveChanges();
                    return x;
                }
                else
                {
                    return "Exists";
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// This method is for Create Subject
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Admin/AddTeacherSubject")]
        public object AddTeacherSubject(assignSubTeacherTab assign)
        {
            var result = objEntity.assignSubTeacherTabs.Where(s => s.uId == assign.uId && s.subId == assign.subId ).FirstOrDefault();
                         

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (result == null)
                {
                    objEntity.assignSubTeacherTabs.Add(new assignSubTeacherTab()
                    {
                        uId = assign.uId,
                        subId = assign.subId,
                        isActive = true

                    });
                int x = objEntity.SaveChanges();
                return x;
                }
                else
                {
                    return "Exists";
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// This method is for Update Course 
        /// </summary>
        // <param name="subName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/EditTeacherSubject")]
        public object EditTeacherSubject(assignSubTeacherTab assign)
        {
            var result = objEntity.assignSubTeacherTabs.Where(s => s.uId == assign.uId && s.subId == assign.subId).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                assignSubTeacherTab objSub = objEntity.assignSubTeacherTabs.Find(assign.aId);
                if (result == null)
                {
                    objEntity.assignSubTeacherTabs.Add(new assignSubTeacherTab()
                    {
                        uId = assign.uId,
                        subId = assign.subId,
                        isActive = true

                    });
                    objEntity.Entry(objSub).Property("uId").IsModified = true;
                    objEntity.Entry(objSub).Property("subId").IsModified = true;
                    objEntity.Entry(objSub).Property("isActive").IsModified = true;
                    int x = objEntity.SaveChanges();
                    return x;
                }
                else
                {
                    return "Exists";
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// this method show all Active Subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Admin/GetNonActiveTeacher")]
        public IEnumerable<userTab> GetNonActiveTeacher()
        {
            try
            {
                userTab objTeacher = new userTab();
                //return objEntity.userTabs.Where(x => x.roleId == null).ToArray();
                var result = from s in objEntity.userTabs
                             where s.roleId == null && s.isActive == true
                             select s;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is to assign teacher role
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/UpdateTeacherRole")]
        public object UpdateTeacherRole(userTab teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                userTab objTeacher = new userTab();
                objTeacher = objEntity.userTabs.Find(teacher.uId);
                if (objTeacher != null)
                {
                    objTeacher.roleId = teacher.roleId;
                }
                objEntity.Entry(objTeacher).Property("roleId").IsModified = true;
                int i = objEntity.SaveChanges();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        /// <summary>
        /// This method is for Update Course 
        /// </summary>
        // <param name="subName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/UpdateSubject")]
        public object UpdateSubject(subjectTab subjectt)
        {
            var result = objEntity.subjectTabs.Where(s => s.subName == subjectt.subName && s.cId == subjectt.cId && s.isActive == true && s.subId == subjectt.subId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (result == null)
                {
                    subjectTab objSub = objEntity.subjectTabs.Find(subjectt.subId);
                    if (objSub != null)
                    {
                        objSub.subId = subjectt.subId;
                        objSub.subName = subjectt.subName;
                        objSub.cId = subjectt.cId;


                    }
                    objEntity.Entry(objSub).Property("subName").IsModified = true;
                    objEntity.Entry(objSub).Property("cId").IsModified = true;
                    objEntity.Entry(objSub).Property("subId").IsModified = true;
                    int i = objEntity.SaveChanges();
                    return i;

                }
                else
                {
                    return "Exists";
                }
            }
            catch(Exception)
            {
                throw;
            }
            
        }
        /// <summary>
        /// This method is view teacher
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/GetTeacher")]
        public object GetTeacher()
        {
           
            try
            {
                var result = from userTabs in objEntity.userTabs
                             where
                               userTabs.roleId == 2 &&
                               userTabs.isActive == true
                             select new
                             {
                                 uId = userTabs.uId,
                                 fullName = userTabs.fullName,
                                 email = userTabs.email,
                                 password = userTabs.password,
                                 roleId = userTabs.roleId,
                                 isActive = userTabs.isActive,
                                 cId = userTabs.cId
                             };
                return result;
                // return objEntity.courseTabs.Where(x => x.isActive == true).ToArray();
                //var result = from x in objEntity.courseTabs.Where(x =>x.isActive == true).ToArray() Select x;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// This method is view course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/GetCourse")]
        public object GetCourse()
        {
           
            try
            {
                //var result = from courseTabs in objEntity.courseTabs
                //             where
                //               courseTabs.isActive == true
                //             select new
                //             {
                //                 cId = courseTabs.cId,
                //                 cName = courseTabs.cName,
                //                 isActive = courseTabs.isActive
                //             };
                var result = objEntity.courseTabs;
                return result;
                // return objEntity.courseTabs.Where(x => x.isActive == true).ToArray();
                //var result = from x in objEntity.courseTabs.Where(x =>x.isActive == true).ToArray() Select x;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is view course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/GetSub")]
        public object GetSub()
        {

            try
            {
                
                var result = from subjectTabs in objEntity.subjectTabs
                             select new
                             {
                                 subjectTabs.subId,
                                 subjectTabs.subName
                             };
                return result;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is view course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/GetSubject/{cid}")]
        public object GetSubject(int cid)
        {
           
            try
            {
                var result = from subjectTabs in objEntity.subjectTabs
                             where
                               subjectTabs.cId == cid &&
                               subjectTabs.isActive == true
                             select new
                             {
                                 subId = subjectTabs.subId,
                                 subName = subjectTabs.subName,
                                 isActive = subjectTabs.isActive,
                                 cId = subjectTabs.cId
                             };
                return result;
                // return objEntity.courseTabs.Where(x => x.isActive == true).ToArray();
                //var result = from x in objEntity.courseTabs.Where(x =>x.isActive == true).ToArray() Select x;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method for view all teacher assign subject 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Admin/GetAllSubjectTeacherDetails")]
        public object GetAllSubjectTeacherDetails()
        {
            try
            {
                //var result = from assignSubTeacherTabs in objEntity.assignSubTeacherTabs
                //             select new
                //             {
                //                 assignSubTeacherTabs.aId,
                //                 assignSubTeacherTabs.userTab.fullName,
                //                 assignSubTeacherTabs.subjectTab.courseTab.cName,
                //                 assignSubTeacherTabs.subjectTab.subName,
                //                 assignSubTeacherTabs.isActive
                //             };
                var result = from assignSubTeacherTabs in objEntity.assignSubTeacherTabs
                             select new
                             {
                                 assignSubTeacherTabs.aId,
                                 assignSubTeacherTabs.userTab.uId,
                                 assignSubTeacherTabs.userTab.fullName,
                                 assignSubTeacherTabs.subjectTab.subId,
                                 assignSubTeacherTabs.subjectTab.subName,
                                 assignSubTeacherTabs.subjectTab.courseTab.cId,
                                 assignSubTeacherTabs.subjectTab.courseTab.cName,
                                 assignSubTeacherTabs.isActive
                             };
                    return result;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is Course for Deletion
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Admin/DeleteTeacherSubject")]
        public object DeleteTeacherSubject(assignSubTeacherTab assign)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                int res = 0;
                assignSubTeacherTab objAssign = objEntity.assignSubTeacherTabs.Find(assign.aId);
                objAssign.aId = assign.aId;
                if (objAssign.isActive == true)
                {
                    objAssign.isActive = assign.isActive;
                    res = 1;
                }
                
                else
                {
                    objAssign.isActive = true;
                    res = 0;
                }
                objEntity.Entry(objAssign).Property("isActive").IsModified = true;
                int i = objEntity.SaveChanges();
                return i + res;


            }
            catch (Exception)
            {
                throw;
            }

        }

        //[HttpGet]
        //[Route("api/Student/GetForgetPassword")]
        //public object GetForgetPassword(string email, int cid, int subid, int tstId1)
        //{
        //    try
        //    {


                
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


    }
}

