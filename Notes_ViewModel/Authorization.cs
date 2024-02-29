using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes_Model;
using Notes_Model.Repository;
using Notes_ViewModel.Models_VM;

namespace Notes_ViewModel
{
    public class Authorization
	{
		public Credentials_VM CredentialsVM { get; set; } = new();

		public User? validUser { get; set; }
		public bool ValidLogin { get; set; }
		public bool ValidPassword { get; set; }
		public async Task GetUserByCredentialsAsync()
		{
			if (!string.IsNullOrEmpty(CredentialsVM.LoginInput) && !string.IsNullOrEmpty(CredentialsVM.PasswordInput))
			{
				List<User> userCollection = await TestRepository.GetAllUsersAsync();
				var matchUser = userCollection.FirstOrDefault(user => user.Сredentials.Login.Equals(CredentialsVM.LoginInput));
				if(matchUser is not null)
				{
					ValidLogin = true;
					if(matchUser.Сredentials.Password.Equals(CredentialsVM.PasswordInput))
					{
						ValidPassword = true;
						validUser = matchUser;
					}
				}
			}
		}
		public int GetUserId()
		{
			if (validUser is null) return -1;
			return validUser.Id;
		}
	}
}
