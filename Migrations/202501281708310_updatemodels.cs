namespace Carzz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarSellingModels", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.CarServicingModels", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CarSellingModels", "UserId");
            CreateIndex("dbo.CarServicingModels", "UserId");
            AddForeignKey("dbo.CarSellingModels", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CarServicingModels", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarServicingModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CarSellingModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CarServicingModels", new[] { "UserId" });
            DropIndex("dbo.CarSellingModels", new[] { "UserId" });
            DropColumn("dbo.CarServicingModels", "UserId");
            DropColumn("dbo.CarSellingModels", "UserId");
        }
    }
}
