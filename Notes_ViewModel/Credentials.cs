﻿using System.ComponentModel.DataAnnotations;

namespace Notes_ViewModel
{
    public class Credentials
    {
		[Required(ErrorMessage = "Enter Login")]
		[MinLength(4, ErrorMessage = "Login must contain at least 4 characters")]
		[MaxLength(16, ErrorMessage = "Login must contain at most 16 characters")]
		public string LoginInput { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter password")]
		[MinLength(4, ErrorMessage = "Password must contain at least 4 characters")]
		[MaxLength(16, ErrorMessage = "Password must contain at most 16 characters")]
		public string PasswordInput { get; set; } = string.Empty;
	}
}
