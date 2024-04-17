using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Model
{
	public class Tag
	{
		public int Id { get; set; }
		public string TagName { get; set; } = string.Empty;
		public List<Note> Notes { get; set; } = [];

		public int UserId { get; set; }
		public User? User { get; set; }
		public override bool Equals(object? obj)
		{
			if (obj is not null)
			{
				var comparedTo = obj as Tag;
				if (comparedTo != null)
				{
					return TagName.Equals(comparedTo.TagName);
				}

			}
			return false;
		}

		public override int GetHashCode()
		{
			return TagName.GetHashCode();
		}
	}
	
}
