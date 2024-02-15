using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	internal class User
	{
		public int Id { get; set; }
		public List<Note> UserNotes { get; set; } = [];
		public List<Reminder> UserReminders { get; set; } = [];
		public Credentials Сredentials { get; set; } = new();
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}
}
