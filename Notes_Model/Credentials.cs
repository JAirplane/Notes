﻿using System.ComponentModel.DataAnnotations;

namespace Notes_Model
{
	public class Credentials
	{
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
