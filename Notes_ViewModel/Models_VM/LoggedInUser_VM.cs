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
		public HashSet<Tag_VM> UserTags { get; set; } = [];
		public bool NotificationPermission { get; set; } = false;
		public LoggedInUser_VM() { }
		public LoggedInUser_VM(User user) :base(user.Сredentials.Login, user.Сredentials.Password,
			user.Name, user.Surname, user.Email, user.Phone)
		{
			Id = user.Id;
			foreach (Tag tag in user.UserTags)
			{
				UserTags.Add(new Tag_VM(tag));
			}
			foreach (Note note in user.UserNotes)
			{
				Note_VM? note_VM = null;
				if(note is Reminder reminder)
				{
					note_VM = new Reminder_VM(reminder);
				}
				else
				{
					note_VM = new Note_VM(note);
				}
				UserNotes.Add(note_VM);
				foreach(Tag tag in note.NoteTags)
				{
					Tag_VM? tag_VM = UserTags.Where(_tag => _tag.Id == tag.Id).FirstOrDefault();
					if(tag_VM is not null)
					{
						note_VM.NoteTags.Add(tag_VM);
					}
				}
			}
		}
	}
}
