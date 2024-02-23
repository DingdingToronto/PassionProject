using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models
{
    public class PatientDentistAppointment
    {
        [Key]
        public int PatientDentistAppointmentID { get; set; }

        // Foreign keys
        public int PatientID { get; set; }
        public int DentistID { get; set; }
        public int AppointmentID { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual Dentist Dentist { get; set; }
        public virtual Appointment Appointment { get; set; }

    }
}