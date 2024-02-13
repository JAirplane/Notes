using Notes_ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class RegistrationFormFields
	{
		[Required(ErrorMessage = "Введите Login")]
		[MinLength(4, ErrorMessage = "Login должен содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Login должен содержать не более 16 символов")]
		public string LoginInput { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите пароль")]
		[MinLength(4, ErrorMessage = "Пароль должен содержать не менее 4 символов")]
		[MaxLength(16, ErrorMessage = "Пароль должен содержать не более 16 символов")]
		public string PasswordInput { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите имя")]
		[MinLength(2, ErrorMessage = "Имя должно содержать не менее 2 символов")]
		[MaxLength(16, ErrorMessage = "Имя должно содержать не более 16 символов")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Имя должно содержать только латинские или русские буквы")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите фамилию")]
		[MinLength(2, ErrorMessage = "Фамилия должна содержать не менее 2 символов")]
		[MaxLength(16, ErrorMessage = "Фамилия должна содержать не более 16 символов")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Фамилия должна содержать только латинские или русские буквы")]
		public string Surname { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите Email")]
		[EmailAddress(ErrorMessage = "Недействительный Email адрес")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите номер телефона")]
		[RegularExpression(@"^[0][1-9]([.][0-9][0-9]){4}", ErrorMessage = "Неверный номер телефона!")]
		public string Phone { get; set; } = string.Empty;
	}
}
