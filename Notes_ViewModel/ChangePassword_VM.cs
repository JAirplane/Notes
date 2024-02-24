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
		public int UserId { get; set; }
		public string NewPassword { get; set; } = string.Empty;
		public async Task<bool> SetNewPasswordAsync()
		{
			if (string.IsNullOrEmpty(NewPassword))
			{
				return false;
			}
			var user = await TestRepository.GetUserByIdAsync(UserId);
			if(user != null)
			{
				user.Сredentials.Password = NewPassword;
				return true;
			}
			return false;
		}
	}
}
