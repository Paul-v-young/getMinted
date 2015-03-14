using System;
using System.Collections.Generic;

namespace Fintech.Data.Models
{
    public partial class Player
    {
        public Player()
        {
            //this.Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<Answer> Answers { get; set; }
    }
}
