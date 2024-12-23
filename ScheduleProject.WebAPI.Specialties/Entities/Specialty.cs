using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access.Entities
{
    public class Specialty : BaseEntity
    {
        public string Cipher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UniversityId { get; set; }

        public ICollection<FacultySpecialty> FacultySpecialties { get; set; }

    }
}
