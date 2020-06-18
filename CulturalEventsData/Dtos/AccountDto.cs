using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData.Dtos
{
	public class AccountDto
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Telephone { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }

		//public virtual ICollection<EventAccount> EventsAccounts { get; set; }
	}
}
