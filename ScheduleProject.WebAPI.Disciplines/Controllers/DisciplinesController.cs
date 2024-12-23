using DisciplineService.Interfaces;
using DisciplineService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisciplineService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;
        private readonly ICatalogService _catalogService;
        private readonly ICatalogDisciplineService _catalogDisciplineService;
        private readonly ISpecialtyDisciplineService _specialtyDisciplineService;

        public DisciplinesController(IDisciplineService disciplineService, ICatalogService catalogService,
            ICatalogDisciplineService catalogDisciplineService, ISpecialtyDisciplineService specialtyDisciplineService)
        {
            _disciplineService = disciplineService;
            _catalogService = catalogService;
            _catalogDisciplineService = catalogDisciplineService;
            _specialtyDisciplineService = specialtyDisciplineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> Get()
        {
            var disciplines = await _disciplineService.GetAllAsync();

            return Ok(disciplines);
        }

        [HttpGet("byEI/{id}")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetByEI(int id)
        {
            var disciplines = await _disciplineService.GetAllAsync();

            return Ok(disciplines.Where(p => p.UniversityId == id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplineModel>> Get(int id)
        {
            var discipline = await _disciplineService.GetByIdAsync(id);

            return Ok(discipline);
        }

        [HttpGet("selective")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetSelective()
        {
            var discipline = await _disciplineService.GetSelective();

            return Ok(discipline);
        }

        [HttpGet("mandatory")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetMandatory()
        {
            var discipline = await _disciplineService.GetMandatory();

            return Ok(discipline);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _disciplineService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DisciplineModel model)
        {
            await _disciplineService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(DisciplineModel model)
        {
            var created = await _disciplineService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /* ------- catalogs ------- */

        [HttpGet("catalogs")]
        public async Task<ActionResult<IEnumerable<CatalogModel>>> GetCatalogs()
        {
            var catalogs = await _catalogService.GetAllAsync();

            return Ok(catalogs);
        }

        [HttpGet("catalogs/{id}")]
        public async Task<ActionResult<CatalogModel>> GetCatalogById(int id)
        {
            var catalog = await _catalogService.GetByIdAsync(id);

            return Ok(catalog);
        }

        [HttpGet("catalogs/byEI/{id}")]
        public async Task<ActionResult<IEnumerable<CatalogModel>>> GetCatalogsByEI(int id)
        {
            var catalogs = await _catalogService.GetAllAsync();

            return Ok(catalogs.Where(p => p.UniversityId == id));
        }

        [HttpPost("catalogs")]
        public async Task<ActionResult> AddCatalog(CatalogModel model)
        {
            var created = await _catalogService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpDelete("catalogs/{id}")]
        public async Task<ActionResult> DeleteCatalog(int id)
        {
            await _catalogService.DeleteByIdAsync(id);

            return NoContent();
        }

        /* ------- catalog disciplines ------- */

        [HttpGet("catalogDisciplines")]
        public async Task<ActionResult<IEnumerable<CatalogDisciplineModel>>> GetAllCatalogDisciplinesAsync()
        {
            var catalogDisciplines = await _catalogDisciplineService.GetAllAsync();

            return Ok(catalogDisciplines);
        }

        [HttpGet("catalogDisciplines/{id}")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetDisciplinesByCatalogIdAsync(int id)
        {
            var catalogDisciplines = await _catalogDisciplineService.GetDisciplinesByCatalogIdAsync(id);

            return Ok(catalogDisciplines);
        }

        [HttpPost("catalogDisciplines")]
        public async Task<ActionResult> AddCatalogDiscipline(CatalogDisciplineModel model)
        {
            var created = await _catalogDisciplineService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpDelete("catalogDisciplines/{id}")]
        public async Task<ActionResult> DeleteCatalogDiscipline(int id)
        {
            await _catalogDisciplineService.DeleteByIdAsync(id);

            return NoContent();
        }

        /* ------- specialty disciplines ------- */

        [AllowAnonymous]
        [HttpGet("specialtyDisciplines")]
        public async Task<ActionResult<IEnumerable<SpecialtyDisciplineModel>>> GetAllSpecialtyDisciplinesAsync()
        {
            var specialtyDisciplines = await _specialtyDisciplineService.GetAllAsync();

            return Ok(specialtyDisciplines);
        }

        [AllowAnonymous]
        [HttpGet("specialtyDisciplines/{specialtyId}")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetDisciplinesBySpecialtyIdAsync(int specialtyId)
        {
            var specialtyDisciplines = await _specialtyDisciplineService.GetDisciplinesBySpecialtyIdAsync(specialtyId);

            return Ok(specialtyDisciplines);
        }

        [AllowAnonymous]
        [HttpGet("facultyDisciplines/{facultyId}")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetDisciplinesByFacultyIdAsync(int facultyId)
        {
            var specialtyDisciplines = await _disciplineService.GetDisciplinesByFacultyId(facultyId);

            return Ok(specialtyDisciplines);
        }

        [AllowAnonymous]
        [HttpPost("specialtyDisciplines/bySpecialtyAndSemester")]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetDisciplinesBySpecialtyIdAsync(SpecialtyDisciplinesRequestModel model)
        {
            var specialtyDisciplines = await _specialtyDisciplineService.GetDisciplinesBySpecialtyIdAndSemesterAsync(model.SpecialtyId, model.Semester);

            return Ok(specialtyDisciplines);
        }

        [HttpPost("specialtyDisciplines")]
        public async Task<ActionResult> AddSpecialtyDiscipline(SpecialtyDisciplineModel model)
        {
            var created = await _specialtyDisciplineService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpDelete("specialtyDisciplines/{id}")]
        public async Task<ActionResult> DeleteSpecialtyDiscipline(int id)
        {
            await _specialtyDisciplineService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
