using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EmailSubscriptionModel : BaseModel
    {
        public string Email { get; set; }
        public int ScheduleId { get; set; }
        public int Semester { get; set; }
    }
}
