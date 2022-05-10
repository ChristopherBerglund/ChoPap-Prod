using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Model
{
    public class NotFoundStocks
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
