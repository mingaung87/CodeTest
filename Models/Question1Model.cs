using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeTest.Models
{
    public class Question1Model
    {
        [Required]
        [Display(Name = "Text Input")]
        public string input { get; set; }

        public string output { get; set; }
    }
}