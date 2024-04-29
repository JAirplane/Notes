using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Logger
{
	public interface INotesLogger
	{
		public void Log(string logMessage);
	}
}
