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
    /// Teacher API
    /// </summary>
    public class teacherController : ApiController
    {
        testmgt_newEntities db = new testmgt_newEntities();

        /// <summary>
        /// This method is related with Teacher Controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Teacher/GetTeacherCourses")]
        public object GetTeacherCourse()
        {
            //var result = from course in db.courseTabs
            //             join subject in db.subjectTabs on course.cId equals subject.cId 
            //             join AssignSubTeacher in db.assignSubTeacherTabs on subject.subId equals AssignSubTeacher.subId
            //             where (AssignSubTeacher.uId == 2)
            //             group new { course} by new { course.cId,course.cName} into grp select new
            //             {
            //                 //CourseName = course.cName,
            //                 grp.Key.cId,
            //                 grp.Key.cName
            //                 //Cid = course.cId

            //             };
            //return result;
           
           
            var result = (from subjectTabs in db.subjectTabs
                          where
                                (from assignSubTeacherTabs in db.assignSubTeacherTabs
                                 where
                      assignSubTeacherTabs.uId == 2
                                 select new
                                 {
                                     assignSubTeacherTabs.subId
                                 }).Contains(new { subId = (Int32?)subjectTabs.subId })
                          select new
                          {
                              subjectTabs.courseTab.cName
                          }).Distinct();

             return result;
            


        }
        //[HttpGet]
        //[Route("api/Teacher/GetTeacherSubject")]
        //public object GetTeacherSubject()
        //{
            
        //    var result = from subjectTabs in db.subjectTabs
        //                 from courseTabs in db.courseTabs
        //                 where
        //                   courseTabs.cId == 1 &&
        //                     (from assignSubTeacherTabs in db.assignSubTeacherTabs
        //                      where
        //            assignSubTeacherTabs.uId == 2 &&
        //              (from subjectTabs0 in db.subjectTabs
        //               where
        //                    subjectTabs0.cId ==
        //                      ((from courseTabs0 in db.courseTabs
        //                        where
        //                    courseTabs0.cId == 1
        //                        select new
        //                        {
        //                            courseTabs0.cId
        //                        }).FirstOrDefault().cId)
        //                                           select new
        //               {
        //                   subjectTabs0.subId
        //               }).Contains(new { subId = (Int32)assignSubTeacherTabs.subId })
        //                      select new
        //                      {
        //                          assignSubTeacherTabs.subId
        //                      }).Contains(new { subId = (Int32?)subjectTabs.subId })
        //                 select new
        //                 {
        //                     subjectTabs.subName
        //                 };
        //    return result;
        //    }
        /// <summary>
        /// This method get all details of teacher
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Teacher/GetAssignId")]
        public object GetAssignId(assignSubTeacherTab assign)
        {
           
            try
            {
                var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
                             where
                               assignSubTeacherTabs.uId == assign.uId  &&
                               assignSubTeacherTabs.subId == assign.subId &&
                               assignSubTeacherTabs.isActive == true
                             select new
                             {
                                 aId = assignSubTeacherTabs.aId,
                                 uId = assignSubTeacherTabs.uId,
                                 subId = assignSubTeacherTabs.subId,
                                 isActive = assignSubTeacherTabs.isActive
                             };
                if (result == null)
                {
                    return "Data not available";
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method get all details of teacher
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Teacher/GetTeacherDetails")]
        public object GetTeacherDetails(string email)
        {
            
            try
            {
                var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
                             where
                               assignSubTeacherTabs.userTab.email == email &&
                               assignSubTeacherTabs.userTab.roleId == 2 &&
                               assignSubTeacherTabs.isActive == true
                             select new
                             {
                                 assignSubTeacherTabs.aId,
                                 assignSubTeacherTabs.uId,
                                 assignSubTeacherTabs.userTab.fullName,
                                 assignSubTeacherTabs.subId,
                                 assignSubTeacherTabs.subjectTab.subName,
                                 assignSubTeacherTabs.subjectTab.courseTab.cId,
                                 assignSubTeacherTabs.subjectTab.courseTab.cName,
                                 assignSubTeacherTabs.isActive
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is for view course
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Teacher/GetTeacherCourse")]
        public object GetTeacherCourse(string email)
        {
           
            try
            {
                var result = (from assignSubTeacherTabs in db.assignSubTeacherTabs
                              where
                                assignSubTeacherTabs.userTab.email == email &&
                                assignSubTeacherTabs.userTab.roleId == 2 &&
                                assignSubTeacherTabs.isActive == true
                              select new
                              {
                                  assignSubTeacherTabs.subjectTab.courseTab.cId,
                                  assignSubTeacherTabs.subjectTab.courseTab.cName
                              }).Distinct();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
       
        /// <summary>
        /// This method is for test
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Teacher/AddTest")]
        public object AddTest(testTab test)
        {
           
            var result = db.testTabs.Where(s => s.tstName == test.tstName && s.cId == test.cId && s.subId == test.subId && s.aId == test.aId).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (result == null)
                {
                    db.testTabs.Add(new testTab()
                    {
                        tstName = test.tstName,
                        cId = test.cId,
                        subId = test.subId,
                        aId = test.aId

                    });
                    int x = db.SaveChanges();
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
        /// This method is for view course
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("api/Teacher/GetTeacherSubject")]
        //public object GetTeacherSubject(string email)
        //{

        //    try
        //    {
        //        var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
        //                     where
        //                       assignSubTeacherTabs.userTab.email == email &&
        //                       assignSubTeacherTabs.userTab.roleId == 2 &&
        //                       assignSubTeacherTabs.isActive == true
        //                     select new
        //                     {
        //                         assignSubTeacherTabs.subId,
        //                         assignSubTeacherTabs.subjectTab.subName
        //                     };
        //        if (result == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return result;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpGet]
        [Route("api/Teacher/GetTeacherSubject")]
        public object GetTeacherSubject(string email, Int64 cid)
        {

            try
            {
                var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
                             where
                               assignSubTeacherTabs.userTab.email == email &&
                               assignSubTeacherTabs.subjectTab.courseTab.cId == cid &&
                               assignSubTeacherTabs.userTab.roleId == 2 &&
                               assignSubTeacherTabs.isActive == true
                             select new
                             {
                                 assignSubTeacherTabs.subId,
                                 assignSubTeacherTabs.subjectTab.subName
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("api/Teacher/DeleteTest")]
        public object DeleteTest(Int64 TestId)
        {
           // var result = db.testTabs.Where(x => x.tstId == TestId).FirstOrDefault();
           var objtest = db.testTabs.Find(TestId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                if (objtest == null)
                {
                    return "No Data Found";
                }
                else
                {
                    db.testTabs.Remove(objtest);
                    int y = db.SaveChanges();

                    return y;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("api/Teacher/EditTest")]
        public object EditTest(testTab test)
        {
            var result = db.testTabs.Where(s => s.tstId == test.tstId && s.tstName == test.tstName && s.cId == test.cId && s.subId == test.subId).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                testTab objTest = db.testTabs.Find(test.tstId);
                if (result == null)
                {
                    if (objTest != null)
                    {

                        objTest.tstName = test.tstName;
                        objTest.cId = test.cId;
                        objTest.subId = test.subId;


                    }
                    else
                    {
                        return "Not Found";
                    }
                    int i = this.db.SaveChanges();
                    return i;
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
        /// This method is for view course
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Teacher/GetAId")]
        public object GetAId(string email, int subid)
        {

            try
            {
                var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
                             where
                               assignSubTeacherTabs.userTab.email == email &&
                               assignSubTeacherTabs.subId == subid &&
                               assignSubTeacherTabs.userTab.roleId == 2 &&
                               assignSubTeacherTabs.isActive == true
                             select new
                             {
                                 assignSubTeacherTabs.aId
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Teacher/GetSub")]
        public object GetSub(string email, int cid)
        {

            try
            {
                var result = from assignSubTeacherTabs in db.assignSubTeacherTabs
                             where
                               assignSubTeacherTabs.userTab.email == email &&
                               assignSubTeacherTabs.subjectTab.courseTab.cId == cid &&
                               assignSubTeacherTabs.userTab.roleId == 2 &&
                               assignSubTeacherTabs.isActive == true
                             select new
                             {
                                 assignSubTeacherTabs.subId,
                                 assignSubTeacherTabs.subjectTab.subName
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Teacher/GetTestdet")]
        public object GetTestdet(string testName, int cId, int SubId)
        {

            try
            {
                var result = from testTabs in db.testTabs
                             where
                               testTabs.tstName == testName &&
                               testTabs.cId == cId &&
                               testTabs.subId == SubId
                             select new
                             {
                                 testTabs.tstId
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/Teacher/GetTestDetails")]
        public object GetTestDetails(string email)
        {

            try
            {
                //var result = from testTabs in db.testTabs 
                //             select new
                //             {
                //                 testTabs.tstId,
                //                 testTabs.tstName,
                //                 testTabs.courseTab.cId,
                //                 testTabs.courseTab.cName,
                //                 testTabs.subjectTab.subId,
                //                 testTabs.subjectTab.subName
                //             };
                var result = from testTabs in db.testTabs
                             where
                               testTabs.assignSubTeacherTab.userTab.email == email
                             select new
                             {
                                 testTabs.tstId,
                                 testTabs.tstName,
                                 testTabs.courseTab.cId,
                                 testTabs.courseTab.cName,
                                 testTabs.subjectTab.subId,
                                 testTabs.subjectTab.subName
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/Teacher/GetTestId")]
        public object GetTestId(string email)
        {

            try
            {
                //var result = from  testTabs in db.testTabs
                //             where
                //               testTabs.assignSubTeacherTab.userTab.email == email &&
                //               testTabs.assignSubTeacherTab.userTab.isActive == true
                //             select new
                //             {
                //                 testTabs.tstId,
                //                 testTabs.tstName,
                //                 testTabs.cId,
                //                 testTabs.subId,
                //                 testTabs.aId,
                //                 testTabs.assignSubTeacherTab.userTab.email,
                //                 isActive = (bool?)testTabs.assignSubTeacherTab.userTab.isActive
                //             };
                var result = (from testTabs in db.testTabs
                              where
                                testTabs.assignSubTeacherTab.userTab.email == email
                              select new
                              {
                                  testTabs.tstName
                              }).Distinct();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Teacher/GetCourseid")]
        public object GetCourseid(string email)
        {

            try
            {
                var result = (from testTabs in db.testTabs
                              where
                                testTabs.courseTab.isActive == true &&
                                testTabs.assignSubTeacherTab.userTab.email == email
                              select new
                              {
                                  testTabs.courseTab.cId,
                                  testTabs.courseTab.cName
                              }).Distinct();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Teacher/GetSubid")]
        public object GetSubid(string email)
        {

            try
            {
                var result = (from testTabs in db.testTabs
                              where
                                testTabs.subjectTab.isActive == true &&
                                testTabs.assignSubTeacherTab.userTab.email == email
                              select new
                              {
                                  testTabs.subjectTab.subId,
                                  testTabs.subjectTab.subName
                              }).Distinct();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("api/Teacher/AddQues")]
        public object AddQues(questionTab question)
        {

            var result = db.questionTabs.Where(s => s.ques == question.ques).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (result == null)
                {
                    db.questionTabs.Add(new questionTab()
                    {
                        ques = question.ques,
                        opt1 = question.opt1,
                        opt2 = question.opt2,
                        opt3 = question.opt3,
                        opt4 = question.opt4,
                        answer = question.answer,
                        tstId = question.tstId,
                        mark = question.mark

                    });
                    int x = db.SaveChanges();
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
        [HttpGet]
        [Route("api/Teacher/GetQuestionDetails")]
        public object GetQuestionDetails(string email, int tstId)
        {

            try
            {
                //var result = from questionTabs in db.questionTabs
                //             where
                //               questionTabs.testTab.assignSubTeacherTab.userTab.email == email &&
                //               questionTabs.testTab.assignSubTeacherTab.userTab.isActive == true &&
                //               questionTabs.testTab.assignSubTeacherTab.userTab.roleId == 2
                //             select new
                //             {
                //                 questionTabs.quesId,
                //                 questionTabs.ques,
                //                 questionTabs.opt1,
                //                 questionTabs.opt2,
                //                 questionTabs.opt3,
                //                 questionTabs.opt4,
                //                 questionTabs.answer,
                //                 questionTabs.testTab.tstName,
                //                 questionTabs.mark
                //             };
                var result = from questionTabs in db.questionTabs
                             where
                               questionTabs.testTab.assignSubTeacherTab.userTab.email == email &&
                               questionTabs.tstId == tstId
                             select new
                             {
                                 questionTabs.quesId,
                                 questionTabs.testTab.tstName,
                                 questionTabs.testTab.assignSubTeacherTab.subjectTab.courseTab.cName,
                                 questionTabs.testTab.assignSubTeacherTab.subjectTab.subName,
                                 questionTabs.ques,
                                 questionTabs.opt1,
                                 questionTabs.opt2,
                                 questionTabs.opt3,
                                 questionTabs.opt4,
                                 questionTabs.answer,
                                 questionTabs.mark
                             };
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/Teacher/GetQuestionDetails1")]
        public object GetQuestionDetails1(string email)
        {

            try
            {
                var result = from questionTabs in db.questionTabs
                             where
                               questionTabs.testTab.assignSubTeacherTab.userTab.email == email &&
                               questionTabs.testTab.assignSubTeacherTab.userTab.isActive == true &&
                               questionTabs.testTab.assignSubTeacherTab.userTab.roleId == 2 orderby questionTabs.quesId
                             select new
                             {
                                 questionTabs.quesId,
                                 questionTabs.ques,
                                 questionTabs.opt1,
                                 questionTabs.opt2,
                                 questionTabs.opt3,
                                 questionTabs.opt4,
                                 questionTabs.answer,
                                 questionTabs.testTab.tstName,
                                 questionTabs.mark
                             } ;

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/Teacher/GetQuesTest")]
        public object GetQuesTest(string email)
        {

            try
            {
                var result = from questionTabs in db.questionTabs
                             where
                               questionTabs.testTab.assignSubTeacherTab.userTab.email == email &&
                               questionTabs.testTab.assignSubTeacherTab.userTab.isActive == true &&
                               questionTabs.testTab.assignSubTeacherTab.userTab.roleId == 2
                             select new
                             {
                                 questionTabs.quesId,
                                 questionTabs.ques,
                                 questionTabs.opt1,
                                 questionTabs.opt2,
                                 questionTabs.opt3,
                                 questionTabs.opt4,
                                 questionTabs.answer,
                                 questionTabs.testTab.tstName,
                                 questionTabs.mark
                             };
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("api/Teacher/DeleteQuestion")]
        public object DeleteQuestion(Int64 quesid)
        {
            // var result = db.testTabs.Where(x => x.tstId == TestId).FirstOrDefault();
            var objQues = db.questionTabs.Find(quesid);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (objQues == null)
                {
                    return "No Data Found";
                }
                else
                {
                    db.questionTabs.Remove(objQues);
                    int y = db.SaveChanges();

                    return y;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }


}