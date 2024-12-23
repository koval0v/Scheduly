using ScheduleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService.Entities
{
    public class Schedule : BaseEntity
    {
        public int GroupId { get; set; }
        public ICollection<ScheduleDiscipline>? ScheduleDisciplines { get; set; }

    }
}
