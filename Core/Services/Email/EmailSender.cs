using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Core.Services.Email
{
	public class EmailSender : IEmailSender
	{

		private readonly IConfiguration _config;

		public EmailSender(IConfiguration config)
		{
			_config = config;

		}
		public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
		{
			var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);

			using (var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
			{
				Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"])
			})
			{
				await client.SendMailAsync(mailMessage);
			}
		}
	}
}