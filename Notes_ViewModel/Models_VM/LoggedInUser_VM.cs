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
		public HashSet<Tag_VM> UserTags { get; set; } = [];
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
			foreach(Tag tag in user.UserTags)
			{
				UserTags.Add(new Tag_VM(tag));
			}
		}
		public bool DeleteNoteById(int noteId)
		{
			var note = UserNotes.FirstOrDefault(note => note.Id.Equals(noteId));
			if (note != null)
			{
				UserNotes.Remove(note);
				return true;
			}
			return false;
		}
		public void DeleteTagById(int tagId)
		{
			var tag = UserTags.FirstOrDefault(tag => tag.Id.Equals(tagId));
			if (tag != null)
			{
				UserTags.Remove(tag);
			}
			foreach(var note in UserNotes)
			{
				var nTag = note.NoteTags.FirstOrDefault(tag => tag.Id.Equals(tagId));
				if(nTag is not null)
				{
					note.NoteTags.Remove(nTag);
				}
			}
			foreach (var reminder in UserReminders)
			{
				var rTag = reminder.NoteTags.FirstOrDefault(tag => tag.Id.Equals(tagId));
				if(rTag is not null)
				{
					reminder.NoteTags.Remove(rTag);
				}
			}
		}
	}
}
