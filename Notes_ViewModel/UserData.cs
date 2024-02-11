using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class UserData
	{
		public int Id { get; set; }
		public Credentials Сredentials { get; set; } = new();
		[Required(ErrorMessage = "Введите имя")]
		[MinLength(4, ErrorMessage = "Имя должно содержать не менее 2 символов")]
		[MaxLength(16, ErrorMessage = "Имя должно содержать не более 16 символов")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Имя должно содержать только латинские и русские буквы")]
		public string Name { get; set; } = string.Empty;
		[Required(ErrorMessage = "Введите фамилию")]
		[MinLength(4, ErrorMessage = "Фамилия должна содержать не менее 2 символов")]
		[MaxLength(16, ErrorMessage = "Фамилия должна содержать не более 16 символов")]
		[RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Фамилия должна содержать только латинские и русские буквы")]
		public string Surname { get; set; } = string.Empty;
		[Required(ErrorMessage = "Введите Email")]
		[EmailAddress(ErrorMessage = "Недействительный Email адрес")]
		public string Email { get; set; } = string.Empty;
		[Required(ErrorMessage = "Введите номер телефона")]
		public string Phone { get; set; } = string.Empty;
	}
}
