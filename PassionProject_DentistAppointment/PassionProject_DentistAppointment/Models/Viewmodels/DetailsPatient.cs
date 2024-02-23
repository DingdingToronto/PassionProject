using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models.Viewmodels
{
    public class DetailsPatient
    {
        public PatientDto SelectedPatient { get; set; }
        public IEnumerable<DentistDto> BookedDentist { get; set; }

        public IEnumerable<DentistDto> AvailableDentist { get; set; }


    }
}