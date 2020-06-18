using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CulturalEventsData.Models;
using CulturalEventsData;
using CulturalEventsServices;
using CulturalEvents.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using CulturalEventsData.Dtos;
using CulturalEventsServices.Helpers;
using AutoMapper;
using Microsoft.Extensions.Options;
using CulturalEvents.Models.Register;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CulturalEvents.Controllers
{

	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class LoginController : Controller
	{

		private IAccountService _accountService;
		private IMapper _mapper;
		private readonly AppSettings _appSettings;

		public LoginController(
			IAccountService accountService,
			IMapper mapper,
			IOptions<AppSettings> appSettings)
		{
			_accountService = accountService;
			_mapper = mapper;
			_appSettings = appSettings.Value;
		}


		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}


		[AllowAnonymous]
		[HttpPost("authenticate")]

		public IActionResult Authenticate([FromForm] IFormCollection form, string returnUrl)
		{
			var email = form["email"];
			var password = form["password"];

			AccountDto accountDto = new AccountDto
			{
				Email = email,
				Password = password
			};

			var account = _accountService.Authenticate(accountDto.Email, accountDto.Password);

			if (account == null)
				return BadRequest(new { message = "Email lub hasło jest nieprawidłowe" });

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, account.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			Cookie userCookie = new Cookie("user", account.Id.ToString());
			Cookie tokenCookie = new Cookie("token", tokenString);
			Cookie adminCookie = new Cookie("admin", "0");

			userCookie.Expires.AddDays(1);

			if (account.Id == 1004)
			{
				HttpContext.Response.Cookies.Append("admin", "1");
			}
			else
			{
				HttpContext.Response.Cookies.Append("admin", "0");
			}

			HttpContext.Response.Cookies.Append("user", account.Id.ToString());
			HttpContext.Response.Cookies.Append("token", tokenString);


			// ret basic user info (withour password) and token to store client side
			return Redirect("/Home/Index/");
		}


		[AllowAnonymous]
		[HttpPost("Register")]
		public IActionResult Register([FromForm] IFormCollection form) //string name, string surname, string email,  string telephone, string password)
		{
			var name = form["name"];
			var surname = form["surname"];
			var email = form["email"];
			var telephone = form["telephone"];
			var password = form["password"];
			AccountDto accountDto = new AccountDto
			{
				Name = name,
				Surname = surname,
				Email = email,
				Telephone = telephone,
				Password = password
			};

			Account account = new Account
			{
				Name = accountDto.Name,
				Surname = accountDto.Surname,
				Email = accountDto.Email,
				Telephone = accountDto.Telephone,
			};
			//map dto to entity
			//var account = _mapper.Map<Account>(accountDto);

			try
			{
				//save
				_accountService.Create(account, accountDto.Password);
				return Redirect("/Home/Index/");
			}
			catch (AppException ex)
			{
				//return error if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		/*
		public IActionResult GetAll()
		{
			var accounts = _accountService.GetAll();
			var accountDtos = _mapper.Map<IList<AccountDto>>(accounts);
			return Ok(accounts);
		}
		*/


		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var account = _accountService.GetById(id);
			var accountDto = _mapper.Map<AccountDto>(account);
			return Ok(accountDto);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, [FromBody]AccountDto accountDto)
		{
			var account = _mapper.Map<Account>(accountDto);
			account.Id = id;

			try
			{
				_accountService.Update(account, accountDto.Password);
				return Ok();
			}
			catch (AppException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_accountService.Delete(id);
			return Ok();
		}

	}


}