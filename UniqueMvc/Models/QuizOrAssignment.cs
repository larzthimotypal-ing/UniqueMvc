using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueMvc.Models
{
    public class QuizOrAssignment
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public double Grade { get; set; }
    }
}
