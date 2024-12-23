using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SimpleService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IDisciplineTeacherService _disciplineTeacherService;

        public TeachersController(ITeacherService teacherService, IDisciplineTeacherService disciplineTeacherService)
        {
            _teacherService = teacherService;
            _disciplineTeacherService = disciplineTeacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> Get()
        {
            var teachers = await _teacherService.GetAllAsync();

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherModel>> Get(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);

            return Ok(teacher);
        }

        [AllowAnonymous]
        [HttpGet("byEI/{id}")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetByEI(int id)
        {
            var teachers = await _teacherService.GetAllAsync();

            return Ok(teachers.Where(p => p.UniversityId == id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _teacherService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TeacherModel model)
        {
            await _teacherService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(TeacherModel model)
        {
            var created = await _teacherService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /* ------- discipline teachers ------- */

        [HttpGet("disciplineTeachers")]
        public async Task<ActionResult<IEnumerable<DisciplineTeacherModel>>> GetAllDisciplineTeachersAsync()
        {
            var disciplineTeachers = await _disciplineTeacherService.GetAllAsync();

            return Ok(disciplineTeachers);
        }

        [HttpGet("disciplineTeachers/{id}")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetTeachersByDisciplineIdAsync(int id)
        {
            var disciplineTeachers = await _disciplineTeacherService.GetTeachersIdByDisciplineIdAsync(id);

            return Ok(disciplineTeachers);
        }

        [HttpGet("disciplineTeachersLecturers/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> GetLecturersByDisciplineIdAsync(int id)
        {
            var disciplineTeachers = await _disciplineTeacherService.GetLecturersIdByDisciplineIdAsync(id);

            return Ok(disciplineTeachers);
        }


        [HttpGet("disciplineTeachersPracticians/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> GetPracticiansByDisciplineIdAsync(int id)
        {
            var disciplineTeachers = await _disciplineTeacherService.GetPracticiansIdByDisciplineIdAsync(id);

            return Ok(disciplineTeachers);
        }

        [HttpGet("facultyTeachers/{id}")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetTeachersByFacultyIdAsync(int id)
        {
            var facultyTeachers = await _teacherService.GetTeachersByFacultyId(id);

            return Ok(facultyTeachers);
        }

        [HttpPost("disciplineTeachers")]
        public async Task<ActionResult> AddDisciplineTeacher(DisciplineTeacherModel model)
        {
            var created = await _disciplineTeacherService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpDelete("disciplineTeachers/{id}")]
        public async Task<ActionResult> DeleteDisciplineTeacher(int id)
        {
            await _disciplineTeacherService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}