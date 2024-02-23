using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_DentistAppointment.Models.Viewmodels
{
    public class DetailsAppointment
    {
        public AppointmentDto SelectedAppointment { get; set; }
        public IEnumerable<DentistDto> ChosenDentist { get; set; }
    }
}