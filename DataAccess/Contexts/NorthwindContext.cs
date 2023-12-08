﻿using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class NorthwindContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Product> Products { get; set; }

        public NorthwindContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            //jsondan veritabanı konfigürasyonu için
            Configuration = configuration;
            //veritabanı migration
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

