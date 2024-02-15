﻿using Notes_ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	//this class is actually like UserData class
	//it was created due to blazor validation limitation
	//Credentials field in UserData is LoginInput and PasswordInput here
	public class RegistrationFormFields
	{
		[Required(ErrorMessage = "Enter Login")]
		[MinLength(4, ErrorMessage = "Login must contain at least 4 characters")]
		[MaxLength(16, ErrorMessage = "Login must contain at most 16 characters")]
		public string LoginInput { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter password")]
		[MinLength(4, ErrorMessage = "Password must contain at least 4 characters")]
		[MaxLength(16, ErrorMessage = "Password must contain at most 16 characters")]
		public string PasswordInput { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter your name")]
		[MaxLength(16, ErrorMessage = "Name must contain at most 50 characters")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "The name must contain Latin or Russian letters")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter your surname")]
		[MaxLength(16, ErrorMessage = "Surname must contain at most 50 characters")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "The surname must contain Latin or Russian letters")]
		public string Surname { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter Email")]
		[EmailAddress(ErrorMessage = "Invalid Email address")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter your phone number")]
		[RegularExpression(@"^([\+][1-9])[(]\d{3}[)]\d{3}([-]\d{2}){2}", ErrorMessage = "Invalid phone number!")]
		public string Phone { get; set; } = string.Empty;
	}
}
