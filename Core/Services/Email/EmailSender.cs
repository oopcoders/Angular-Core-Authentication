using System.Threading.Tasks;

namespace Core.Services.Email
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsnc(string fromAddress, string toAddress, string subject, string message)
		{
			throw new System.NotImplementedException();
		}
	}
}