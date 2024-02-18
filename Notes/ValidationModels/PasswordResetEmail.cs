using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class PasswordResetEmail
	{
		[Required(ErrorMessage = "Please enter Email")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; } = string.Empty;
	}
}
