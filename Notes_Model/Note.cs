using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	public class Note
	{
		public int Id { get; set; }
		public DateTime CreationDateTime { get; set; } = DateTime.Now;
		public string Header { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public HashSet<Tag> NoteTags { get; set; } = [];

		public int UserId { get; set; }

	}
}
