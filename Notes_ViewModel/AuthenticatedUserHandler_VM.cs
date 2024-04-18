using Notes_ViewModel.Helpers;
using Notes_Model;
using Notes_Model.Repository;
using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class AuthenticatedUserHandler_VM
	{
		private readonly NotesRepository repository = new();
		private LoggedInUser_VM user_VM = new();
		public void SetUser(User? _user)
		{
			if(_user is null)
			{
				throw new Exception("AuthenticatedUserHandler_VM.SetUser() got null");
			}
			user_VM = new(_user);
		}
		public Reminder_VM? ConvertNoteToReminder(Note_VM? note)
		{
			if(note is not null)
			{
				var reminder = new Reminder_VM
				{
					Id = TestRepository.GetNewNoteId(),
					CreationDateTime = note.CreationDateTime,
					Header = note.Header,
					Body = note.Body,
					NoteTags = note.NoteTags
				};
				user_VM?.UserNotes.Remove(note);
				user_VM?.UserNotes.Add(reminder);
				return reminder;
			}
			return null;
		}
		public IEnumerable<Note_VM> GetUserNotes()
		{
			if (user_VM.UserNotes is not null)
			{
				return user_VM.UserNotes.Where(note => note is not Reminder_VM);
			}
			return [];
		}
		public IEnumerable<Note_VM> GetUserReminders()
		{
			if (user_VM is not null && user_VM.UserNotes is not null)
			{
				return user_VM.UserNotes.Where(reminder => reminder is Reminder_VM);
			}
			else
			{
				return [];
			}
		}
		public IEnumerable<Note_VM>? GetUserNotesByTag(Tag_VM tag)
		{
			return user_VM?.UserNotes.Where(note => note.NoteTags.Contains(tag));
		}
		public IEnumerable<Tag_VM> GetUserTags()
		{
			return user_VM.UserTags;
		}
		public void DeleteUserNote(int noteId)
		{
			bool isDeleted = user_VM.DeleteNoteById(noteId);
			if (!isDeleted)
			{
				//TODO: to log file
			}
			bool isDeletedFromDb = repository.DeleteUserNote(noteId);
			if (!isDeletedFromDb)
			{
				//TODO: to log file
			}
		}
		public void AddNewNote(NoteContent content)
		{
			Note_VM? note;
			Note? note_model;
			if (content.RemindDateTime is null)
			{
				note = new();
				note_model = new();
			}
			else
			{
				note = new Reminder_VM()
				{
					RemindTime = (DateTime)content.RemindDateTime
				};
				note_model = new Reminder()
				{
					RemindTime = DateTime.SpecifyKind((DateTime)content.RemindDateTime, DateTimeKind.Utc)
				};
			}
			note.CreationDateTime = DateTime.Now;
			note.Header = content.NoteHeader;
			note.Body = content.NoteText;
			user_VM.UserNotes.Add(note);

			note_model.CreationDateTime = DateTime.SpecifyKind(note.CreationDateTime, DateTimeKind.Utc);
			note_model.Header = note.Header;
			note_model.Body = note.Body;
			int noteId = repository.AddUserNote(user_VM.Id, note_model);
			if(noteId == -1)
			{
				//TODO: write error to log
			}
			else
			{
				note.Id = noteId;
			}
		}
		public Tag_VM? AddNewTag(string tagName)
		{
			if (tagName.Length == 0) return null;
			var fixedTagName = "#" + tagName;
			if(fixedTagName.Length > 30)
			{
				fixedTagName = fixedTagName[..30];
			}
			var tag = new Tag
			{
				TagName = fixedTagName
			};
			repository.AddUserTag(user_VM.Id, tag);
			var tag_vm = new Tag_VM(tag);
			user_VM.UserTags.Add(tag_vm);
			return tag_vm;
		}
		public void DeleteUserTag(int tagId)
		{
			user_VM.DeleteTagById(tagId);
			//TODO: Delete from user and save changes in db
		}
		//return all tags that contains tagName
		public IEnumerable<Tag_VM> GetUserTagsByTagName(string tagName)
		{
			return GetUserTags().Where(tag => tag.TagName.Contains(tagName));
		}
		//return one tag with name equals to tagName
		public Tag_VM? GetUserTagByName(string tagName)
		{
			return GetUserTags().FirstOrDefault(tag => tag.TagName.Equals(tagName));
		}
		public void NullifyUser()
		{
			user_VM = new();
		}
	}
}
