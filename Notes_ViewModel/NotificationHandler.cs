
using NLog;
using Notes_Model;
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

		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
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
			logger.Error($"NotificationHandler->CancelNotification() -> Notification cancellation for {reminderId} at {DateTime.Now}.");
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
			try
			{
				if (reminder is null || reminder.RemindTime < DateTime.Now) return false;
				logger.Error($"NotificationHandler->RunNotification() -> Notification running for {reminder.Id} with header {reminder.Header} at {DateTime.Now}.");
				var timeDiff = reminder.RemindTime - DateTime.Now;
				//delayed by timeDiff
				CancellationTokenSource tokenSource = new();
				AddTokenSource(reminder.Id, tokenSource);
				await Task.Delay((int)timeDiff.TotalMilliseconds, tokenSource.Token);
				ShowNotification?.Invoke(reminder.Header, reminder.Body);
				logger.Error($"NotificationHandler->RunNotification() -> Notification invoked for {reminder.Id} with header {reminder.Header} at {DateTime.Now}.");
				return true;
			}
			catch (OperationCanceledException)
			{
				logger.Error($"NotificationHandler->RunNotification() -> Notification was cancelled for {reminder.Id} with header {reminder.Header}.");
				//task is cancelled
				return false;
			}
			catch (Exception ex)
			{
				logger.Error($"NotificationHandler->RunNotification() -> {ex.Message}");
				return false;
			}
		}

		public void CancelAllNotifications()
		{
			foreach(var entry in notificationTasksCancellations)
			{
				entry.Value.Cancel();
				entry.Value.Dispose();
			}
			notificationTasksCancellations.Clear();
		}
	}
}
