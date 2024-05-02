using Notes_ViewModel.Helpers;
using Notes_Model;
using Notes_Model.Repository;
using Notes_ViewModel.Models_VM;
using NLog;

namespace Notes_ViewModel
{
	public class AuthenticatedUserHandler_VM
	{
		private readonly IRepository repository;
		private LoggedInUser_VM user_VM = new();
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		public AuthenticatedUserHandler_VM(IRepository repo)
		{
			repository = repo;
		}
		public void SetUser(int userId)
		{
			try
			{
				if (userId == -1)
				{
					throw new Exception("AuthenticatedUserHandler_VM.SetUser() got bad userId");
				}
				var user = repository.GetUser(userId);
				if (user is null)
				{
					logger.Error($"AuthenticatedUserHandler_VM -> SetUser(): User was not found in DB. Time: {DateTime.Now}");
					return;
				}
				user_VM = new LoggedInUser_VM(user);
			}
			catch(Exception ex)
			{
				logger.Error(ex.Message);
			}
		}
		//Remind time is wrong here, need to be updated further
		public int ConvertNoteToReminder(Note_VM? note, DateTime remindTime)
		{
			if(note is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> ConvertNoteToReminder() got null Note_VM argument. Time: {DateTime.Now}");
				return -1;
			}
			if (string.IsNullOrWhiteSpace(note.Header))
			{
				return -1;
			}
			NoteContent content = new()
			{
				NoteHeader = note.Header,
				NoteText = note.Body,
				RemindDateTime = remindTime,
			};
			int reminderId = AddNewNote(content);
			if(reminderId == -1) return -1;
			DeleteUserNote(note.Id);
			return reminderId;
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
			return user_VM.UserNotes.Where(note => note.NoteTags.Contains(tag));
		}
		public IEnumerable<Tag_VM> GetUserTags()
		{
			return user_VM.UserTags;
		}
		public void DeleteUserNote(int noteId)
		{
			var note = user_VM.UserNotes.FirstOrDefault(note => note.Id.Equals(noteId));
			if (note != null)
			{
				user_VM.UserNotes.Remove(note);
			}
			else
			{
				logger.Error($"AuthenticatedUserHandler_VM -> DeleteUserNote(): Note was not found in User_VM. Time: {DateTime.Now}");
			}
			bool isDeletedFromDb = repository.DeleteUserNote(noteId);
			if (!isDeletedFromDb)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> DeleteUserNote() -> repository.DeleteUserNote: Note was not found in DB. Time: {DateTime.Now}");
			}
		}
		public int AddNewNote(NoteContent content)
		{
			if (string.IsNullOrWhiteSpace(content.NoteHeader))
			{
				return -1;
			}
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
				logger.Error($"AuthenticatedUserHandler_VM -> AddNewNote() -> repository.AddUserNote: user was not found in DB. Time: {DateTime.Now}");
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
				logger.Error($"AuthenticatedUserHandler_VM -> UpdateNoteData(): Note was not found in User_VM. Time: {DateTime.Now}");
				return false;
			}
			if(string.IsNullOrWhiteSpace(content.NoteHeader))
			{
				return false;
			}
			note.Header = content.NoteHeader;
			note.Body = content.NoteText;
			if(note is not Reminder_VM && content.RemindDateTime is not null)
			{
				int reminderId = ConvertNoteToReminder(note, (DateTime)content.RemindDateTime);
				if (reminderId == -1) return false;
				return true;
			}
			bool headerUpdated = repository.UpdateNoteHeader(noteId, content.NoteHeader);
			if(!headerUpdated)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> UpdateNoteData() -> repository.UpdateNoteHeader: Update failed. Note was not found in DB. Time: {DateTime.Now}");
				return false;
			}
			bool textUpdated = repository.UpdateNoteText(noteId, content.NoteText);
			if(!textUpdated)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> UpdateNoteData() -> repository.UpdateNoteText: Update failed. Note was not found in DB. Time: {DateTime.Now}");
				return false;
			}
			if(note is Reminder_VM reminder)
			{
				if(content.RemindDateTime is null)
				{
					logger.Error($"AuthenticatedUserHandler_VM -> UpdateNoteData(): Reminder got null RemindTime to update. Time: {DateTime.Now}");
					return false;
				}
				reminder.RemindTime = (DateTime)content.RemindDateTime;
				bool remindTimeUpdatedInDB = repository.UpdateRemindTime(noteId, (DateTime)content.RemindDateTime);
				if(!remindTimeUpdatedInDB)
				{
					logger.Error($"AuthenticatedUserHandler_VM -> UpdateNoteData() -> repository.UpdateRemindTime: Update failed. Note was not found in DB. Time: {DateTime.Now}");
					return false;
				}
			}
			return true;
		}
		public Tag_VM? AddNewTag(string tagName)
		{
			if (tagName.Length == 0 || string.IsNullOrWhiteSpace(tagName))
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddNewTag(): tagName argument is empty or whitespace. Time: {DateTime.Now}");
				return null;
			}
			var fixedTagName = "#" + tagName;
			if(fixedTagName.Length > 30)
			{
				fixedTagName = fixedTagName[..30];
			}
			var tag = new Tag
			{
				TagName = fixedTagName
			};
			int tagId = repository.AddUserTag(user_VM.Id, tag);
			if(tagId == -1)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddNewTag() -> repository.AddUserTag(): User was not found in DB. Time: {DateTime.Now}");
			}
			var tag_vm = new Tag_VM(tag);
			user_VM.UserTags.Add(tag_vm);
			return tag_vm;
		}
		public void AddExistingTagToNote(int noteId, int tagId)
		{
			var note = user_VM.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if(note is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddExistingTagToNote(): Note was not found in User_VM. Time: {DateTime.Now}");
				return;
			}
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddExistingTagToNote(): Tag was not found in User_VM. Time: {DateTime.Now}");
				return;
			}
			note.NoteTags.Add(tag);
			bool addedSuccessfully = repository.AddTagToNote(noteId, tagId);
			if(!addedSuccessfully)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddExistingTagToNote() -> repository.AddTagToNote(): Note or tag was not found in DB. Time: {DateTime.Now}");
			}
		}
		public void RemoveExistingTagFromNote(int noteId, int tagId)
		{
			var note = user_VM.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if (note is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddExistingTagToNote(): Note was not found in User_VM. Time: {DateTime.Now}");
				return;
			}
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> AddExistingTagToNote(): Tag was not found in User_VM. Time: {DateTime.Now}");
				return;
			}
			note.NoteTags.Remove(tag);
			bool addedSuccessfully = repository.RemoveTagFromNote(noteId, tagId);
			if (!addedSuccessfully)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> RemoveExistingTagFromNote() -> repository.RemoveTagFromNote(): Note or tag was not found in DB. Time: {DateTime.Now}");
			}
		}
		public void DeleteUserTag(int tagId)
		{
			var tag = user_VM.UserTags.FirstOrDefault(tag => tag.Id.Equals(tagId));
			if (tag is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> DeleteUserTag(): Tag was not found in User_VM. Time: {DateTime.Now}");
				return;
			}	
			foreach (var note in user_VM.UserNotes)
			{
				var nTag = note.NoteTags.FirstOrDefault(tag => tag.Id.Equals(tagId));
				if (nTag is not null)
				{
					note.NoteTags.Remove(nTag);
				}
			}
			user_VM.UserTags.Remove(tag);

			bool tagDeleted = repository.DeleteUserTag(tagId);
			if(!tagDeleted)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> DeleteUserTag() -> repository.DeleteUserTag(): Tag was not found in DB. Time: {DateTime.Now}");
			}
		}
		public void UpdateTagData(int tagId, string tagName)
		{
			if (string.IsNullOrWhiteSpace(tagName)) return;
			var tag = user_VM.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if(tag is null)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> UpdateTagData(): Tag was not found in User_VM collection. Time: {DateTime.Now}");
				return;
			}
			if (tagName.Length > 30)
			{
				tagName = tagName[..30];
			}
			tagName = "#" + tagName;
			tag.TagName = tagName;
			bool updated = repository.UpdateTagName(tagId, tagName);
			if(!updated)
			{
				logger.Error($"AuthenticatedUserHandler_VM -> UpdateTagData() -> repository.UpdateTagName(): Tag was not found in DB. Time: {DateTime.Now}");
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
