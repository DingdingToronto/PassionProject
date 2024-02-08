namespace PassionProject_DentistAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DentistAppointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DentistAppointments",
                c => new
                    {
                        Dentist_DentistID = c.Int(nullable: false),
                        Appointment_AppointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dentist_DentistID, t.Appointment_AppointmentID })
                .ForeignKey("dbo.Dentists", t => t.Dentist_DentistID, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointment_AppointmentID, cascadeDelete: true)
                .Index(t => t.Dentist_DentistID)
                .Index(t => t.Appointment_AppointmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DentistAppointments", "Appointment_AppointmentID", "dbo.Appointments");
            DropForeignKey("dbo.DentistAppointments", "Dentist_DentistID", "dbo.Dentists");
            DropIndex("dbo.DentistAppointments", new[] { "Appointment_AppointmentID" });
            DropIndex("dbo.DentistAppointments", new[] { "Dentist_DentistID" });
            DropTable("dbo.DentistAppointments");
        }
    }
}
