using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	public class Reminder_VM : Note_VM
	{
		public DateTime RemindTime { get; set; }
		public Reminder_VM() { }
		public Reminder_VM(Reminder reminder) : base(reminder)
		{
			RemindTime = reminder.RemindTime;
		}
		public Reminder_VM(Note_VM note)
		{
			CreationDateTime = note.CreationDateTime;
			Header = note.Header;
			Body = note.Body;
			NoteTags = note.NoteTags;
		}
	}
}
