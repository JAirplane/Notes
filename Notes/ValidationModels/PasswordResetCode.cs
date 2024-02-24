using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ValidationModels
{
	public class PasswordResetCode
	{
		public string EmailCode { get; set; } = string.Empty;

		[Compare(nameof(EmailCode), ErrorMessage = "Invalid code")]
		public string EmailCodeInput { get; set; } = string.Empty;
	}
}
