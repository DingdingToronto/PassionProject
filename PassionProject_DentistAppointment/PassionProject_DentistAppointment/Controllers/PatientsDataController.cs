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
    public class PatientsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientsData/ListPatient
        [HttpGet]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> Patients = db.Patients.ToList();
            List<PatientDto> PatientDto = new List<PatientDto>();

            Patients.ForEach(a => PatientDto.Add(new PatientDto()
            {
                PatientID = a.PatientID,
                PatientName = a.PatientName,
                Phone = a.Phone,
                Email = a.Email,
               
            }));


            return PatientDto;
        }


        // GET: api/PatientsData/FindPatient/1
        [ResponseType(typeof(Patient))]
        [HttpGet]
        public IHttpActionResult FindPatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            PatientDto PatientDto = new PatientDto()
            {
                PatientID = Patient.PatientID,
                PatientName = Patient.PatientName,
                Phone = Patient.Phone,
                Email = Patient.Email,
            };
            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }
        // POST: api/PatientsData/UpdatePatient/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePatient(int id, Patient Patient)
        {
            Debug.WriteLine("I have reached the update Patient method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Patient.PatientID)
            {
               
                return BadRequest();
            }

            db.Entry(Patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    Debug.WriteLine("Patient not found");
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

        private bool PatientExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/PatientsData/AddPatient
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient Patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(Patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Patient.PatientID }, Patient);
        }


        // POST: api/PatientData/DeletePatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            if (Patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(Patient);
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

        private bool PatientsExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}