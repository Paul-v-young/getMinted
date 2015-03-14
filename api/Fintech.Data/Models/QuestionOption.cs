using System;
using System.Collections.Generic;

namespace Fintech.Data.Models
{
    public partial class QuestionOption
    {
        public QuestionOption()
        {
            //this.Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public decimal Score { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        //public virtual Question Question { get; set; }
    }
}
