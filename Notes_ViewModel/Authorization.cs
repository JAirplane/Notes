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
		public int GetUserId()
		{
			if (ValidUser is null) return -1;
			return ValidUser.Id;
		}
	}
}
