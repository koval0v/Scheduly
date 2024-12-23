using ScheduleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ScheduleModel : BaseModel
    {
        public int GroupId { get; set; }
        public ICollection<ScheduleDisciplineModel>? ScheduleDisciplines { get; set; }
    }
}
