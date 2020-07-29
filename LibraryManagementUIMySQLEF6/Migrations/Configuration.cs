namespace LibraryManagementUIMySQLEF6.Migrations
{
    using LibraryManagementUIMySQLEF6.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryManagementUIMySQLEF6.DAL.LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryManagementUIMySQLEF6.DAL.LibraryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //LibraryDataInitilizer.Seed(context);
        }
    }
}
