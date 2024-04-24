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
		private readonly IRepository repository = new NotesRepository();
		public Credentials_VM CredentialsVM { get; set; } = new();

		public int ValidUserId { get; set; } = -1;
		public bool ValidLogin { get; set; }
		public bool ValidPassword { get; set; }
		public void CheckUser()
		{
			if (!string.IsNullOrEmpty(CredentialsVM.LoginInput) && !string.IsNullOrEmpty(CredentialsVM.PasswordInput))
			{
				ValidUserId = repository.CheckUserCredentials(CredentialsVM.LoginInput, CredentialsVM.PasswordInput, out bool validLogin);
				ValidLogin = validLogin;
				if(ValidUserId != -1)
				{
					ValidPassword = true;
				}
			}
		}
	}
}
