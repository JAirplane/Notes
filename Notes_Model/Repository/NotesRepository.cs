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
		public bool AddTagToNote(int userId, int noteId, int tagId)
		{
			throw new NotImplementedException();
		}

		public bool AddUserNote(int userId, Note note)
		{
			throw new NotImplementedException();
		}

		public bool AddUserTag(int userId, Tag tag)
		{
			throw new NotImplementedException();
		}
		public bool CheckUserLogin(string login)
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
			loginIsValid = CheckUserLogin(login);
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

		public User? GetUserByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public User? GetUserById(int userId)
		{
			throw new NotImplementedException();
		}

		public bool SaveUser(User newUser)
		{
			throw new NotImplementedException();
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
	}
}
