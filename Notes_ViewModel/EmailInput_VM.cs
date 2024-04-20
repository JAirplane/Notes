using Notes_Model.Repository;
using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class EmailInput_VM
	{
		private readonly IRepository repository = new NotesRepository();
		public string Email { get; set; } = string.Empty;
		public int GetUserIdByEmail()
		{
			return repository.GetUserIdByEmail(Email);
		}
		virtual public string GetSecureCode()
		{
			Random rnd = new();
			string secureCode = string.Empty;
			for(int i = 0; i < 4; i++)
			{
				secureCode += rnd.Next(0, 10);
			}
			return secureCode;
		}
	}
}
