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
    public class AnswersController : ApiController
    {
        private Repository<Answer> answerRepo = new Repository<Answer>();
        private Repository<AgeGroup> ageGroupRepo = new Repository<AgeGroup>();
        private Repository<Question> questionRepo = new Repository<Question>();

        // GET: api/Answers
        public IQueryable<Answer> GetAnswers()
        {
            return answerRepo.Table;
        }

        // GET: api/Answers/5
        [ResponseType(typeof(Answer))]
        public IHttpActionResult GetAnswer(int id)
        {
            Answer Answer = answerRepo.Find(id);
            if (Answer == null)
            {
                return NotFound();
            }

            return Ok(Answer);
        }

        // PUT: api/Answers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnswer(int id, Answer Answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Answer.Id)
            {
                return BadRequest();
            }

            try
            {
                answerRepo.Update(Answer);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(Answer);
        }

        // POST: api/Answers
        [ResponseType(typeof(Answer))]
        public IHttpActionResult PostAnswer(Answer Answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                answerRepo.Add(Answer);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(Answer);
        }

        // DELETE: api/Answers/5
        [ResponseType(typeof(Answer))]
        public IHttpActionResult DeleteAnswer(int id)
        {
            Answer Answer = answerRepo.Find(id);
            if (Answer == null)
            {
                return NotFound();
            }

            try
            {
                answerRepo.Delete(Answer);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("Api/Answers/NextQuestion/{player}")]
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetNextQuestionByAge(int player)
        {
            int age = 18;
            var answerList = answerRepo.Table.Where(q => q.PlayerId == player).ToList();
            if (answerList.Any())
            {
                var lastAnswer = answerList.LastOrDefault();
                age = lastAnswer.Age + 2;
            }

            var ageGroup = ageGroupRepo.Table.Where(q => age >= q.StartAge && age <= q.EndAge).FirstOrDefault();

            var questionAvailables = questionRepo.Table.Include("QuestionOptions")
                                        .Where(q => q.AgeGroup == ageGroup.Id)
                                        .ToList();

            var nextQuestion = questionAvailables.Where(q => !answerList.Select(o => o.QuestionId).Contains(q.Id))
                                                    .FirstOrDefault();

            if (nextQuestion != null)
            {
                nextQuestion.Age = age;
                return Ok(nextQuestion);
            }
            else
                return NotFound();

        }

        [Route("Api/Answers/Score/{player}")]
        public IHttpActionResult GetScore(int player)
        {
            decimal score = 0;
            var answerList = answerRepo.Table.Include("QuestionOption").Where(q => q.PlayerId == player);
            if (answerList.Any())
                score = answerList.Sum(o => o.QuestionOption.Score);

            return Ok(score);
        }

        [Route("Api/Answers/ScorePerAge/{player}")]
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetScorePerAge(int player)
        {
            var answerList = answerRepo.Table.Include("QuestionOption").Where(q => q.PlayerId == player);
            var result = from answer in answerList
                         select new
                         {
                             Age = answer.Age,
                             Score = answer.QuestionOption.Score
                         };

            return Ok(result);
        }

        [Route("Api/RunningBalance/{player}")]
        //[ResponseType(typeof(Question))]
        public IHttpActionResult GetRunningBalance(int player)
        {
            var answerList = answerRepo.Table.Include("QuestionOption").Where(q => q.PlayerId == player).ToList();

            decimal balance = 0;
            var statement = answerList.Select(transaction =>
            {
                balance += transaction.QuestionOption.Score;

                return new 
                {
                    Age = transaction.Age,
                    Score = transaction.QuestionOption.Score,
                    Balance = balance
                };
            }).ToList();

            return Ok(statement);
        }

    }
}