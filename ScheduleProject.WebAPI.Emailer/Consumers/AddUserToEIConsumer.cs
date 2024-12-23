using Emailer.Interfaces;
using Emailer.Models;
using MassTransit;
using TokenService.RabbitMQModels;

namespace ScheduleService.Consumers
{
    public class AddUserToEIConsumer : IConsumer<AddUserToEIEmailTemplate>
    {
        private readonly IEmailSender _emailSender;

        public AddUserToEIConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<AddUserToEIEmailTemplate> context)
        {
            await Task.Run(() => { var obj = context.Message;
                Console.WriteLine($"RECEIVED -> {context.Message}");
            });

            var message = new Message(new string[] { context.Message.UserToAdd }, "SCHEDULY: Management request",
                $"Hello, {context.Message.UserToAdd}!" + "\n" + $"User {context.Message.UserWantsToAdd} invited you to edit the schedule {context.Message.EiName} together." + "\n" +
                $"You can accept or reject the invitation in Scheduly app:" + "\n" +
                $"http://localhost:4200/management/faculties");
            await _emailSender.SendEmailAsync(message);
        }
    }
}
