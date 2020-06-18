using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData
{
	public interface IEvent
	{
		IEnumerable<Event> GetAll();
		IEnumerable<Event> GetAllIndex();
		Event GetById(int id);
		void Add(Event newEvent);
		string GeTitle(int id);
		string GetCity(int id);
		byte[] GetPicture(int id);
		int GetCreatorId(int id);
		string GetStreet(int id);
		int GetHouseNumber(int id);
		int GetApartmentNumber(int id);
		string GetPostalCode(int id);
		DateTime GetDateStart(int id);
		DateTime GetDateEnd(int id);
		string GetDescription(int id);
		int GetAccountsCount(int id);
		Event Create(Event culturalEvent, StaticCategory staticCategory);
		Event Edit(Event culturalEvent);
		Event Delete(Event culturalEvent);
		EventAccount GetEventAccountToDelete(int userId, int eventId);
		EventAccount Resign(int userId, int eventId);
		EventAccount Attend(EventAccount eventAccount);
		IEnumerable<PaidEvent> GetAllPaidEvents();

		IEnumerable<Event> GetAllCreated(int userId);
		IEnumerable<Event> GetAllAttend(int userId);




		IEnumerable<Event> GetAllFromDateOrderByAttendants(DateTime dateStart);
		IEnumerable<Event> GetAllFromDateOrderByDate(DateTime dateStart);
		IEnumerable<Event> GetAllSearchByTitleFromDateOrderByAttendants(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByDescriptionFromDateOrderByAttendants(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByCityFromDateOrderByAttendants(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByCategoryFromDateOrderByAttendants(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByTitleFromDateOrderByDate(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByDescriptionFromDateOrderByDate(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByCityFromDateOrderByDate(DateTime dateStart, string fraza);
		IEnumerable<Event> GetAllSearchByCategoryFromDateOrderByDate(DateTime dateStart, string fraza);
		//int GetNumberOfParticipants(int id);
	}
}
