namespace PassionProject_DentistAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientDentist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientDentists",
                c => new
                    {
                        Patient_PatientID = c.Int(nullable: false),
                        Dentist_DentistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_PatientID, t.Dentist_DentistID })
                .ForeignKey("dbo.Patients", t => t.Patient_PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Dentists", t => t.Dentist_DentistID, cascadeDelete: true)
                .Index(t => t.Patient_PatientID)
                .Index(t => t.Dentist_DentistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDentists", "Dentist_DentistID", "dbo.Dentists");
            DropForeignKey("dbo.PatientDentists", "Patient_PatientID", "dbo.Patients");
            DropIndex("dbo.PatientDentists", new[] { "Dentist_DentistID" });
            DropIndex("dbo.PatientDentists", new[] { "Patient_PatientID" });
            DropTable("dbo.PatientDentists");
        }
    }
}
