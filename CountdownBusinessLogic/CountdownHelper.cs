using CountdownDataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownBusinessLogic
{
   public class CountdownHelper : ReminderProduct
    {
       public Durations Duration { get; set; }
       public IEnumerable<Exercises> Exercises { get; set; }
    }
}
