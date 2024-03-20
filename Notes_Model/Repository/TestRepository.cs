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
		private static int currentUserId = 0;
		private static int currentNoteId = 0;
		private static int currentReminderId = 0;
		private static int currentTagId = 0;
		static TestRepository()
		{
			users =
			[
				new(GetNewUserId(), "admin", "admin", "Eugene", "Shevchenko", "eugeneshevchenko0@gmail.com", "79615796948")
			];
			users[0].UserTags.Add(new Tag { Id = GetNewTagId(), TagName = "#11111" });
			users[0].UserTags.Add(new Tag { Id = GetNewTagId(), TagName = "#2222" });
			users[0].UserTags.Add(new Tag { Id = GetNewTagId(), TagName = "#33333333333333" });
			users[0].UserTags.Add(new Tag { Id = GetNewTagId(), TagName = "#444444444" });
			users[0].UserTags.Add(new Tag { Id = GetNewTagId(), TagName = "#5555555555555555555555555555" });
			var note = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note",
				Body = "This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes" +
				"This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes" +
				"This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes This note created for test purposes"
			};
			for(int i = 1; i < 6; i++)
			{
				var tag = users[0].UserTags.FirstOrDefault(tag => tag.Id.Equals(i));
				if(tag is not null)
					note.NoteTags.Add(tag);
			}
			users[0].UserNotes.Add(note);
			var note1 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note1",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note1);
			var note2 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note2",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note2);
			var note3 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note3",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note3);
			var note4 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note4",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note4);
			var note5 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note5",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note5);
			var note6 = new Note
			{
				Id = GetNewNoteId(),
				Header = "Test Note6",
				Body = "This note created for test purposes"
			};
			users[0].UserNotes.Add(note6);
			var reminder1 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header1",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(12)
			};
			users[0].UserReminders.Add(reminder1);
			var reminder2 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header2",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(-45)
			};
			users[0].UserReminders.Add(reminder2);
			var reminder3 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header3",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(-123)
			};
			users[0].UserReminders.Add(reminder3);
			var reminder4 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header4",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(-222)
			};
			users[0].UserReminders.Add(reminder4);
			var reminder5 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header5",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(288)
			};
			users[0].UserReminders.Add(reminder5);
			var reminder6 = new Reminder
			{
				Id = GetNewReminderId(),
				Header = "Reminder header6",
				Body = "This reminder created for test purposes",
				RemindTime = DateTime.Now.AddHours(-3000)
			};
			users[0].UserReminders.Add(reminder6);
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
		public static int GetNewUserId()
		{
			return ++currentUserId;
		}
		public static int GetNewNoteId()
		{
			return ++currentNoteId;
		}
		public static int GetNewReminderId()
		{
			return ++currentReminderId;
		}
		public static int GetNewTagId()
		{
			return ++currentTagId;
		}
	}
}
