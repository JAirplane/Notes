using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	public class User
	{
		public int Id { get; set; }
		public List<Note> UserNotes { get; set; } = [];
		public HashSet<Tag> UserTags { get; set; } = [];
		public Credentials Сredentials { get; set; } = new();
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public User(int _id, string _login, string _password, string _name, string _surname,
						string _email, string _phone)
		{
			Id = _id;
			Сredentials.Login = _login;
			Сredentials.Password = _password;
			Name = _name;
			Surname = _surname;
			Email = _email;
			Phone = _phone;
		}
	}
}
