using System;
using System.Collections.Generic;

namespace Fintech.Data.Models
{
    public partial class Question
    {
        public Question()
        {
            //this.Answers = new List<Answer>();
            this.QuestionOptions = new List<QuestionOption>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int AgeGroup { get; set; }
        //public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }

        public int Age { get; set; }
    }
}
