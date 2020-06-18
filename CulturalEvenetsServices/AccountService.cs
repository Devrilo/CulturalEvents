using CulturalEvents.Helpers;
using CulturalEventsData;
using CulturalEventsData.Models;
using CulturalEventsServices.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CulturalEventsServices
{
	public class AccountService : IAccountService
	{
		private CulturalEventsContext _context;

		public AccountService(CulturalEventsContext context)
		{
			_context = context;
		}

		public Account Authenticate(string email, string password)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
				return null;

			var account = _context.Accounts.SingleOrDefault(x => x.Email == email);

			if (account == null)
				return null;

			if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
				return null;

			return account;
		}

		public IEnumerable<Account> GetAll()
		{
			return _context.Accounts;
		}

		public Account GetById(int id)
		{
			return _context.Accounts.Find(id);
		}

		public Account Create(Account account, string password)
		{
			///validation
			if (string.IsNullOrWhiteSpace(password))
				throw new AppException("Hasło jest wymagane");

			if (_context.Accounts.Any(x => x.Email == account.Email))
				throw new AppException("Ta nazwa użytkownika jest zajęta");

			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			account.PasswordHash = passwordHash;
			account.PasswordSalt = passwordSalt;

			_context.Accounts.Add(account);
			_context.SaveChanges();

			return account;
		}

		public void Update(Account accountParam, string password = null)
		{
			var account = _context.Accounts.Find(accountParam.Id);

			if (account == null)
				throw new AppException("Nie istnieje taki użytkownik!");

			if (accountParam.Email != account.Email)
			{
				// login has changed so check if the login is already taken
				if (_context.Accounts.Any(x => x.Email == accountParam.Email))
					throw new AppException("Email " + accountParam.Email + " jest już zajęty");
			}

			// update account properties
			account.Name = accountParam.Name;
			account.Surname = accountParam.Surname;
			account.Email = accountParam.Email;
			account.Telephone = accountParam.Telephone;

			// update password if it was entered

			_context.Accounts.Update(account);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var account = _context.Accounts.Find(id);
			if (account != null)
			{
				_context.Accounts.Remove(account);
				_context.SaveChanges();
			}
		}

		// private helper methods

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != storedHash[i]) return false;
				}
			}
			return true;
		}
	}
}
