using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EatmWebApp.Models
{
    public class EatmAccount
    {
        public string Name { get; set; }
        [Required]
        [Key]
        public int CardNumber { get; set; }
        [Required]
        public int Password { get; set; }
        public int Balance { get; set; }
        public int TransactionCount { get; set;}
    }
}