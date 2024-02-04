using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	internal class User
	{
		public int Id { get; set; }
		public List<Note> UserNotes { get; set; } = [];
		public List<Reminder> UserReminders { get; set; } = [];
	}
}
