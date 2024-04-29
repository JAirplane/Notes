using Microsoft.EntityFrameworkCore;
using Notes_Model.PostgresDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	public class NotesRepository : IRepository
	{
		public bool IsUserExists(int userId)
		{
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Id == userId).FirstOrDefault();
			return user != null;
		}
		public int AddUserTag(int userId, Tag tag)
		{
			if (!IsUserExists(userId))
			{
				return -1;
			}
			tag.UserId = userId;
			using NotesContext db = new();
			db.UserTags.Add(tag);
			db.SaveChanges();
			return tag.Id;
		}
		public bool AddTagToNote(int noteId, int tagId)
		{
			using NotesContext db = new();
			var tag = db.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			var note = db.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if (tag is null)
			{
				return false;
			}
			if (note is null)
			{
				return false;
			}
			note.NoteTags.Add(tag);
			db.SaveChanges();
			return true;
		}
		public bool RemoveTagFromNote(int noteId, int tagId)
		{
			using NotesContext db = new();
			var tag = db.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			var note = db.UserNotes.Include(note => note.NoteTags).Where(note => note.Id == noteId).FirstOrDefault();
			if (tag is null)
			{
				return false;
			}
			if (note is null)
			{
				return false;
			}
			note.NoteTags.Remove(tag);
			db.SaveChanges();
			return true;
		}
		public int AddUserNote(int userId, Note note)
		{
			if(!IsUserExists(userId))
			{
				return -1;
			}
			note.UserId = userId;
			using NotesContext db = new();
			db.UserNotes.Add(note);
			db.SaveChanges();
			return note.Id;
		}
		public bool IsEmailRegistered(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				return false;
			}
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
			if (user is null)
			{
				return false;
			}
			return true;
		}
		public bool IsLoginRegistered(string login)
		{
			if(string.IsNullOrEmpty(login))
			{
				return false;
			}
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Сredentials.Login.Equals(login)).FirstOrDefault();
			if(user is null)
			{
				return false;
			}
			return true;
		}

		public int CheckUserCredentials(string login, string password, out bool loginIsValid)
		{
			loginIsValid = IsLoginRegistered(login);
			if (!loginIsValid) return -1;
			if (string.IsNullOrEmpty(password))
			{
				return -1;
			}
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Сredentials.Password.Equals(password)).FirstOrDefault();
			if (user is null) return -1;
			return user.Id;
		}
		public bool DeleteUserNote(int noteId)
		{
			using NotesContext db = new();
			var note = db.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if(note is null)
			{
				return false;
			}
			db.UserNotes.Remove(note);
			db.SaveChanges();
			return true;
		}
		public bool DeleteUserTag(int tagId)
		{
			using NotesContext db = new();
			var tag = db.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null)
			{
				return false;
			}
			db.UserTags.Remove(tag);
			db.SaveChanges();
			return true;
		}
		public int GetUserIdByEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				return -1;
			}
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
			if(user is null)
			{
				return -1;
			}
			return user.Id;
		}

		public User? GetUser(int userId)
		{
			using NotesContext db = new();
			return db.Users.Where(user => user.Id.Equals(userId))
				.Include(user => user.UserTags)
				.Include(user => user.UserNotes)
					.ThenInclude(note => note.NoteTags)
				.FirstOrDefault();
		}

		public void AddNewUser(User newUser)
		{
			if (newUser == null) return;
			using NotesContext db = new();
			db.Users.Add(newUser);
			db.SaveChanges();
		}

		public bool UpdateNoteHeader(int noteId, string header)
		{
			using NotesContext db = new();
			var note = db.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if (note is null) return false;
			note.Header = header;
			db.SaveChanges();
			return true;
		}

		public bool UpdateNoteText(int noteId, string text)
		{
			using NotesContext db = new();
			var note = db.UserNotes.Where(note => note.Id == noteId).FirstOrDefault();
			if (note is null) return false;
			note.Body = text;
			db.SaveChanges();
			return true;
		}
		//works for reminders only, converts DateTime to utc
		public bool UpdateRemindTime(int reminderId, DateTime remindTime)
		{
			using NotesContext db = new();
			var reminder = db.UserReminders.Where(reminder => reminder.Id == reminderId).FirstOrDefault();
			if (reminder is null) return false;
			reminder.RemindTime = DateTime.SpecifyKind(remindTime, DateTimeKind.Utc);
			db.SaveChanges();
			return true;
		}
		public bool UpdateTagName(int tagId, string name)
		{
			using NotesContext db = new();
			var tag = db.UserTags.Where(tag => tag.Id == tagId).FirstOrDefault();
			if (tag is null) return false;
			tag.TagName = name;
			db.SaveChanges();
			return true;
		}

		public bool ChangeUserPassword(int userId, string password)
		{
			if(string.IsNullOrEmpty(password))
			{
				return false;
			}
			using NotesContext db = new();
			var user = db.Users.Where(user => user.Id == userId).FirstOrDefault();
			if(user is null)
			{
				return false;
			}
			user.Сredentials.Password = password;
			db.SaveChanges();
			return true;
		}
	}
}
