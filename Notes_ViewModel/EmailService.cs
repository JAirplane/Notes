using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Notes_ViewModel
{
	public class EmailService
	{
		public string Smtp { get; set; } = "smtp.gmail.com";
		public int Port { get; set; } = 587;
		public bool UseSSL { get; set; }
		public string Email { get; set; } = "service.notes00@gmail.com";
		public string Password { get; set; } = "11Notes11";
		public string EmailTitle { get; set; } = "Notes Password Reset";

        public async Task SendEmailAsync(string email, string subject, string message)
		{
			using var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress(EmailTitle, Email));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message
			};

			using var client = new SmtpClient();
			await client.ConnectAsync(Smtp, Port, UseSSL);
			await client.AuthenticateAsync(Email, Password);
			await client.SendAsync(emailMessage);
			await client.DisconnectAsync(true);
		}
	}
}
