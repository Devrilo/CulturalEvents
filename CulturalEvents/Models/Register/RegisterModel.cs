
using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Models.Register
{
	public class RegisterModel
	{
		
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Telephone { get; set; }
		public string Password { get; set; }

	}
}
