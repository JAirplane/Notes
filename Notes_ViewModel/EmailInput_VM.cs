using Notes_Model.Repository;
using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class EmailInput_VM
	{
		public string Email { get; set; } = string.Empty;
		public int GetUserIdByEmail()
		{
			List<User> userCollection = TestRepository.GetAllUsers();
			var user = userCollection.FirstOrDefault(user => user.Email.Equals(Email));
			if (user is not null)
			{
				return user.Id;
			}
			return -1;
		}
		virtual public string GetSecureCode()
		{
			Random rnd = new();
			string secureCode = string.Empty;
			for(int i = 0; i < 4; i++)
			{
				secureCode += rnd.Next();
			}
			return secureCode;
		}
	}
}
