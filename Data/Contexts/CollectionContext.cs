using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    public class CollectionContext : IdentityDbContext<User, UserRole, string>
    {
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {

        }

        public DbSet<Accrual> Accruals { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaskResult> TaskResults { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCommunication> ContactCommunications { get; set; }
        public DbSet<VehicleManufacturer> VehicleManufacturers { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}