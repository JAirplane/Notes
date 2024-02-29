using Notes_Model;
using Notes_Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class AuthenticatedUserHandler_VM
	{
		private User? user;
		public void SetUser(User? _user)
		{
			if(_user is null)
			{
				throw new Exception("AuthenticatedUserHandler_VM.SetUser() got null");
			}
			user = _user;
		}
		public IEnumerable<Note> GetUserNotes()
		{
			if (user is not null && user.UserNotes is not null)
			{
				return user.UserNotes;
			}
			else
			{
				return [];
			}
		}
	}
}
