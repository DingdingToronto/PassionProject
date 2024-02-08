namespace PassionProject_DentistAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dentists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dentists",
                c => new
                    {
                        DentistID = c.Int(nullable: false, identity: true),
                        DentistName = c.String(),
                        Specialization = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DentistID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dentists");
        }
    }
}
