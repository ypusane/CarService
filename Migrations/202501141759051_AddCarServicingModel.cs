namespace Carzz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarServicingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarServicingModels",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceCategory = c.Int(nullable: false),
                        AppointmentTime = c.DateTime(nullable: false),
                        BookingTime = c.DateTime(nullable: false),
                        ProblemDescription = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarServicingModels");
        }
    }
}
