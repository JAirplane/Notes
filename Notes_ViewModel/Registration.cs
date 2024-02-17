using Notes_Model;
using Notes_Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class Registration
	{
		public User_VM? NewUser { get; set; }
		private User? User_m { get; set; }
		public bool AddNewUser()
		{
			if (NewUser is null) return false;
			User_m = new User(NewUser.Сredentials.LoginInput, NewUser.Сredentials.PasswordInput,
				NewUser.Name, NewUser.Surname, NewUser.Email, NewUser.Phone);
			TestRepository.AddUser(User_m);
			return true;
		}
	}
}
