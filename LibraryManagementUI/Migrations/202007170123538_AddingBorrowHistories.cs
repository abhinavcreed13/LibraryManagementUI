namespace LibraryManagementUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBorrowHistories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BorrowHistory",
                c => new
                    {
                        BorrowHistoryId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BorrowHistoryId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistory", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.BorrowHistory", "BookId", "dbo.Book");
            DropIndex("dbo.BorrowHistory", new[] { "CustomerId" });
            DropIndex("dbo.BorrowHistory", new[] { "BookId" });
            DropTable("dbo.BorrowHistory");
        }
    }
}
