using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testmgtapp.Models;

namespace testmgtapp.Controllers
{
    public class studentController : ApiController
    {
        testmgt_newEntities objEntity = new testmgt_newEntities();
        [HttpGet]
        [Route("api/Student/GetQuestion")]
        public object GetQuestion()
        {
            try
            {
                questionTab objQues = new questionTab();
                return objEntity.questionTabs;


            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet]
        //[Route("api/Student/Duplicate")]
        //public object Duplicate(string email, int tstId)
        //{
        //    try
        //    {

        //        var result = from s in objEntity.scores
        //                     where
        //                       s.email == email &&
        //                       s.tstId == tstId
        //                     select new
        //                     {
        //                         s.scoreId
        //                        };

        //    return result;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpGet]
        [Route("api/Student/GetCourseName")]
        public object GetCourseName(int cid)
        {
            try
            {
                var result = from courseTabs in objEntity.courseTabs
                             where
                               courseTabs.cId == cid
                             select new
                             {
                                 courseTabs.cId,
                                 courseTabs.cName
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
        [Route("api/Student/GetSubjectName")]
        public object GetSubjectName(string email, int cid)
        {
            try
            {
                var result = (from questionTabs in objEntity.questionTabs
                              from userTabs in objEntity.userTabs
                              where
                                userTabs.email == email &&
                                questionTabs.testTab.courseTab.cId == cid
                              select new
                              {
                                  questionTabs.testTab.subjectTab.subId,
                                  questionTabs.testTab.subjectTab.subName
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
        [Route("api/Student/Questions")]
        public object GetQuestions(string email, int cid, int subid, int tstId1)
        {
            try
            {

                
               var  result1 = objEntity.scores.Where(x => x.email == email && x.tstId == tstId1).FirstOrDefault();
                if (result1 == null)
                {
                    var result = from questionTabs in objEntity.questionTabs
                                 from userTabs in objEntity.userTabs
                                 where
                                   userTabs.email == email &&
                                   questionTabs.testTab.cId == cid &&
                                   questionTabs.testTab.subId == subid

                                 select new
                                 {
                                     questionTabs.quesId,
                                     questionTabs.ques,
                                     questionTabs.opt1,
                                     questionTabs.opt2,
                                     questionTabs.opt3,
                                     questionTabs.opt4,
                                     questionTabs.answer,
                                     questionTabs.tstId,

                                     questionTabs.testTab.tstName,
                                     questionTabs.testTab.courseTab.cName,
                                     questionTabs.testTab.subjectTab.subName
                                 };
                    var Qns = result.Select(x => new { quesId = x.quesId, ques = x.ques, opt1 = x.opt1, opt2 = x.opt2, opt3 = x.opt3, opt4 = x.opt4 }).OrderBy(y => Guid.NewGuid()).Take(10).ToArray();

                    var updated = Qns.AsEnumerable().Select(x => new
                    {
                        quesId = x.quesId,
                        ques = x.ques,
                        options = new string[] { x.opt1, x.opt2, x.opt3, x.opt4 }

                    }).ToList();


                    return updated;
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


        [HttpPost]
        [Route("api/Student/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIDs)
        {
            var result = objEntity.questionTabs.AsEnumerable().Where(y => qIDs.Contains(y.quesId)).OrderBy(x => { return Array.IndexOf(qIDs, x.quesId); })
                .Select(z => z.answer).ToArray();
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/Student/InsertParticipant")]
        public object Insert(score Score)
        {
           // var result = objEntity.assignSubTeacherTabs.Where(s => s.uId == assign.uId && s.subId == assign.subId && s.isActive == assign.isActive).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                    objEntity.scores.Add(Score);
                    int x = objEntity.SaveChanges();
                    return x;
                

            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPut]
        [Route("api/Student/UpdateOutput")]
        public object UpdateOutput(score scoreboard)
        {
            // var result = objEntity.assignSubTeacherTabs.Where(s => s.uId == assign.uId && s.subId == assign.subId && s.isActive == assign.isActive).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                objEntity.Entry(scoreboard).State = System.Data.Entity.EntityState.Modified;
                int x = objEntity.SaveChanges();
                return x;


            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("api/Student/GetCName")]
        public object GetCName(string email)
        {
            try
            {
                var result = from userTabs in objEntity.userTabs
                             where
                               userTabs.email == email &&
                               userTabs.roleId == 3 &&
                               userTabs.isActive == true
                             select new
                             {
                                 userTabs.courseTab.cId,
                                 userTabs.courseTab.cName
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


    }
}
