using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    public class ULFContext : DbContext
    {
        public ULFContext(DbContextOptions<ULFContext> options) : base(options)
        {

        }

        public DbSet<Accrual> Accruals { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaskResultsGroup> TaskResultsGroups { get; set; }
        public DbSet<TaskResultsItem> TaskResultsItems { get; set; }
    }
}