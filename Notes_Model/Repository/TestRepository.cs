using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	public static class TestRepository
	{
		private static List<User>? users;
		static TestRepository()
		{
			users =
			[
				new("admin", "admin", "Eugene", "Shevchenko", "eugeneshevchenko0@gmail.com", "79615796948")
			];
		}
		public static List<User> GetAllUsers()
		{
			if(users is not null)
			{
				return users;
			}
			return [];
		}
		public static bool AddUser(User newUser)
		{
			if (newUser is null) return false;
			users?.Add(newUser);
			return true;
		}
	}
}
