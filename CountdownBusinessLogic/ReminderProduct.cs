using CountdownDataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownBusinessLogic
{
    public class ReminderProduct
    {
        public Interval Interval { get; set; }
        public Reminder Reminder { get; set; }
        public IEnumerable<Settings> Settings { get; set; }
    }
}
