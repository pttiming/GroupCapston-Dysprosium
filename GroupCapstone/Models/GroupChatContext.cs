using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstone.Models
{
    public class GroupChatContext : DbContext
    {
        public GroupChatContext(DbContextOptions<GroupChatContext> options)
        : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
    }
}
