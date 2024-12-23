using Emailer.Interfaces;
using Emailer.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net;
using MimeKit.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SimpleService.Interfaces;
using AutoMapper;
using Business.Models;
using SimpleService.Entities;

namespace Emailer.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailSenderRepository _emailerRepository;

        private readonly IMapper _mapper;

        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig, IEmailSenderRepository emailerRepository, IMapper mapper)
        {
            _emailConfig = emailConfig;
            _emailerRepository = emailerRepository;
            _mapper = mapper;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        public async Task<EmailSubscriptionModel> AddAsync(EmailSubscriptionModel model)
        {
            var faculty = _mapper.Map<EmailSubscription>(model);

            var facultyCreated = await _emailerRepository.AddAsync(faculty);

            await _emailerRepository.SaveAsync();

            return _mapper.Map<EmailSubscriptionModel>(facultyCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _emailerRepository.DeleteByIdAsync(modelId);

            await _emailerRepository.SaveAsync();
        }

        public async Task<IEnumerable<EmailSubscriptionModel>> GetAllAsync()
        {
            var faculties = await _emailerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EmailSubscriptionModel>>(faculties);
        }

        public async Task<EmailSubscriptionModel> GetByIdAsync(int id)
        {
            var faculty = await _emailerRepository.GetByIdAsync(id);

            return _mapper.Map<EmailSubscriptionModel>(faculty);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            /*emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };*/
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        static bool MySslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // If there are no errors, then everything went smoothly.
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Note: MailKit will always pass the host name string as the `sender` argument.
            var host = (string)sender;

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
            {
                // This means that the remote certificate is unavailable. Notify the user and return false.
                Console.WriteLine("The SSL certificate was not available for {0}", host);
                return false;
            }

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0)
            {
                // This means that the server's SSL certificate did not match the host name that we are trying to connect to.
                var certificate2 = certificate as X509Certificate2;
                var cn = certificate2 != null ? certificate2.GetNameInfo(X509NameType.SimpleName, false) : certificate.Subject;

                Console.WriteLine("The Common Name for the SSL certificate did not match {0}. Instead, it was {1}.", host, cn);
                return false;
            }

            // The only other errors left are chain errors.
            Console.WriteLine("The SSL certificate for the server could not be validated for the following reasons:");

            // The first element's certificate will be the server's SSL certificate (and will match the `certificate` argument)
            // while the last element in the chain will typically either be the Root Certificate Authority's certificate -or- it
            // will be a non-authoritative self-signed certificate that the server admin created. 
            foreach (var element in chain.ChainElements)
            {
                // Each element in the chain will have its own status list. If the status list is empty, it means that the
                // certificate itself did not contain any errors.
                if (element.ChainElementStatus.Length == 0)
                    continue;

                Console.WriteLine("\u2022 {0}", element.Certificate.Subject);
                foreach (var error in element.ChainElementStatus)
                {
                    // `error.StatusInformation` contains a human-readable error string while `error.Status` is the corresponding enum value.
                    Console.WriteLine("\t\u2022 {0}", error.StatusInformation);
                }
            }

            return false;
        }
    }
}
