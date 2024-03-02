using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	public class LoggedInUser_VM : User_VM
	{
		public int Id { get; set; }
		public List<Note_VM> UserNotes { get; set; } = [];
		public List<Reminder_VM> UserReminders { get; set; } = [];
		public LoggedInUser_VM() { }
		public LoggedInUser_VM(User user) :base(user.Сredentials.Login, user.Сredentials.Password,
			user.Name, user.Surname, user.Email, user.Phone)
		{
			Id = user.Id;
			foreach(Note note in user.UserNotes)
			{
				UserNotes.Add(new Note_VM(note));
			}
			foreach(Reminder reminder in user.UserReminders)
			{
				UserReminders.Add(new Reminder_VM(reminder));
			}
		}
	}
}
