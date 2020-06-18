using CulturalEventsData.Dtos;
using CulturalEventsData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData
{
	public class CulturalEventsContext : DbContext
	{
		public CulturalEventsContext(DbContextOptions options) : base(options) { }

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<EventAccount> EventsAccounts { get; set; }
		public DbSet<PaidEvent> PaidEvents { get; set; }

		public DbSet<StaticContact> Contacts { get; set; }
		public DbSet<StaticCategory> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StaticCategory>()
				.HasMany(sc => sc.Events)
				.WithOne(e => e.StaticCategory);

			modelBuilder.Entity<EventAccount>()
			   .HasKey(ea => new { ea.AccountId, ea.EventId });
			modelBuilder.Entity<EventAccount>()
				.HasOne(ea => ea.Account)
				.WithMany(ea => ea.EventsAccounts)
				.HasForeignKey(ea => ea.AccountId);
			modelBuilder.Entity<EventAccount>()
				.HasOne(ea => ea.Event)
				.WithMany(ea => ea.EventsAccounts)
				.HasForeignKey(ea => ea.EventId);
		}
	}
}
