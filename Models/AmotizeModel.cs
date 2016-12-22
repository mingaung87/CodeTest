using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeTest.Models
{
    public class AmotizeModel
    {
        [Required]
        [Display(Name = "Loan Amount")]
        public double LoanAmount { get; set; }

        [Required]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required]
        [Display(Name = "Term (Months)")]
        public int Month { get; set; }
    }


}