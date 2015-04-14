namespace CountdownBusinessLogic
{
    using CountdownDataBaseLayer;
    using CountdownDataBaseLayer.Repo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CountdownInfo : ReminderFactory
    {
        public IEnumerable<CountdownHelper> Helper { get; set; }

        public CountdownInfo()
        {
            List<CountdownHelper> el = new List<CountdownHelper>();
            foreach (var item in Reminders)
            {
                el.Add(item);
            }
        }
    }
}
