using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Notes_Model.PostgresDB
{
	public class NotesContext : DbContext
	{
		readonly StreamWriter dbLogWriter = new("DB_logs.txt", true);
		public DbSet<User> Users { get; set; }
		public DbSet<Note> UserNotes { get; set; }
		public DbSet<Reminder> UserReminders { get; set; }
		public DbSet<Tag> UserTags { get; set; }
		public NotesContext() : base() {}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder()
						.AddJsonFile("appsettings.json")
						.SetBasePath(Directory.GetCurrentDirectory())
						.Build();
			optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			optionsBuilder.LogTo(dbLogWriter.WriteLine, LogLevel.Warning);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Credentials class owned by User, so it has no table
			//Notes + Reminders and Tags are many to many
			modelBuilder.Entity<Note>().UseTphMappingStrategy(); //one table for Notes and Reminders
		}
		public override void Dispose()
		{
			base.Dispose();
			dbLogWriter.Dispose();
		}

		public override async ValueTask DisposeAsync()
		{
			await base.DisposeAsync();
			await dbLogWriter.DisposeAsync();
		}
	}
}
