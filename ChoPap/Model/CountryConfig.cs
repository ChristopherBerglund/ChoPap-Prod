using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Model
{
    public class CountryConfig
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public bool DoneForTheDay { get; set; }
        public string Day { get; set; }

    }
}
