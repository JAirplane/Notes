using Notes_Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class ChangePassword_VM
	{
		private readonly NotesRepository repository = new();
		public int UserId { get; set; }
		public string NewPassword { get; set; } = string.Empty;
		public bool SetNewPassword()
		{
			if (string.IsNullOrEmpty(NewPassword))
			{
				return false;
			}
			return repository.ChangeUserPassword(UserId, NewPassword);
		}
	}
}
