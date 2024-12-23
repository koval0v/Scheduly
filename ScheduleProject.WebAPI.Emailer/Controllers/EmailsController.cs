using Business.Models;
using Emailer.Interfaces;
using Emailer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailsController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpGet("testEmail")]
        public async Task GetEmails()
        {
            var message = new Message(new string[] { "vzlobinkov@gmail.com" }, "Test email", "This is the content from our email.");
            await _emailSender.SendEmailAsync(message);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailSubscriptionModel>>> Get()
        {
            var faculties = await _emailSender.GetAllAsync();

            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmailSubscriptionModel>> Get(int id)
        {
            var faculty = await _emailSender.GetByIdAsync(id);

            return Ok(faculty);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _emailSender.DeleteByIdAsync(id);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Add(EmailSubscriptionModel model)
        {
            var created = await _emailSender.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}