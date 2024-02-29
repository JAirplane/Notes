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
	}
}
