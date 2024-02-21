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
			var secrets = new ClientSecrets
			{
				ClientId = Environment.GetEnvironmentVariable("GMailClientId"),
				ClientSecret = Environment.GetEnvironmentVariable("GMailClientSecret")
			};
			var googleCredentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, new[] { GmailService.Scope.MailGoogleCom }, email, CancellationToken.None);
			if (googleCredentials.Token.IsStale)
			{
				await googleCredentials.RefreshTokenAsync(CancellationToken.None);
			}
			using (var client = new SmtpClient())
			{
				client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

				var oauth2 = new SaslMechanismOAuth2(googleCredentials.UserId, googleCredentials.Token.AccessToken);
				client.Authenticate(oauth2);

				using var emailMessage = new MimeMessage();
				emailMessage.From.Add(new MailboxAddress(EmailTitle, Email));
				emailMessage.To.Add(new MailboxAddress("", email));
				emailMessage.Subject = subject;
				emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
				{
					Text = message
				};

				await client.SendAsync(emailMessage);
				client.Disconnect(true);
			}
		}
	}
}
