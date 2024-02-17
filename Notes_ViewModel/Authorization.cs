using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes_Model;
using Notes_Model.Repository;

namespace Notes_ViewModel
{
	public class Authorization
	{
		public Credentials_VM CredentialsVM { get; set; } = new();

		private User? validUser;
		public bool ValidLogin { get; set; }
		public bool ValidPassword { get; set; }
		public void GetUserByCredentials()
		{
			if (!string.IsNullOrEmpty(CredentialsVM.LoginInput) && !string.IsNullOrEmpty(CredentialsVM.PasswordInput))
			{
				List<User> userCollection = TestRepository.GetAllUsers();
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
	}
}
