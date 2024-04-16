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
		private readonly NotesRepository repository = new();
		public Credentials_VM CredentialsVM { get; set; } = new();

		public User? ValidUser { get; set; }
		public bool ValidLogin { get; set; }
		public bool ValidPassword { get; set; }
		public void CheckUser()
		{
			if (!string.IsNullOrEmpty(CredentialsVM.LoginInput) && !string.IsNullOrEmpty(CredentialsVM.PasswordInput))
			{
				ValidUser = repository.CheckUserCredentials(CredentialsVM.LoginInput, CredentialsVM.PasswordInput, out bool validLogin);
				ValidLogin = validLogin;
				if(ValidUser != null)
				{
					ValidPassword = true;
				}
			}
		}
		//public async Task GetUserByCredentialsAsync()
		//{
		//	if (!string.IsNullOrEmpty(CredentialsVM.LoginInput) && !string.IsNullOrEmpty(CredentialsVM.PasswordInput))
		//	{
		//		List<User> userCollection = await TestRepository.GetAllUsersAsync();
		//		var matchUser = userCollection.FirstOrDefault(user => user.Сredentials.Login.Equals(CredentialsVM.LoginInput));
		//		if(matchUser is not null)
		//		{
		//			ValidLogin = true;
		//			if(matchUser.Сredentials.Password.Equals(CredentialsVM.PasswordInput))
		//			{
		//				ValidPassword = true;
		//				ValidUser = matchUser;
		//			}
		//		}
		//	}
		//}
		public int GetUserId()
		{
			if (ValidUser is null) return -1;
			return ValidUser.Id;
		}
	}
}
