using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class NotificationHandler : INotificationHandler
	{
		private readonly Dictionary<int, CancellationTokenSource> notificationTasksCancellations = [];
		//it is sent from MainLayout render after user logged in
		public Func<string, string, Task>?  ShowNotification { get; set; }

		public bool AddTokenSource(int reminderId, CancellationTokenSource source)
		{
			if(reminderId < 0 || source is null)
			{
				return false;
			}
			notificationTasksCancellations.Add(reminderId, source);
			return true;
		}
		public bool CancelNotification(int reminderId)
		{
			if (!notificationTasksCancellations.TryGetValue(reminderId, out CancellationTokenSource? source))
			{
				return false;
			}
			if (source is null)
			{
				return false;
			}
			source.Cancel();
			notificationTasksCancellations.Remove(reminderId);
			source.Dispose();
			return true;
		}

		public async Task<bool> RunNotification(Reminder_VM reminder)
		{
			if (reminder is null || reminder.RemindTime < DateTime.Now) return false;
			var timeDiff = reminder.RemindTime - DateTime.Now;
			//delayed by timeDiff
			CancellationTokenSource tokenSource = new();
			AddTokenSource(reminder.Id, tokenSource);
			await Task.Delay((int)timeDiff.TotalMilliseconds, tokenSource.Token);
			if (tokenSource.Token.IsCancellationRequested)
			{
				return false;
			}
			ShowNotification?.Invoke(reminder.Header, reminder.Body);
			return true;
		}
	}
}
