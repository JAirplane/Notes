﻿using Notes_Model;
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
		private User? user;
		private LoggedInUser_VM? user_VM;
		public void SetUser(User? _user)
		{
			if(_user is null)
			{
				throw new Exception("AuthenticatedUserHandler_VM.SetUser() got null");
			}
			user = _user;
			ConvertFromUserToUserVM();
		}
		public bool ConvertFromUserToUserVM()
		{
			if (user is null)
			{
				return false;
			}
			user_VM = new(user);
			return true;
		}
		public IEnumerable<Note_VM> GetUserNotes()
		{
			if (user_VM is not null && user_VM.UserNotes is not null)
			{
				return user_VM.UserNotes;
			}
			else
			{
				return [];
			}
		}
	}
}