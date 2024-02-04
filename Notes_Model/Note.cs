using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	internal class Note
	{
		public DateTime CreationDateTime { get; set; } = DateTime.Now;
		public string Header { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public List<Tag> NoteTags { get; set; } = [];
	}
}
