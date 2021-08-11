namespace eProject_SymphonyLimited.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eProject_SymphonyLimited.Models.SymphonyLimitedDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "eProject_SymphonyLimited.Models.SymphonyLimitedDBContext";
        }

        protected override void Seed(eProject_SymphonyLimited.Models.SymphonyLimitedDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
