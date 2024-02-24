using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Google.Apis.Gmail.v1;
using MailKit.Security;
using MimeKit.Text;
//hesm ttak tyas sneo
namespace Notes_ViewModel
{
	public class EmailService
	{
		private string FromEmail { get; set; } = "service.notes00@gmail.com";
		public string Smtp { get; set; } = "smtp.gmail.com";
		public int Port { get; set; } = 587;
		public bool UseSSL { get; set; }
		public string EmailTitle { get; set; } = "Notes Password Reset";

		public async Task SendEmailAsync(string toEmail, string subject, string message)
		{
			// create email message
			var mail = new MimeMessage();
			mail.From.Add(MailboxAddress.Parse(FromEmail));
			mail.To.Add(MailboxAddress.Parse(toEmail));
			mail.Subject = subject;
			mail.Body = new TextPart(TextFormat.Plain) { Text = message };

			// send email
			using var smtp = new SmtpClient();
			smtp.Connect(Smtp, Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(FromEmail, Environment.GetEnvironmentVariable("GoogleAPIPassword", EnvironmentVariableTarget.User));
			await smtp.SendAsync(mail);
			smtp.Disconnect(true);
		}
	}
}
