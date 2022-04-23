using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Model
{
    public class Account
    {
        [Display(Name = "Id")]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Saldo { get; set; }
        [Display(Name = "Total")]
        public int qtyTotal { get; set; }
        [Display(Name = "Possession")]
        public int qtyInPossession { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public decimal Balance { get; set; }
        public int Days { get; set; }
        [Display(Name = "Updated")]
        public string lastUpdated { get; set; }
    }
}
