using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model.Repository
{
	internal interface IRepository
	{
		public List<User> GetAllUsers();
	}
}
