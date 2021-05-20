namespace WebPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfirstandlastnaem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GivenName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "GivenName");
        }
    }
}
