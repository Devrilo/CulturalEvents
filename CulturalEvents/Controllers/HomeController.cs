using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturalEvents.Models;
using CulturalEventsData;
using CulturalEvents.Models.Events;

namespace CulturalEvents.Controllers
{
	public class HomeController : Controller
	{
		private IEvent _events;

		public HomeController(IEvent events)
		{
			_events = events;
		}

		public IActionResult Index()
		{
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
				PaidEvents = listingEventPaidResult
			};

			return View(model);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
