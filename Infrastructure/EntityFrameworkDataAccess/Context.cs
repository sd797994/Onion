using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Infrastructure.PersistentObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkDataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
    }
}
