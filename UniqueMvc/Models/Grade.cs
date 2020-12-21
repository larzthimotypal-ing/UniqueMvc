using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueMvc.Models
{
    public class Grade
    {
        [Key]
        public int ID { get; set; }
        public string AppUserID { get; set; }
        public double SubjectGrade { get; set; }
        public int? PrelimID { get; set; }
        public int? MidtermID { get; set; }
        public int? PrefinalID { get; set; }
        public int? FinalID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public TermGrade Prelim { get; set; }
        public TermGrade Midterm { get; set; }
        public TermGrade Prefinal { get; set; }
        public TermGrade Final { get; set; }
    }
}
