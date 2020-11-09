using System;
using System.Collections.Generic;
using System.Text;
using GroupCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Participant> Participants { get; set; } 

        public DbSet<Event> Events { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<Participant> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Participant",
                NormalizedName = "PARTICIPANT"
            }
            );
        }
        //public DbSet<GroupCapstone.Models.UserGroup> UserGroup { get; set; }
    }
}
