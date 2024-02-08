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
    public class DentistsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DentistsData/ListDentist
        [HttpGet]
        public IEnumerable<DentistDto> ListDentists()
        {
            List<Dentist>Dentists = db.Dentists.ToList();
            List<DentistDto> DentistDtos = new List<DentistDto>();

            Dentists.ForEach(a => DentistDtos.Add(new DentistDto()
            {
                DentistID = a.DentistID,
                DentistName = a.DentistName,
                Specialization = a.Specialization,
                Phone = a.Phone,
                Email = a.Email,
                Address = a.Address,
                Rating = a.Rating,
            }));

            
            return DentistDtos;
        }

        // GET: api/DentistData/FindDentist/1
        [ResponseType(typeof(Dentist))]
        [HttpGet]
        public IHttpActionResult FindDentist(int id)
        {
            Dentist Dentist = db.Dentists.Find(id);
            DentistDto DentistDto = new DentistDto()
            {
                DentistID = Dentist.DentistID,
                DentistName = Dentist.DentistName,
                Specialization = Dentist.Specialization,
                Phone = Dentist.Phone,
                Email = Dentist.Email,
                Address = Dentist.Address,
                Rating = Dentist.Rating,
            };
            if (Dentist == null)
            {
                return NotFound();
            }

            return Ok(DentistDto);
        }
        // POST: api/DentistData/UpdateDentist/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDentist(int id, Dentist Dentist)
        {
            Debug.WriteLine("I have reached the update Dentist method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Dentist.DentistID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("id:" + id);
                Debug.WriteLine("DentistID:"+Dentist.DentistID);
                return BadRequest();
            }

            db.Entry(Dentist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DentistExists(id))
                {
                    Debug.WriteLine("Dentist not found");
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

        // POST: api/DentistsData/AddDentist
        [ResponseType(typeof(Dentist))]
        [HttpPost]
        public IHttpActionResult AddDentist(Dentist Dentist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dentists.Add(Dentist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Dentist.DentistID }, Dentist);
        }


        // POST: api/DentistData/DeleteDentist/5
        [ResponseType(typeof(Dentist))]
        [HttpPost]
        public IHttpActionResult DeleteDentist(int id)
        {
            Dentist Dentist = db.Dentists.Find(id);
            if (Dentist == null)
            {
                return NotFound();
            }

            db.Dentists.Remove(Dentist);
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

        private bool DentistExists(int id)
        {
            return db.Dentists.Count(e => e.DentistID == id) > 0;
        }
    }
}