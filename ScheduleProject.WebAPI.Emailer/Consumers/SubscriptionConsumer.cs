using Emailer.Interfaces;
using Emailer.Models;
using MassTransit;
using TokenService.RabbitMQModels;

namespace Emailer.Consumers
{
    public class SubscriptionConsumer : IConsumer<ScheduleSubscriptionEmailTemplate>
    {
        private readonly IEmailSender _emailSender;

        public SubscriptionConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<ScheduleSubscriptionEmailTemplate> context)
        {
            await Task.Run(() => {
                var obj = context.Message;
                Console.WriteLine($"RECEIVED -> {context.Message}");
            });

            var message = new Message(new string[] { context.Message.UserName }, "SCHEDULY: Subscription request",
                $"Hello, {context.Message.UserName}!" + "\n" + $"The {context.Message.EiName} schedule for {context.Message.GroupCipher} group is completely filled for {context.Message.Semester} semester!" + "\n" +
                $"You can see it in Scheduly app:" + "\n" +
                $"http://localhost:4200/schedule/external/group/" + $"{context.Message.EiLink}");
            await _emailSender.SendEmailAsync(message);
        }
    }
}
