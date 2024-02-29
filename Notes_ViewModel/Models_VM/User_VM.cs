using Notes_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	//this class used before login
	public class User_VM
	{
		public Credentials_VM Сredentials { get; set; } = new();
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}
}
