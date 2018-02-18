using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatmWebApp.Models
{
    public class Count
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public EatmAccount EatmAccounts { get; set; }
        public int EatmAccountsId { get; set; }

        public int Counter { get; set; }
    }
}