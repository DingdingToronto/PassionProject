using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models.Viewmodels
{
    public class DetailsDentist
    {
        public DentistDto SelectedDentist { get; set; }
        public IEnumerable<AppointmentDto> BookedAppointments { get; set; }

        public IEnumerable<AppointmentDto> AvailableAppointments { get; set; }

     
    }
}