using System;
using System.Collections.Generic;

namespace Fintech.Data.Models
{
    public partial class AgeGroup
    {
        public int Id { get; set; }
        public int StartAge { get; set; }
        public int EndAge { get; set; }
    }
}
