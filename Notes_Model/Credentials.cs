using Microsoft.EntityFrameworkCore;

namespace Notes_Model
{
	[Owned]
	public class Credentials
	{
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
