using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService.Entities
{
    public class EmailSubscription : BaseEntity
    {
        public string Email { get; set; }
        public int ScheduleId { get; set; }
        public int Semester { get; set; }
    }
}
