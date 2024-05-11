using Microsoft.Identity.Client;
using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class NotificationManager: INotificationManager
	{
		private Notification? notification;
		private readonly Dictionary<int, CancellationTokenSource> notificationTasksCancellations = [];

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

		public bool RunNotification(Reminder_VM reminder)
		{
			if (reminder is null || reminder.RemindTime < DateTime.Now) return false;
			var timeDiff = reminder.RemindTime - DateTime.Now;
			//delayed by timeDiff
			CancellationTokenSource tokenSource = new();
			tokenSources.AddTokenSource(reminder.Id, tokenSource);
			if (notification is not null)
			{
				await notification.ShowSimpeNotification(reminder.Header, reminder.Body, timeDiff, tokenSource.Token);
			}
		}
	}
}
