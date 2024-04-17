using Microsoft.EntityFrameworkCore;
using Notes_Model.PostgresDB;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public bool AddTagToNote(int userId, int noteId, int tagId)
		{
			throw new NotImplementedException();
		}

		public int AddUserNote(int userId, Note note)
		{
			if(note is null)
			{
				//TODO: write to log
				return -1;
			}
			if(!IsUserExists(userId))
			{
				//TODO: write to log
				return -1;
			}
			note.UserId = userId;
			using NotesContext db = new();
			db.UserNotes.Add(note);
			db.SaveChanges();
			return note.Id;
		}

		public bool AddUserTag(int userId, Tag tag)
		{
			throw new NotImplementedException();
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

		public User? CheckUserCredentials(string login, string password, out bool loginIsValid)
		{
			loginIsValid = IsLoginRegistered(login);
			if (string.IsNullOrEmpty(password))
			{
				return null;
			}
			using NotesContext db = new();
			return db.Users.Where(user => user.Сredentials.Password.Equals(password))
				.Include(user => user.UserTags)
				.Include(user => user.UserNotes)
					.ThenInclude(note => note.NoteTags)
				.FirstOrDefault();
		}

		public bool DeleteTagFromNote(int userId, int noteId, int tagId)
		{
			throw new NotImplementedException();
		}

		public bool DeleteUserNote(int userId, int noteId)
		{
			throw new NotImplementedException();
		}

		public bool DeleteUserTag(int userId, int tagId)
		{
			throw new NotImplementedException();
		}

		public List<User> GetAllUsers()
		{
			throw new NotImplementedException();
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

		public User? GetUserById(int userId)
		{
			throw new NotImplementedException();
		}

		public void AddNewUser(User newUser)
		{
			using NotesContext db = new();
			db.Users.Add(newUser);
			_ = db.SaveChanges();
		}

		public bool UpdateNoteHeader(int userId, int noteId, string header)
		{
			throw new NotImplementedException();
		}

		public bool UpdateNoteText(int userId, int noteId, string text)
		{
			throw new NotImplementedException();
		}

		public bool UpdateTagName(int userId, int tagId, string name)
		{
			throw new NotImplementedException();
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
