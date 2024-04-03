﻿using Notes_ViewModel.Helpers;
using Notes_Model;
using Notes_Model.Repository;
using Notes_ViewModel.Models_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_ViewModel
{
	public class AuthenticatedUserHandler_VM
	{
		private User? user;
		private LoggedInUser_VM? user_VM;
		public void SetUser(User? _user)
		{
			if(_user is null)
			{
				throw new Exception("AuthenticatedUserHandler_VM.SetUser() got null");
			}
			user = _user;
			if(!ConvertFromUserToUserVM())
			{
				throw new Exception("AuthenticatedUserHandler_VM.ConvertFromUserToUserVM() failed to convert user");
			}
		}
		public bool ConvertFromUserToUserVM()
		{
			if (user is null)
			{
				return false;
			}
			user_VM = new(user);
			return true;
		}
		public Reminder_VM? ConvertNoteToReminder(Note_VM? note)
		{
			if(note is not null)
			{
				var reminder = new Reminder_VM
				{
					Id = TestRepository.GetNewNoteId(),
					CreationDateTime = note.CreationDateTime,
					Header = note.Header,
					Body = note.Body,
					NoteTags = note.NoteTags
				};
				user_VM?.UserNotes.Remove(note);
				user_VM?.UserNotes.Add(reminder);
				return reminder;
			}
			return null;
		}
		public IEnumerable<Note_VM> GetUserNotes()
		{
			if (user_VM is not null && user_VM.UserNotes is not null)
			{
				return user_VM.UserNotes.Where(reminder => reminder is not Reminder_VM);
			}
			else
			{
				return [];
			}
		}
		public IEnumerable<Note_VM> GetUserReminders()
		{
			if (user_VM is not null && user_VM.UserNotes is not null)
			{
				return user_VM.UserNotes.Where(reminder => reminder is Reminder_VM);
			}
			else
			{
				return [];
			}
		}
		public IEnumerable<Note_VM>? GetUserNotesByTag(Tag_VM tag)
		{
			return user_VM?.UserNotes.Where(note => note.NoteTags.Contains(tag));
		}
		public IEnumerable<Tag_VM> GetUserTags()
		{
			if (user_VM is not null && user_VM.UserTags is not null)
			{
				return user_VM.UserTags;
			}
			else
			{
				return [];
			}
		}
		public void DeleteUserNote(int noteId)
		{
			if (user_VM is not null)
			{
				bool isDeleted = user_VM.DeleteNoteById(noteId);
				//TODO: Delete from user and save changes in db
				if (!isDeleted)
				{
					throw new Exception($"AuthenticatedUserHandler_VM.DeleteUserNote(noteId = {noteId}) failed");
				}
			}
		}
		public void AddNewNote(NoteContent content)
		{
			Note_VM? note;
			if (content.RemindDateTime is null)
			{
				note = new Note_VM()
				{
					Id = TestRepository.GetNewNoteId(),
					CreationDateTime = DateTime.Now,
					Header = content.NoteHeader,
					Body = content.NoteText
				};
			}
			else
			{
				note = new Reminder_VM()
				{
					Id = TestRepository.GetNewNoteId(),
					CreationDateTime = DateTime.Now,
					Header = content.NoteHeader,
					Body = content.NoteText,
					RemindTime = content.RemindDateTime ?? DateTime.MinValue
				};
			}
			user_VM?.UserNotes.Add(note);
		}
		public Tag_VM? AddNewTag(string tagName)
		{
			if (tagName.Length == 0) return null;
			var fixedTagName = "#" + tagName;
			if(fixedTagName.Length > 30)
			{
				fixedTagName = fixedTagName[..30];
			}
			var tag = new Tag { Id = TestRepository.GetNewTagId(), TagName = fixedTagName };
			user?.UserTags.Add(tag);
			var tag_vm = new Tag_VM(tag);
			user_VM?.UserTags.Add(tag_vm);
			return tag_vm;
		}
		public void DeleteUserTag(int tagId)
		{
			if (user_VM is not null)
			{
				user_VM.DeleteTagById(tagId);
				//TODO: Delete from user and save changes in db
			}
		}
		public IEnumerable<Tag_VM> GetUserTagsByTagName(string tagName)
		{
			var tags = GetUserTags();
			return tags.Where(tag => tag.TagName.Contains(tagName));
		}
		public Tag_VM? GetUserTagByName(string tagName)
		{
			var tags = GetUserTags();
			return tags?.FirstOrDefault(tag => tag.TagName.Equals(tagName));
		}
		public void NullifyUser()
		{
			user_VM = null;
			user = null;
		}
	}
}
