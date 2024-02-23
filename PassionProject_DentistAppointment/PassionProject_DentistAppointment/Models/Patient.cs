using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string PatientName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        // Navigation property for appointments
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Dentist> Dentist { get; set; }

        public virtual ICollection<PatientDentistAppointment> PatientDentistAppointments { get; set; }

    }

    public class PatientDto
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}