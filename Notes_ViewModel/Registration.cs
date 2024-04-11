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
		public User_VM? NewUser { get; set; }
		private User? User_m { get; set; }
		public async Task<bool> LoginIsUniqueAsync()
		{
			if (NewUser is null || string.IsNullOrEmpty(NewUser.Сredentials.LoginInput)) return false;
			List<User> userCollection = await TestRepository.GetAllUsersAsync();
			var matchUser = userCollection.FirstOrDefault(user => user.Сredentials.Login.Equals(NewUser.Сredentials.LoginInput));
			if (matchUser is null) return true;
			return false;
		}
		public async Task<bool> EmailIsUniqueAsync()
		{
			if (NewUser is null || string.IsNullOrEmpty(NewUser.Email)) return false;
			List<User> userCollection = await TestRepository.GetAllUsersAsync();
			var matchUser = userCollection.FirstOrDefault(user => user.Email.Equals(NewUser.Email));
			if (matchUser is null) return true;
			return false;
		}
		public async Task<bool> AddNewUserAsync()
		{
			if (NewUser is null) return false;
			Credentials creds = new()
			{
				Login = NewUser.Сredentials.LoginInput,
				Password = NewUser.Сredentials.PasswordInput
			};
			User_m = new()
			{
				Id = TestRepository.GetNewUserId(),
				Сredentials = creds,
				Name = NewUser.Name,
				Surname = NewUser.Surname,
				Email = NewUser.Email,
				Phone = NewUser.Phone
			};
			await TestRepository.AddUserAsync(User_m);
			return true;
		}
	}
}
