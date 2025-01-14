namespace Carzz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarSelling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarSellingModels",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        RCBookPath = c.String(nullable: false),
                        InsurancePath = c.String(nullable: false),
                        FrontPhotoPath = c.String(nullable: false),
                        RearPhotoPath = c.String(nullable: false),
                        LeftPhotoPath = c.String(nullable: false),
                        RightPhotoPath = c.String(nullable: false),
                        ServiceName = c.String(),
                        SubmittedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarSellingModels");
        }
    }
}
