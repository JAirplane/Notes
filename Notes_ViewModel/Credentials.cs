using System.ComponentModel.DataAnnotations;

namespace Notes_ViewModel
{
    public class Credentials
    {
		[Required(ErrorMessage ="Введите имя пользователя")]
		[MinLength(4, ErrorMessage = "Имя пользователя должно содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Имя пользователя должно содержать не более 16 символов")]
		public string LoginInput { get; set; } = string.Empty;
		[Required(ErrorMessage = "Введите пароль")]
		[MinLength(4, ErrorMessage = "Пароль должен содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Пароль должен содержать не более 16 символов")]
		public string PasswordInput { get; set; } = string.Empty;
	}
}
