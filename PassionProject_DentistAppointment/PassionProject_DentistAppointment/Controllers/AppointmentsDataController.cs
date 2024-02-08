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