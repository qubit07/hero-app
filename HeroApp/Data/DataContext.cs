﻿using HeroApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeroApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

    }
}
