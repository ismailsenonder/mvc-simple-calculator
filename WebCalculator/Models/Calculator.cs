using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCalculator.Models
{
    public class Calculator
    {
        [Required]
        public decimal FirstValue { get; set; }
        [Required]
        public decimal SecondValue { get; set; }
        [Required]
        [Display(Name = "Sign")]
        public char FunctionSign { get; set; }
    }
}