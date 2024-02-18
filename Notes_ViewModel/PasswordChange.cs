using Notes_Model.Repository;
using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class PasswordChange
	{
		public string Email { get; set; } = string.Empty;
        public bool UserFound { get; set; }
        private User? matchedUser;
		public void FindUserByEmail()
		{
			List<User> userCollection = TestRepository.GetAllUsers();
			var user = userCollection.FirstOrDefault(user => user.Email.Equals(Email));
			if (user is not null)
			{
				matchedUser = user;
				UserFound = true;
			}
		}
	}
}
