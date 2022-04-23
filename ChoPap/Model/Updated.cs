using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Model
{
    public class Updated
    {
        private static ChopapContext context = new ChopapContext();
        public int UpdatedID { get; set; }
        public DateTime updatedTime { get; set; }
        public static void IsRunning()
        {
            var up = context.updates.FirstOrDefault();
            up.updatedTime = DateTime.Now;
            context.updates.Update(up);
            context.SaveChanges();
        }
    }
}