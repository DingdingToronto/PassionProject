namespace PassionProject_DentistAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientDentistAppointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientDentistAppointments",
                c => new
                    {
                        PatientDentistAppointmentID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        DentistID = c.Int(nullable: false),
                        AppointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientDentistAppointmentID)
                .ForeignKey("dbo.Appointments", t => t.AppointmentID, cascadeDelete: true)
                .ForeignKey("dbo.Dentists", t => t.DentistID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DentistID)
                .Index(t => t.AppointmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDentistAppointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.PatientDentistAppointments", "DentistID", "dbo.Dentists");
            DropForeignKey("dbo.PatientDentistAppointments", "AppointmentID", "dbo.Appointments");
            DropIndex("dbo.PatientDentistAppointments", new[] { "AppointmentID" });
            DropIndex("dbo.PatientDentistAppointments", new[] { "DentistID" });
            DropIndex("dbo.PatientDentistAppointments", new[] { "PatientID" });
            DropTable("dbo.PatientDentistAppointments");
        }
    }
}
