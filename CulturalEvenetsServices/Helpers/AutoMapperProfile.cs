using AutoMapper;
using CulturalEventsData.Dtos;
using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsServices.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AccountService, AccountDto>();
			CreateMap<AccountDto, Account>();
		}
	}
}
