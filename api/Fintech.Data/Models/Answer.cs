using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fintech.Data.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int PlayerId { get; set; }
        public int Age { get; set; }
        //public virtual Player Player { get; set; }
        //public virtual Question Question { get; set; }
        [JsonIgnore]
        public virtual QuestionOption QuestionOption { get; set; }
    }
}
