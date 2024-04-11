using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	public interface IRepository
	{
		public List<User> GetAllUsers();
		public bool SaveUser(User newUser);
		public User? GetUserByEmail(string email);
		public User? GetUserById(int userId);
		public bool AddUserNote(int userId, Note note);
		public bool AddUserTag(int userId, Tag tag);
		public bool AddTagToNote(int userId, int noteId, int tagId);
		public bool DeleteUserNote(int userId, int noteId);
		public bool DeleteUserTag(int userId, int tagId);
		public bool DeleteTagFromNote(int userId, int noteId, int tagId);
		public bool UpdateNoteHeader(int userId, int noteId, string header);
		public bool UpdateNoteText(int userId, int noteId, string text);
		public bool UpdateTagName(int userId, int tagId, string name);
	}
}
