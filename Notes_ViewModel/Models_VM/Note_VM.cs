using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	public class Note_VM
	{
		public int Id { get; set; }
		public DateTime CreationDateTime { get; set; } = DateTime.Now;
		public string Header { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public HashSet<Tag_VM> NoteTags { get; set; } = [];
		public Note_VM() { }
		//constructor do not copy tags, User_VM constructor do this instead
		public Note_VM(Note note)
		{
			Id = note.Id;
			CreationDateTime = note.CreationDateTime;
			Header = note.Header;
			Body = note.Body;
		}
	}
}
