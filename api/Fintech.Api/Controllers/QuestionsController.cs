using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Fintech.Data.Models;
using Fintech.Data.Repositories;

namespace Fintech.Api.Controllers
{
    public class QuestionsController : ApiController
    {
        private Repository<Question> questionRepo = new Repository<Question>();
        private Repository<AgeGroup> ageGroupRepo = new Repository<AgeGroup>();

        // GET: api/Questions
        public IQueryable<Question> GetQuestions()
        {
            return questionRepo.Table.Include("QuestionOptions");
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            Question question = questionRepo.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }


        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            try
            {
                questionRepo.Update(question);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(question);
        }

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                questionRepo.Add(question);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = questionRepo.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            try
            {
                questionRepo.Delete(question);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}