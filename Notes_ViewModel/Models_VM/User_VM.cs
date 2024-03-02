using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	//this class used before login
	public class User_VM
	{
		public Credentials_VM Сredentials { get; set; } = new();
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public User_VM() { }
		public User_VM(string _login, string _password,
			string _name, string _surname, string _email, string _phone)
		{
			Сredentials.LoginInput = _login;
			Сredentials.PasswordInput = _password;
			Name = _name;
			Surname = _surname;
			Email = _email;
			Phone = _phone;
		}
	}
}
