using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }

        // Navigation property for dentists
        public virtual ICollection<Dentist> Dentists { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<PatientDentistAppointment> PatientDentistAppointments { get; set; }
    }

    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }

    }
}