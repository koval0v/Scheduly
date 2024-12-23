using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using System.Collections.Generic;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ScheduleService.Models;
using MassTransit;
using TokenService.RabbitMQModels;

namespace SimpleService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private IScheduleService _scheduleService;
        private readonly IBus _busService;

        public ScheduleController(IScheduleService scheduleService, IBus busService)
        {
            _scheduleService = scheduleService;
            _busService = busService;
        }

        [HttpPost("sendSubscriptionEmail")]
        public async Task<string> SendAddUserToEIEmailTemplate(ScheduleSubscriptionEmailTemplate emailData)
        {
            if (emailData is not null)
            {
                Uri uri = new Uri("rabbitmq://localhost/subscriptionQueue");
                var endpoint = await _busService.GetSendEndpoint(uri);
                await endpoint.Send(emailData);
                return "true";
            }

            return "false";
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleModel>>> Get()
        {
            var schedules = await _scheduleService.GetAllAsync();

            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleModel>> Get(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);

            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _scheduleService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ScheduleModel model)
        {
            await _scheduleService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(ScheduleModel model)
        {
            var created = await _scheduleService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /* schedule discipline */
        [AllowAnonymous]
        [HttpPost("disciplines/groupsemester")]
        public async Task<ActionResult<IEnumerable<ScheduleDisciplineModel>>> GetByGroupAndSemesterId(DisciplinesRequestModel model)
        {
            var scheduleDisciplines = await _scheduleService.GetAllDisciplinesAsync();

            return Ok(scheduleDisciplines.Where(p => _scheduleService.GetByIdAsync(p.ScheduleId).Result.GroupId == model.GroupId && p.Semester == model.Semester));
        }

        [AllowAnonymous]
        [HttpPost("disciplines/teachersemester")]
        public async Task<ActionResult<IEnumerable<ScheduleDisciplineModel>>> GetByTeacherAndSemesterId(TeacherDisciplinesRequestModel model)
        {
            var scheduleDisciplines = await _scheduleService.GetAllDisciplinesAsync();

            return Ok(scheduleDisciplines.Where(p => p.TeacherId == model.TeacherId && p.Semester == model.Semester));
        }

        [HttpGet("disciplines")]
        public async Task<ActionResult<IEnumerable<ScheduleDisciplineModel>>> GetDisciplines()
        {
            var scheduleDisciplines = await _scheduleService.GetAllDisciplinesAsync();

            return Ok(scheduleDisciplines);
        }

        [HttpGet("disciplines/{id}")]
        public async Task<ActionResult<ScheduleDisciplineModel>> GetDiscipline(int id)
        {
            var scheduleDiscipline = await _scheduleService.GetDisciplineByIdAsync(id);

            return Ok(scheduleDiscipline);
        }

        [HttpDelete("disciplines/{id}")]
        public async Task<ActionResult> DeleteDiscipline(int id)
        {
            await _scheduleService.DeleteDisciplineByIdAsync(id);

            return NoContent();
        }

        [HttpPut("disciplines/{id}")]
        public async Task<ActionResult> UpdateDiscipline(int id, ScheduleDisciplineModel model)
        {
            await _scheduleService.UpdateDisciplineAsync(id, model);

            return NoContent();
        }

        [HttpPost("disciplines")]
        public async Task<ActionResult> AddDiscipline(ScheduleDisciplineModel model)
        {
            var created = await _scheduleService.AddDisciplineAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}