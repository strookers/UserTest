using System.Data.Entity;

namespace UserTest.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EntityContext : DbContext
    {

        public EntityContext() : base("name=EntityContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Person> Persons { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.Email).HasColumnType("VARCHAR").HasMaxLength(450);

        }
    }
}