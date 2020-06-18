using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData
{
	public interface IAccountService
	{
		Account Authenticate(string email, string password);
		IEnumerable<Account> GetAll();
		Account GetById(int id);
		Account Create(Account account, string password);
		void Update(Account account, string password = null);
		void Delete(int id);
	}
}
