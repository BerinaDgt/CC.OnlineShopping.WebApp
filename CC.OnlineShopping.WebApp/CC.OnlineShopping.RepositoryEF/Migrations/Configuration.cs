namespace CC.OnlineShopping.RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CC.OnlineShopping.RepositoryEF.DatabaseOnlineShopping>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CC.OnlineShopping.RepositoryEF.DatabaseOnlineShopping";
        }

        protected override void Seed(CC.OnlineShopping.RepositoryEF.DatabaseOnlineShopping context)
        {
            
        }
    }
}
