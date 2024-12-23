using DisciplineService.Dtos;
using DisciplineService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupModel>>> Get()
        {
            var groups = await _groupService.GetAllAsync();

            return Ok(groups);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupModel>> Get(int id)
        {
            var group = await _groupService.GetByIdAsync(id);

            return Ok(group);
        }

        [AllowAnonymous]
        [HttpGet("byEI/{id}")]
        public async Task<ActionResult<IEnumerable<GroupModel>>> GetByEI(int id)
        {
            var groups = await _groupService.GetAllAsync();

            return Ok(groups.Where(p => p.UniversityId == id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _groupService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, GroupModel model)
        {
            await _groupService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(GroupModel model)
        {
            var created = await _groupService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}