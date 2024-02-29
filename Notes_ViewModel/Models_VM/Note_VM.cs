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
		public List<Tag_VM> NoteTags { get; set; } = [];
	}
}
