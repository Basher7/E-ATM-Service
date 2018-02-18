namespace EatmWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Finally : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EatmAccountsId = c.Int(nullable: false),
                        Counter = c.Int(nullable: false),
                        EatmAccounts_CardNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EatmAccounts", t => t.EatmAccounts_CardNumber)
                .Index(t => t.EatmAccounts_CardNumber);
            
            CreateTable(
                "dbo.EatmAccounts",
                c => new
                    {
                        CardNumber = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        TransactionCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counts", "EatmAccounts_CardNumber", "dbo.EatmAccounts");
            DropIndex("dbo.Counts", new[] { "EatmAccounts_CardNumber" });
            DropTable("dbo.EatmAccounts");
            DropTable("dbo.Counts");
        }
    }
}
