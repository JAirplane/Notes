using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Logger
{
	public class NotesLogger : INotesLogger
	{
		public void Log(string logMessage)
		{
			try
			{
				using var logger = new StreamWriter("Program_logs.txt", true);
				logger.Write("\r\nLog Entry : ");
				logger.WriteLine(DateTime.Now.ToLongTimeString());
				logger.WriteLine("  :" + logMessage);
				logger.WriteLine("-------------------------------");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
