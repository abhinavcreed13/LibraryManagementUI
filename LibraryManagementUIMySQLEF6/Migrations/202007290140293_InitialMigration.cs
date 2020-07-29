namespace LibraryManagementUIMySQLEF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, unicode: false),
                        SerialNumber = c.String(nullable: false, unicode: false),
                        Author = c.String(unicode: false),
                        Publisher = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Address = c.String(nullable: false, unicode: false),
                        Contact = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
            DropTable("dbo.Books");
        }
    }
}
