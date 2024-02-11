using System.ComponentModel.DataAnnotations;

namespace Notes_ViewModel
{
    public class Credentials
    {
		[Required(ErrorMessage ="Введите Login")]
		[MinLength(4, ErrorMessage = "Login должен содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Login должен содержать не более 16 символов")]
		public string LoginInput { get; set; } = string.Empty;
		[Required(ErrorMessage = "Введите пароль")]
		[MinLength(4, ErrorMessage = "Пароль должен содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Пароль должен содержать не более 16 символов")]
		public string PasswordInput { get; set; } = string.Empty;
	}
}
