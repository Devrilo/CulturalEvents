using CulturalEvents.Models.Events;
using CulturalEventsData;
using CulturalEventsData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Controllers
{
	//[Authorize]
	public class EventsController : Controller
	{
		private IEvent _events;

		public EventsController(IEvent events)
		{
			_events = events;
		}


		public IActionResult Index()
		{
			var eventModels = _events.GetAllIndex();

			var listingEventsResult = eventModels
				.Select(result => new EventsIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					DateStart = result.DateStart,
					DateEnd = result.DateEnd,
					City = result.City,
					Street = result.Street,
					HouseNumber = result.HouseNumber,
					ApartmentNumber = result.ApartmentNumber,
					PostalCode = result.PostalCode,
					Picture = result.Picture,
					StaticCategory = result.StaticCategory,
					GetAccountsCount = _events.GetAccountsCount(result.Id)
				});


			var eventPaidModels = _events.GetAllPaidEvents();

			var listingEventPaidResult = eventPaidModels
				.Select(result => new EventsPaidIndexListingModel
				{
					Id = result.Id,
					TopText = result.TopText,
					BottomText = result.BottomText,
					PaidPicture = result.Picture
				});


			var model = new EventsIndexModel()
			{
				Events = listingEventsResult,
				PaidEvents = listingEventPaidResult
			};


			return View(model);
		}

		public IActionResult AddPromo()
		{
			return View();
		}

			public IActionResult Created()
		{
			var eventModels = _events.GetAllCreated(int.Parse(Request.Cookies["user"]));

			var listingEventsResult = eventModels
				.Select(result => new EventsIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					DateStart = result.DateStart,
					DateEnd = result.DateEnd,
					City = result.City,
					Street = result.Street,
					HouseNumber = result.HouseNumber,
					ApartmentNumber = result.ApartmentNumber,
					PostalCode = result.PostalCode,
					Picture = result.Picture,
					StaticCategory = result.StaticCategory,
					GetAccountsCount = _events.GetAccountsCount(result.Id)
				});


			var model = new EventsIndexModel()
			{
				Events = listingEventsResult
			};


			return View(model);
		}

		public IActionResult TakingPart()
		{
			var eventModels = _events.GetAllAttend(int.Parse(Request.Cookies["user"]));

			var listingEventsResult = eventModels
				.Select(result => new EventsIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					DateStart = result.DateStart,
					DateEnd = result.DateEnd,
					City = result.City,
					Street = result.Street,
					HouseNumber = result.HouseNumber,
					ApartmentNumber = result.ApartmentNumber,
					PostalCode = result.PostalCode,
					Picture = result.Picture,
					StaticCategory = result.StaticCategory,
					GetAccountsCount = _events.GetAccountsCount(result.Id)
				});


			var model = new EventsIndexModel()
			{
				Events = listingEventsResult
			};


			return View(model);
		}

		[HttpPost]
		public IActionResult Search([FromForm] IFormCollection form, [FromForm] IFormFile picFile)//string name, string surname, string email,  string telephone, string password)
		{
			string searchText = form["searchText"];
			string searchBy = form["searchBy"];
			string orderBy = form["orderBy"];
			DateTime fromDate = DateTime.Parse(form["fromDate"]);

			var eventModels = _events.GetAll();

			if (searchText == "")
			{
				if (orderBy == "Attendants")
					eventModels = _events.GetAllFromDateOrderByAttendants(fromDate);
				else
					eventModels = _events.GetAllFromDateOrderByDate(fromDate);
			}
			else if (searchBy == "Title")
			{
				if (orderBy == "Attendants")
					eventModels = _events.GetAllSearchByTitleFromDateOrderByAttendants(fromDate, searchText);
				else
					eventModels = _events.GetAllSearchByTitleFromDateOrderByDate(fromDate, searchText);
			}
			else if (searchBy == "Description")
			{
				if (orderBy == "Attendants")
					eventModels = _events.GetAllSearchByDescriptionFromDateOrderByAttendants(fromDate, searchText);
				else
					eventModels = _events.GetAllSearchByDescriptionFromDateOrderByDate(fromDate, searchText);
			}
			else if (searchBy == "City")
			{
				if (orderBy == "Attendants")
					eventModels = _events.GetAllSearchByCityFromDateOrderByAttendants(fromDate, searchText);
				else
					eventModels = _events.GetAllSearchByCityFromDateOrderByDate(fromDate, searchText);
			}
			else if (searchBy == "Category")
			{
				if (orderBy == "Attendants")
					eventModels = _events.GetAllSearchByCategoryFromDateOrderByAttendants(fromDate, searchText);
				else
					eventModels = _events.GetAllSearchByCategoryFromDateOrderByDate(fromDate, searchText);
			}

			var listingEventsResult = eventModels
				.Select(result => new EventsIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					DateStart = result.DateStart,
					DateEnd = result.DateEnd,
					City = result.City,
					Street = result.Street,
					HouseNumber = result.HouseNumber,
					ApartmentNumber = result.ApartmentNumber,
					PostalCode = result.PostalCode,
					Picture = result.Picture,
					StaticCategory = result.StaticCategory,
					GetAccountsCount = _events.GetAccountsCount(result.Id)
				});

			var model = new EventsIndexModel()
			{
				Events = listingEventsResult
			};

			return View(model);
		}

			/*
			public IActionResult ()
			{
				var eventModels = _events.GetAll();

				var listingResult = eventModels
					.Select(result => new EventsIndexListingModel
					{
						Id = result.Id,
						Title = result.Title,
						DateStart = result.DateStart,
						DateEnd = result.DateEnd,
						City = result.City,
						Street = result.Street,
						HouseNumber = result.HouseNumber,
						ApartmentNumber = result.ApartmentNumber,
						PostalCode = result.PostalCode,
						Picture = result.Picture,
						StaticCategory = result.StaticCategory,
						GetAccountsCount = _events.GetAccountsCount(result.Id)
					});

				var model = new EventsIndexModel()
				{
					Events = listingResult
				};

				return View(model);
			}
			*/

		public IActionResult Detail(int id)
		{
			var culturalEvent = _events.GetById(id);

			bool userAttends = false;

			if (Request.Cookies["user"] != null)
			{
				if (_events.GetEventAccountToDelete(int.Parse(Request.Cookies["user"]), id) == null)
					userAttends = false;
				else
					userAttends = true;
			}

			var model = new EventsDetailModel
			{
				Id = id,
				Title = culturalEvent.Title,
				DateStart = culturalEvent.DateStart,
				DateEnd = culturalEvent.DateEnd,
				City = culturalEvent.City,
				Street = culturalEvent.Street,
				HouseNumber = culturalEvent.HouseNumber,
				ApartmentNumber = culturalEvent.ApartmentNumber,
				PostalCode = culturalEvent.PostalCode,
				Picture = culturalEvent.Picture,
				Description = culturalEvent.Description,
				UserAttends = userAttends,
				StaticCategory = culturalEvent.StaticCategory,
				GetAccountsCount = _events.GetAccountsCount(culturalEvent.Id),
				CreatorId = culturalEvent.CreatorId
			};

			return View(model);
		}

		public IActionResult Add()
		{
			return View();
		}

		public IActionResult Edit(int id)
		{
			var culturalEvent = _events.GetById(id);

			var model = new EventsEditModel
			{
				Id = id,
				Title = culturalEvent.Title,
				DateStart = culturalEvent.DateStart,
				DateEnd = culturalEvent.DateEnd,
				City = culturalEvent.City,
				Street = culturalEvent.Street,
				HouseNumber = culturalEvent.HouseNumber,
				ApartmentNumber = culturalEvent.ApartmentNumber,
				PostalCode = culturalEvent.PostalCode,
				Picture = culturalEvent.Picture,
				Description = culturalEvent.Description,
				StaticCategory = culturalEvent.StaticCategory,
				GetAccountsCount = _events.GetAccountsCount(culturalEvent.Id),
				CreatorId = culturalEvent.CreatorId
			};

			return View(model);
		}

		[HttpPost("Change")]
		public IActionResult Change([FromForm] IFormCollection form, [FromForm] IFormFile picFile)//string name, string surname, string email,  string telephone, string password)
		{

			//int creatorId = int.Parse(Request.Cookies["user"]);
			int id = int.Parse(form["id"]);
			var title = form["title"];
			DateTime dateStart = DateTime.Parse(form["dateStart"]);
			DateTime dateEnd = DateTime.Parse(form["dateEnd"]);
			var city = form["city"];
			var street = form["street"];
			var houseNumber = int.Parse(form["houseNumber"]);
			var apartmentNumber = int.Parse(form["apartmentNumber"]);
			var postalCode = form["postalCode"];
			var description = form["description"];

			byte[] picture = null;


			if (picFile is null)
			{
				Event culturalEvent = new Event
				{
					Id = id,
					Title = title,
					DateStart = dateStart,
					DateEnd = dateEnd,
					City = city,
					Street = street,
					HouseNumber = houseNumber,
					ApartmentNumber = apartmentNumber,
					PostalCode = postalCode,
					Description = description,
					Picture = _events.GetPicture(id),
					CreatorId = _events.GetCreatorId(id)
				};

				try
				{
					//save
					_events.Edit(culturalEvent);
					return Redirect("/Events/Created");
				}
				catch (Exception ex)
				{
					//return error if there was an exception
					return BadRequest(new { message = ex.Message });
				}
			}
			else
			{
				BinaryReader reader = new BinaryReader(picFile.OpenReadStream());
				picture = reader.ReadBytes((int)picFile.Length);

				Event culturalEvent = new Event
				{
					Id = id,
					Title = title,
					DateStart = dateStart,
					DateEnd = dateEnd,
					City = city,
					Street = street,
					HouseNumber = houseNumber,
					ApartmentNumber = apartmentNumber,
					PostalCode = postalCode,
					Description = description,
					Picture = picture,
					CreatorId = _events.GetCreatorId(id)
				};

				try
				{
					//save
					_events.Edit(culturalEvent);
					return Redirect("Events/Created");
				}
				catch (Exception ex)
				{
					//return error if there was an exception
					return BadRequest(new { message = ex.Message });
				}
			}
		}

		[HttpPost("New")]
		public IActionResult New([FromForm] IFormCollection form, [FromForm] IFormFile picFile) //string name, string surname, string email,  string telephone, string password)
		{
			int creatorId = int.Parse(Request.Cookies["user"]);
			var title = form["title"];
			DateTime dateStart = DateTime.Parse(form["dateStart"]);
			DateTime dateEnd = DateTime.Parse(form["dateEnd"]);
			var city = form["city"];
			var street = form["street"];
			var houseNumber = int.Parse(form["houseNumber"]);
			var apartmentNumber = int.Parse(form["apartmentNumber"]);
			var postalCode = form["postalCode"];
			var description = form["description"];

			byte[] picture = null;
			BinaryReader reader = new BinaryReader(picFile.OpenReadStream());
			picture = reader.ReadBytes((int)picFile.Length);

			int staticCategoryId = int.Parse(Request.Form["category"]);

			StaticCategory staticCategory = new StaticCategory();
			staticCategory.Id = staticCategoryId;

			Event culturalEvent = new Event
			{
				Title = title,
				DateStart = dateStart,
				DateEnd = dateEnd,
				City = city,
				Street = street,
				HouseNumber = houseNumber,
				ApartmentNumber = apartmentNumber,
				PostalCode = postalCode,
				Description = description,
				Picture = picture,
				StaticCategory = staticCategory,
				CreatorId = creatorId
			};

			try
			{
				//save
				_events.Create(culturalEvent, staticCategory);
				return Redirect("/Events/Created");
			}
			catch (Exception ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
			/*
			  			using (var transaction = _context.Database.BeginTransaction())
			{
				
				var list = _context.Categories.FromSql("SELECT * FROM dbo.Categories").ToList();
				transaction.Commit();
			}
			*/
		}

		[HttpPost("Delete")]
		public IActionResult Delete([FromForm] IFormCollection form, [FromForm] IFormFile picFile) //string name, string surname, string email,  string telephone, string password)
		{
			int id = int.Parse(form["id"]);
			Event culturalEvent = new Event
			{
				Id = id
			};

			try
			{
				//save
				_events.Delete(culturalEvent);
				return Redirect("/Events/Created");
			}
			catch (Exception ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("Attend")]
		public IActionResult Attend([FromForm] IFormCollection form, [FromForm] IFormFile picFile) //string name, string surname, string email,  string telephone, string password)
		{
			int eventId = int.Parse(form["id"]);
			EventAccount eventAccount = new EventAccount
			{
				EventId = eventId,
				AccountId = int.Parse(Request.Cookies["user"])
			};

			try
			{
				//save
				_events.Attend(eventAccount);
				return Redirect("/Events/Detail/"+eventId+"/");
			}
			catch (Exception ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("Resign")]
		public IActionResult Resign([FromForm] IFormCollection form, [FromForm] IFormFile picFile) //string name, string surname, string email,  string telephone, string password)
		{

			int eventId = int.Parse(form["id"]);
			/*
			EventAccount eventAccount = new EventAccount
			{
				EventId = eventId,
				AccountId = int.Parse(Request.Cookies["user"])
			};
			*/

			try
			{
				//save
				_events.Resign(int.Parse(Request.Cookies["user"]), eventId);
				return Redirect("/Events/Detail/" + eventId + "/");
			}
			catch (Exception ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

	}
}
