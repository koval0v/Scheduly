using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class FacultyModel : BaseModel
    {
        public string Name { get; set; }
        public string Description {get;set;}
        public int? UniversityId { get; set; }
        public int BuildingId { get; set; } 
    }
}
