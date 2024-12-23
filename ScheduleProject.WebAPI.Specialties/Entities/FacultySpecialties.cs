using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access.Entities
{
    public class FacultySpecialty : BaseEntity
    {
        public int FacultyId { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
