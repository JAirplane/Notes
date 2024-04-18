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
		public List<User> GetAllUsers();
		public bool IsEmailRegistered(string email);
		public bool IsLoginRegistered(string login);
		public User? CheckUserCredentials(string login, string password, out bool loginIsValid);
		public void AddNewUser(User newUser);
		public int GetUserIdByEmail(string email);
		public User? GetUserById(int userId);
		public bool ChangeUserPassword(int userId, string password);
		public int AddUserNote(int userId, Note note);
		public int AddUserTag(int userId, Tag tag);
		public bool AddTagToNote(int userId, int noteId, int tagId);
		public bool DeleteUserNote(int noteId);
		public bool DeleteUserTag(int userId, int tagId);
		public bool DeleteTagFromNote(int userId, int noteId, int tagId);
		public bool UpdateNoteHeader(int userId, int noteId, string header);
		public bool UpdateNoteText(int userId, int noteId, string text);
		public bool UpdateTagName(int userId, int tagId, string name);
	}
}
