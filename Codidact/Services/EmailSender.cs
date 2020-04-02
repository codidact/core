using Codidact;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Codidact.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly AppSettings _settings;
		private readonly ILogger<EmailSender> _log;

		public EmailSender(AppSettings settings, ILogger<EmailSender> logger)
		{
			_settings = settings;
			_log = logger;
		}


		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
#if DEBUG
			var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {htmlMessage}\n\n";
			File.AppendAllText("emails.txt", emailMessage);
#endif

			_log.LogInformation($"Sending [{subject}] email to [{email}]");

			using (var msg = new MailMessage(_settings.EmailFrom, email, subject, htmlMessage /*plainMessage*/))
			{
/*				using AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlMessage, new System.Net.Mime.ContentType("text/html"));
				msg.AlternateViews.Add(htmlView);*/

				// configure the smtp server
				var smtp = new SmtpClient(_settings.EmailServer, _settings.EmailPort);
				var cred = new System.Net.NetworkCredential(_settings.EmailUser, _settings.EmailPassword);

				smtp.UseDefaultCredentials = false;
				smtp.EnableSsl = true;
				smtp.Credentials = cred;

				// send the message
				try
				{
					smtp.Send(msg);
				}
				catch (Exception ex)
				{
					_log.LogError(ex, $"Failed to send message to [{email}]");
				}
			}

			return Task.CompletedTask;
		}
	}
}

