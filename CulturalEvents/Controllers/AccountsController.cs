using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulturalEvents.Models.Accounts;
using CulturalEventsData;
using CulturalEventsData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulturalEvents.Controllers
{
	public class AccountsController : Controller
	{
		private IAccountService _accountService;

		public AccountsController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public IActionResult Index()
		{
			var accountModels = _accountService.GetAll();

			var listingAccountsResult = accountModels
				.Select(result => new AccountsListIndexModel
				{
					Id = result.Id,
					Email = result.Email,
					Name = result.Name,
					Surname = result.Surname,
					Telephone = result.Telephone
				});

			var model = new AccountsListModel()
			{
				Accounts = listingAccountsResult
			};

			return View(model);
		}

		public IActionResult Edit(int id)
		{
			var accountModels = _accountService.GetById(id);

			var model = new AccountsEditModel
			{
				Id = id,
				Email = accountModels.Email,
				Name = accountModels.Name,
				Surname = accountModels.Surname,
				Telephone = accountModels.Telephone
			};

			return View(model);
		}

		[HttpPost("Update")]
		public IActionResult Update([FromForm] IFormCollection form)//string name, string surname, string email,  string telephone, string password)
		{
			var id = int.Parse(form["id"]);
			var name = form["name"];
			var surname = form["surname"];
			var email = form["email"];
			var telephone = form["telephone"];

			Account account = new Account
			{
				Id = id,
				Name = name,
				Surname = surname,
				Email = email,
				Telephone = telephone
			};

			try
			{
				//save
				_accountService.Update(account);
				return Redirect("/Accounts/Edit/" + id + "/");
			}
			catch (Exception ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("Logout")]
		public IActionResult Logout([FromForm] IFormCollection form)
		{
			Response.Cookies.Delete("user");
			Response.Cookies.Delete("token");
			Response.Cookies.Delete("admin");
			return Redirect("/Home/Index/");
		}
	}
}