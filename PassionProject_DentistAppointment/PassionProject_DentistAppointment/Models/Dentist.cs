using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models
{
    public class Dentist
    {

        [Key]
        public int DentistID { get; set; }
        public string DentistName { get; set; }

       public string Specialization {  get; set; }

        public string Phone { get; set; }

      
        public string Email { get; set; }

        
        public string Address { get; set; }

        public int Rating { get; set; }

        // Navigation property for appointments
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<PatientDentistAppointment> PatientDentistAppointments { get; set; }

    }

    public class DentistDto
    {
        public int DentistID { get; set; }
        public string DentistName { get; set; }

        public string Specialization { get; set; }

        public string Phone { get; set; }


        public string Email { get; set; }


        public string Address { get; set; }

        public int Rating { get; set; }



    }
}