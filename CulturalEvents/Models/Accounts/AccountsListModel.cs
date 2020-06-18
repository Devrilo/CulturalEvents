using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Models.Accounts
{
	public class AccountsListModel
	{
		public IEnumerable<AccountsListIndexModel> Accounts { get; set; }
	}
}
