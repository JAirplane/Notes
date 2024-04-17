using Notes_Model;
using Notes_Model.Repository;
using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
    public class Registration
	{
		private readonly NotesRepository repository = new();
		public User_VM NewUser { get; set; } = new();
		public bool LoginIsUnique()
		{
			if (NewUser is null || string.IsNullOrEmpty(NewUser.Сredentials.LoginInput)) return false;
			var loginAlreadyInDB = repository.IsLoginRegistered(NewUser.Сredentials.LoginInput);
			if(loginAlreadyInDB) return false;
			return true;
		}
		public bool EmailIsUnique()
		{
			if (NewUser is null || string.IsNullOrEmpty(NewUser.Email)) return false;
			var emailAlreadyInDB = repository.IsEmailRegistered(NewUser.Email);
			if (emailAlreadyInDB) return false;
			return true;
		}
		public bool AddNewUser()
		{
			if (NewUser is null) return false;
			User user_model = new()
			{
				Сredentials = new Credentials()
				{
					Login = NewUser.Сredentials.LoginInput,
					Password = NewUser.Сredentials.PasswordInput
				},
				Name = NewUser.Name,
				Surname = NewUser.Surname,
				Email = NewUser.Email,
				Phone = NewUser.Phone
			};
			repository.AddNewUser(user_model);
			return true;
		}
	}
}
