using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Owin.BuilderProperties;
using PassionProject_DentistAppointment.Models;
using PassionProject_DentistAppointment.Models.Viewmodels;

namespace PassionProject_DentistAppointment.Controllers
{
    public class AppointmentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentsData/ListAppointment
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointment()
        {
            List<Appointment> Appointments = db.Appointments.ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = a.AppointmentID,
                AppointmentDate = a.AppointmentDate,
              
            }));


            return AppointmentDtos;
        }

        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult listAppointmentForDentist(int id)
        {

            List<Appointment> Appointments = db.Appointments.Where(
                k => k.Dentists.Any(
                    a => a.DentistID == id)
                ).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(k => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = k.AppointmentID,
                AppointmentDate = k.AppointmentDate,
                
            }));

            return Ok(AppointmentDtos);
        }
       
        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult listAppointmentNotForDentist(int id)
        {
            List<Appointment> Appointments = db.Appointments.Where(
                k => !k.Dentists.Any(
                    a => a.DentistID == id)
                ).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(k => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = k.AppointmentID,
                AppointmentDate = k.AppointmentDate,
            }));

            return Ok(AppointmentDtos);
        }
        [HttpPost]
        [Route("api/appointmentsdata/AssociateAppointmentWithDentist/{dentistid}/{appointmentid}")]
        public IHttpActionResult AssociateAnimalWithKeeper(int dentistid, int appointmentid)
        {

            Dentist SelectedDentist = db.Dentists.Include(a => a.Appointments).Where(a => a.DentistID == dentistid).FirstOrDefault();
            Appointment SelectedAppointment = db.Appointments.Find(appointmentid);

            if (SelectedDentist == null || SelectedAppointment == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input dentist id is: " + dentistid);
            Debug.WriteLine("selected dentist name is: " + SelectedDentist.DentistName);
            Debug.WriteLine("input appointment id is: " + appointmentid);
            Debug.WriteLine("selected appointment date is: " + SelectedAppointment.AppointmentDate);



            SelectedDentist.Appointments.Add(SelectedAppointment);
            db.SaveChanges();

            return Ok();
        }
        [HttpPost]
        [Route("api/AppointmentsData/UnAssociateAppointmentWithDentist/{dentistid}/{appointmentid}")]
        public IHttpActionResult UnAssociateAppointmentWithDentist(int dentistid, int appointmentid)
        {

            Dentist SelectedDentist = db.Dentists.Include(a => a.Appointments).Where(a => a.DentistID == dentistid).FirstOrDefault();
            Appointment SelectedAppointment = db.Appointments.Find(appointmentid);

            if (SelectedDentist == null || SelectedAppointment == null)
            {
                return NotFound();
            }

            //todo: verify that the keeper actually is keeping track of the animal

            SelectedDentist.Appointments.Remove(SelectedAppointment);
            db.SaveChanges();

            return Ok();
        }


        // GET: api/AppointmentsData/FindAppointment/1
        [ResponseType(typeof(Appointment))]
        [HttpGet]
        public IHttpActionResult FindAppointment(int id)
        {
            Appointment Appointment = db.Appointments.Find(id);
            AppointmentDto AppointmentDto = new AppointmentDto()
            {
                 AppointmentID = Appointment.AppointmentID,
                AppointmentDate = Appointment.AppointmentDate,
            };
            if (Appointment == null)
            {
                return NotFound();
            }

            return Ok(AppointmentDto);
        }

        // POST: api/AppointmentsData/UpdateAppointment/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id, Appointment Appointment)
        {
            
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Appointment.AppointmentID)
            {
               
                return BadRequest();
            }

            db.Entry(Appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    Debug.WriteLine("Appointment not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/AppointmentsData/AddAppointment
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment Appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(Appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Appointment.AppointmentID }, Appointment);
        }

        // POST: api/AppointmentData/DeleteAppointment/5
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment Appointment = db.Appointments.Find(id);
            if (Appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(Appointment);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AppointmentID == id) > 0;
        }
    }
}