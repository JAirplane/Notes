using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class PasswordResetCode
	{
		public int EmailCode { get; set; }

		[Compare(nameof(EmailCode), ErrorMessage = "Invalid code")]
		public int EmailCodeInput { get; set; }
	}
}
