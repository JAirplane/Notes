using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	public interface IRepository
	{
		public bool IsUserExists(int userId);
		public bool IsEmailRegistered(string email);
		public bool IsLoginRegistered(string login);
		public int CheckUserCredentials(string login, string password, out bool loginIsValid);
		public void AddNewUser(User newUser);
		public int GetUserIdByEmail(string email);
		public User? GetUser(int userId);
		public bool ChangeUserPassword(int userId, string password);
		public int AddUserNote(int userId, Note note);
		public int AddUserTag(int userId, Tag tag);
		public bool AddTagToNote(int noteId, int tagId);
		public bool DeleteUserNote(int noteId);
		public bool DeleteUserTag(int tagId);
		public bool RemoveTagFromNote(int noteId, int tagId);
		public bool UpdateNoteHeader(int noteId, string header);
		public bool UpdateNoteText(int noteId, string text);
		public bool UpdateRemindTime(int reminderId, DateTime remindTime);
		public bool UpdateTagName(int tagId, string name);
	}
}
