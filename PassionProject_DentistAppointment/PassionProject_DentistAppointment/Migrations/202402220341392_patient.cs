namespace PassionProject_DentistAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        PatientName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
            CreateTable(
                "dbo.PatientAppointments",
                c => new
                    {
                        Patient_PatientID = c.Int(nullable: false),
                        Appointment_AppointmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_PatientID, t.Appointment_AppointmentID })
                .ForeignKey("dbo.Patients", t => t.Patient_PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointment_AppointmentID, cascadeDelete: true)
                .Index(t => t.Patient_PatientID)
                .Index(t => t.Appointment_AppointmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientAppointments", "Appointment_AppointmentID", "dbo.Appointments");
            DropForeignKey("dbo.PatientAppointments", "Patient_PatientID", "dbo.Patients");
            DropIndex("dbo.PatientAppointments", new[] { "Appointment_AppointmentID" });
            DropIndex("dbo.PatientAppointments", new[] { "Patient_PatientID" });
            DropTable("dbo.PatientAppointments");
            DropTable("dbo.Patients");
        }
    }
}
