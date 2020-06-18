using CulturalEventsData;
using CulturalEventsData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CulturalEventsServices
{
	public class EventService : IEvent
	{
		private CulturalEventsContext _context;

		public EventService(CulturalEventsContext context)
		{
			_context = context;
		}

		public void Add(Event newEvent)
		{
			_context.Add(newEvent);
			_context.SaveChanges();
		}

		public IEnumerable<Event> GetAll()
		{
			var x = _context.Events
			   .Include(e => e.StaticCategory)
			   .Include(e => e.EventsAccounts);
			return x;
		}

		public IEnumerable<Event> GetAllIndex()
		{
			var x = GetAll()
				.Where(e => e.DateStart >= DateTime.Now)
				.OrderByDescending(e => GetAccountsCount(e.Id));
			return x;
		}

		public IEnumerable<Event> GetAllCreated(int userId)
		{
			var x = GetAll()
				.Where(e => e.CreatorId == userId);
			return x;
		}

		public IEnumerable<Event> GetAllAttend(int userId)
		{
			var x = _context.EventsAccounts
				.Where(ea => ea.AccountId == userId)
				.Select(e => e.Event)
				.Include(e => e.StaticCategory)
			    .Include(e => e.EventsAccounts);
			return x;
			
		}

		public int GetAccountsCount(int id)
		{
			return GetById(id).EventsAccounts.Count;
		}



		public IEnumerable<Event> GetAllFromDateOrderByAttendants(DateTime dateStart)
		{
			var x = GetAll()
				.Where(e => e.DateStart >= dateStart)
				.OrderByDescending(e => GetAccountsCount(e.Id))
				.ThenBy(e => e.DateStart);
			return x;
		}



		public IEnumerable<Event> GetAllFromDateOrderByDate(DateTime dateStart)
		{
			var x = GetAll()
				.Where(e => e.DateStart >= dateStart)
				.OrderBy(e => e.DateStart)
				.ThenByDescending(e => GetAccountsCount(e.Id));
			return x;
		}



		public IEnumerable<Event> GetAllSearchByTitleFromDateOrderByAttendants(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.Title.Contains(fraza) && e.DateStart >= dateStart)
				.OrderByDescending(e => GetAccountsCount(e.Id))
				.ThenBy(e => e.DateStart); 
			return x;
		}

		public IEnumerable<Event> GetAllSearchByDescriptionFromDateOrderByAttendants(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.Description.Contains(fraza) && e.DateStart >= dateStart)
				.OrderByDescending(e => GetAccountsCount(e.Id))
				.ThenBy(e => e.DateStart);
			return x;
		}

		public IEnumerable<Event> GetAllSearchByCityFromDateOrderByAttendants(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.City.Contains(fraza) && e.DateStart >= dateStart)
				.OrderByDescending(e => GetAccountsCount(e.Id))
				.ThenBy(e => e.DateStart);
			return x;
		}

		public IEnumerable<Event> GetAllSearchByCategoryFromDateOrderByAttendants(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.StaticCategory.Name.Contains(fraza) && e.DateStart >= dateStart)
				.OrderByDescending(e => GetAccountsCount(e.Id))
				.ThenBy(e => e.DateStart);
			return x;
		}



		public IEnumerable<Event> GetAllSearchByTitleFromDateOrderByDate(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.Title.Contains(fraza) && e.DateStart >= dateStart)
				.OrderBy(e => e.DateStart)
				.ThenByDescending(e => GetAccountsCount(e.Id));
			return x;
		}

		public IEnumerable<Event> GetAllSearchByDescriptionFromDateOrderByDate(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.Description.Contains(fraza) && e.DateStart >= dateStart)
				.OrderBy(e => e.DateStart)
				.ThenByDescending(e => GetAccountsCount(e.Id));
			return x;
		}

		public IEnumerable<Event> GetAllSearchByCityFromDateOrderByDate(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.City.Contains(fraza) && e.DateStart >= dateStart)
				.OrderBy(e => e.DateStart)
				.ThenByDescending(e => GetAccountsCount(e.Id));
			return x;
		}

		public IEnumerable<Event> GetAllSearchByCategoryFromDateOrderByDate(DateTime dateStart, string fraza)
		{
			var x = GetAll()
				.Where(e => e.StaticCategory.Name.Contains(fraza) && e.DateStart >= dateStart)
				.OrderBy(e => e.DateStart)
				.ThenByDescending(e => GetAccountsCount(e.Id));
			return x;
		}



		public IEnumerable<Event> GetAllSortedSearch(string sortArg, string searchArg)
		{
			var x = _context.Events
			   .Include(e => e.StaticCategory)
			   .Include(e => e.EventsAccounts);
			return x;
		}




		public Event GetById(int id)
		{
			return GetAll()
				.FirstOrDefault(e => e.Id == id);
		}

		public Event Create(Event culturalEvent, StaticCategory staticCategory)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories ON;");
				transaction.Commit();
			}

			using (var transaction = _context.Database.BeginTransaction())
			{
				var list = _context.Categories.FromSql("SELECT * FROM dbo.Categories").ToList();
				transaction.Commit();
			}
			_context.Events.Add(culturalEvent);
			_context.SaveChanges();

			using (var transaction = _context.Database.BeginTransaction())
			{
				_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories OFF;");
				transaction.Commit();
			}
			return culturalEvent;
		}

		public Event Edit(Event culturalEvent)
		{
			var toEdit = _context.Events.FirstOrDefault(e => e.Id == culturalEvent.Id);
			toEdit.Title = culturalEvent.Title;
			toEdit.DateStart = culturalEvent.DateStart;
			toEdit.DateEnd = culturalEvent.DateEnd;
			toEdit.City = culturalEvent.City;
			toEdit.Street = culturalEvent.Street;
			toEdit.HouseNumber = culturalEvent.HouseNumber;
			toEdit.ApartmentNumber = culturalEvent.ApartmentNumber;
			toEdit.PostalCode = culturalEvent.PostalCode;
			toEdit.Description = culturalEvent.Description;
			toEdit.StaticCategory = culturalEvent.StaticCategory;
			if (culturalEvent.Picture != null)
			{
				toEdit.Picture = culturalEvent.Picture;
			}

			_context.Events.Update(toEdit);
			_context.SaveChanges();
			return culturalEvent;
		}

		/*
		public IEnumerable<StaticCategory> GetAllStaticCategories()
		{
			return (from c in _context.Categories select 'Name'
			var x = _context.Categories;
			return x;
				
		}
		*/

		public Event Delete(Event culturalEvent)
		{
			var toDelete = _context.Events.FirstOrDefault(e => e.Id == culturalEvent.Id);
			_context.Events.Remove(toDelete);
			_context.SaveChanges();
			return culturalEvent;
		}

		public byte[] GetPicture(int id)
		{
			return _context.Events
				.FirstOrDefault(e => e.Id == id).Picture;
		}

		public int GetCreatorId(int id)
		{
			return _context.Events
				.FirstOrDefault(e => e.Id == id).CreatorId;
		}

		public EventAccount Attend(EventAccount eventAccount)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories ON;");
				transaction.Commit();
			}

			using (var transaction = _context.Database.BeginTransaction())
			{
				var list = _context.Categories.FromSql("SELECT * FROM dbo.Categories").ToList();
				transaction.Commit();
			}
			_context.EventsAccounts.Add(eventAccount);
			_context.SaveChanges();

			using (var transaction = _context.Database.BeginTransaction())
			{
				_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Categories OFF;");
				transaction.Commit();
			}
			return eventAccount;
		}

		public EventAccount GetEventAccountToDelete(int userId, int eventId)
		{
			return _context.EventsAccounts
			.FirstOrDefault(ea => ea.AccountId == userId && ea.EventId == eventId);
		}



		public EventAccount Resign(int userId, int eventId)
		{
			var toDelete = GetEventAccountToDelete(userId, eventId);
			_context.EventsAccounts.Remove(toDelete);
			_context.SaveChanges();
			return toDelete;
		}

		public IEnumerable<PaidEvent> GetAllPaidEvents()
		{
			var x = _context.PaidEvents;
			return x;
		}
		/*
		public Byte[] ImgToByte()
		{
			return Ok
		}
		*/
		/*
		public IEnumerable<Event> GetCreatedByUser(int id, int userId)
		{

			var x = _context.EventsAccounts
				.Where(ea => ea.IsCreator == true && ea.AccountId == userId)
				.SelectMany(ea => _context.Events);
				*/
		//.Select(ea => ea.Event);

		/*
		var x = _context.Accounts
			.Where()
			.Where(a => a.Id == userId).SelectMany(a => _context.Events);
			//.Where(b => b.IsCreator);


		return x;
		/*
		var x = _context.Events
			.Include(e => e.StaticCategory)
			.Include(e => e.EventsAccounts)
			.Where(e => e.Id.AccountId == userId);
		return x;

	}
	*/

		public string GetCity(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).City;
			}

			public string GetStreet(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).Street;
			}

			public int GetHouseNumber(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).HouseNumber;
			}

			public int GetApartmentNumber(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).ApartmentNumber;
			}

			public string GetPostalCode(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).PostalCode;
			}

			public DateTime GetDateEnd(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).DateEnd;
			}

			public DateTime GetDateStart(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).DateStart;
			}

			public string GetDescription(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).Description;
			}

			public string GeTitle(int id)
			{
				return _context.Events
					.FirstOrDefault(e => e.Id == id).Title;
			}
	}
}
