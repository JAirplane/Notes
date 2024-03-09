using Notes_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel.Models_VM
{
	public class Tag_VM
	{
		public int Id { get; set; }
		public string TagName { get; set; } = string.Empty;
		public Tag_VM() { }
		public Tag_VM(Tag tag)
		{
			Id = tag.Id;
			TagName = tag.TagName;
		}
		public override bool Equals(object? obj)
		{
			if(obj is not null)
			{
				var comparedTo = obj as Tag_VM;
				if(comparedTo != null)
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
