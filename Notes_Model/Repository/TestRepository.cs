﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	public static class TestRepository
	{
		private static List<User>? users;
		private static int currentId = 0;
		static TestRepository()
		{
			users =
			[
				new(GetNewId(), "admin", "admin", "Eugene", "Shevchenko", "eugeneshevchenko0@gmail.com", "79615796948")
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
		public async static Task<List<User>> GetAllUsersAsync()
		{
			return await Task.Run(GetAllUsers);
		}
		public static bool AddUser(User newUser)
		{
			if (newUser is null) return false;
			users?.Add(newUser);
			return true;
		}
		public async static Task<bool> AddUserAsync(User newUser)
		{
			return await Task.Run(() => AddUser(newUser));
		}
		public static User? GetUserByEmail(string email)
		{
			List<User> userCollection = TestRepository.GetAllUsers();
			var user = userCollection.FirstOrDefault(user => user.Email.Equals(email));
			return user;
		}
		public async static Task<User?> GetUserByEmailAsync(string email)
		{
			return await Task.Run(() => GetUserByEmail(email));
		}
		public static User? GetUserById(int id)
		{
			List<User> userCollection = GetAllUsers();
			var user = userCollection.FirstOrDefault(user => user.Id.Equals(id));
			return user;
		}
		public async static Task<User?> GetUserByIdAsync(int id)
		{
			return await Task.Run(() => GetUserById(id));
		}
		public static int GetNewId()
		{
			return ++currentId;
		}
	}
}
