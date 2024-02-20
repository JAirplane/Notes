using System.ComponentModel.DataAnnotations;

namespace Notes.ValidationModels
{
	public class ChangePasswordFields
	{
		[Required(ErrorMessage = "Enter new password")]
		[MinLength(4, ErrorMessage = "Password must contain at least 4 characters")]
		[MaxLength(16, ErrorMessage = "Password must contain at most 16 characters")]
		public string PasswordInput { get; set; } = string.Empty;

		[Compare(nameof(PasswordInput), ErrorMessage = "Passwords must be equal")]
		public string PasswordConfirmInput { get; set; } = string.Empty;
	}
}
