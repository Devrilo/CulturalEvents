using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData.Models
{
	public class Account
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Telephone { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

		public virtual ICollection<EventAccount> EventsAccounts { get; set; }
	}
}
