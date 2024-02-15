using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	internal class TestRepository : IRepository
	{
		private List<User>? users;
        public TestRepository()
        {
			users =
			[
				new()
				{
					Id = 1,
					Сredentials = {LoginInput = "admin", PasswordInput = "admin"},
					Name = "Eugene",
					Surname = "Shevchenko",
					Email = "eugeneshevchenko0@gmail.com",
					Phone = "79615796948"
				}
			];
        }
        public List<User> GetAllUsers()
		{
			if(users is not null)
			{
				return users;
			}
			return [];
		}
	}
}
