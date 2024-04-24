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
		private readonly IRepository repository = new NotesRepository();
		private LoggedInUser_VM user_VM = new();
		public void SetUser(int userId)
		{
			if(userId == -1)
			{
				throw new Exception("AuthenticatedUserHandler_VM.SetUser() got bad userId");
			}
			var user = repository.GetUser(userId);
			if(user is null)
			{
				//TODO: to log file
				return;
			}
			user_VM = new LoggedInUser_VM(user);
		}
		//Remind time is wrong here, need to be updated further
		public int ConvertNoteToReminder(Note_VM? note)
		{
			if(note is not null)
			{
				DeleteUserNote(note.Id);
				NoteContent content = new()
				{
					NoteHeader = note.Header,
					NoteText = note.Body,
					RemindDateTime = DateTime.Now,
				};
				return AddNewNote(content);
			}
			return -1;
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
		public int AddNewNote(NoteContent content)
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
			note_model.CreationDateTime = DateTime.SpecifyKind(note.CreationDateTime, DateTimeKind.Utc);
			note_model.Header = content.NoteHeader;
			note_model.Body = content.NoteText;
			int noteId = repository.AddUserNote(user_VM.Id, note_model);

			note.Header = content.NoteHeader;
			note.Body = content.NoteText;
			user_VM.UserNotes.Add(note);

			if(noteId == -1)
			{
				//TODO: write error to log
			}
			else
			{
				note.Id = noteId;
			}
			return noteId;
		}
		public bool UpdateNoteData(int noteId, NoteContent content)
		{
			var note = user_VM.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if(note is null)
			{
				//TODO: to log
				return false;
			}
			note.Header = content.NoteHeader;
			note.Body = content.NoteText;
			if(note is not Reminder_VM && content.RemindDateTime is not null)
			{
				int reminderId = ConvertNoteToReminder(note);
				if (reminderId == -1) return false;
				repository.UpdateRemindTime(reminderId, (DateTime)content.RemindDateTime);
				return true;
			}
			repository.UpdateNoteHeader(noteId, content.NoteHeader);
			repository.UpdateNoteText(noteId, content.NoteText);
			if(note is Reminder_VM reminder)
			{
				if(content.RemindDateTime is null)
				{
					//TODO: to log
					return false;
				}
				reminder.RemindTime = (DateTime)content.RemindDateTime;
				bool remindTimeUpdatedInDB = repository.UpdateRemindTime(noteId, (DateTime)content.RemindDateTime);
				if(!remindTimeUpdatedInDB)
				{
					//TODO: to log
					return false;
				}
			}
			return true;
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
		public void AddExistingTagToNote(int noteId, int tagId)
		{
			var note = user_VM.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if(note is null)
			{
				//TODO: to log
				return;
			}
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null)
			{
				//TODO: to log
				return;
			}
			note.NoteTags.Add(tag);
			repository.AddTagToNote(noteId, tagId);
		}
		public void RemoveExistingTagFromNote(int noteId, int tagId)
		{
			var note = user_VM.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if (note is null)
			{
				//TODO: to log
				return;
			}
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null)
			{
				//TODO: to log
				return;
			}
			note.NoteTags.Remove(tag);
			repository.RemoveTagFromNote(noteId, tagId);
		}
		public void DeleteUserTag(int tagId)
		{
			user_VM.DeleteTagById(tagId);
			bool tagDeleted = repository.DeleteUserTag(tagId);
			if(!tagDeleted)
			{
				//TODO: to log file
			}
		}
		public void UpdateTagData(int tagId, string tagName)
		{
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if(tag is null)
			{
				//TODO: to log file
				return;
			}
			tag.TagName = tagName;
			bool updated = repository.UpdateTagName(tagId, tagName);
			if(!updated)
			{
				//TODO: to log file
			}
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
