using Business.Models;
using Emailer.Models;

namespace Emailer.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
        Task<EmailSubscriptionModel> AddAsync(EmailSubscriptionModel model);
        Task<IEnumerable<EmailSubscriptionModel>> GetAllAsync();
        Task<EmailSubscriptionModel> GetByIdAsync(int id);
        Task DeleteByIdAsync(int modelId);
    }
}
