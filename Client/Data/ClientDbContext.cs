﻿using Client.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Client.Data
{
    class ClientDbContext: DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Status> Status { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreDemoClient2;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
