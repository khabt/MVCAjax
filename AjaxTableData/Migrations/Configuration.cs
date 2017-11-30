namespace AjaxTableData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AjaxTableData.EmployeeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AjaxTableData.EmployeeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Employees.AddOrUpdate(
                new Employees { Name = "John Switch", Salary = 3500000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Michael Josn", Salary = 4000000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Tom Cruise", Salary = 4200000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "John Switch", Salary = 4500000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "John Witch", Salary = 5000000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Assassin Creed", Salary = 5200000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "The Witch", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Over Watch", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "John Vision", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Gangter", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Agent FIB", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Satosi Nagamoto", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Bit Remito", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Kim Con Son", Salary = 350000, CreateDate = DateTime.Now, Status = true },
                new Employees { Name = "Tom Hank", Salary = 350000, CreateDate = DateTime.Now, Status = true }
            );
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // Change column name to PersonId
        //    modelBuilder.Entity<Employees>()
        //        .Property(p => p.CreateDate)
        //        .HasColumnName("CreateDateEdate");

        //    // Change table name to People
        //    modelBuilder.Entity<Employees>()
        //        .ToTable("Emplyees");
        //}
    }
}
