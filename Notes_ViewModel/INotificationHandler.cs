using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public interface INotificationHandler
	{
		public Func<string, string, Task>? ShowNotification { get; set; }
		public bool AddTokenSource(int reminderId, CancellationTokenSource source);
		public bool CancelNotification(int reminderId);
		public Task<bool> RunNotification(Reminder_VM reminder);
		public void CancelAllNotifications();
	}
}
