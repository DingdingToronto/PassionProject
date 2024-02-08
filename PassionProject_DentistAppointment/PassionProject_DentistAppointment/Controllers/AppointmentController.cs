using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject_DentistAppointment.Models;
using System.Web.Script.Serialization;
using PassionProject_DentistAppointment.Migrations;

namespace PassionProject_DentistAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44366/api/");

        }


        // GET: Appointment/List
        public ActionResult List()
        {
            //objective: communicate with our dentist data api to retrieve a list of dentists
            //curl https://localhost:44366/api/AppointmentsData/ListDentists


            string url = "Appointmentsdata/ListAppointment";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AppointmentDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            Debug.WriteLine("Number of dentists received : ");
            Debug.WriteLine(appointments.Count());



            return View(appointments);
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Appointment data api to retrieve one Appointment
            //curl https://localhost:44336/api/appointmentsdata/findappointment/{id}

            string url = "appointmentsdata/findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            Debug.WriteLine("appointment received : ");
            Debug.WriteLine(selectedappointment.AppointmentDate);


            return View(selectedappointment);
        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Appointment/new
        public ActionResult New()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(Appointment Appointment)
        {
            Debug.WriteLine("the json payload is :");
            
            string url = "appointmentsdata/addappointment";


            string jsonpayload = jss.Serialize(Appointment);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }


        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the animal information

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44366/api/Appointmentsdata/findAppointment/{id}

            string url = "Appointmentsdata/findAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            Debug.WriteLine("Appointment received : ");


            return View(selectedappointment);
        }

        // POST: appointment/Update/5
        [HttpPost]
        public ActionResult Update(int id, Appointment Appointment)
        {

            try
            {
                Debug.WriteLine("The new  info is:");
                Debug.WriteLine(Appointment.AppointmentID);


                //serialize into JSON
                //Send the request to the API

                string url = "Appointmentsdata/UpdateAppointment/" + id;


                string jsonpayload = jss.Serialize(Appointment);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/DentistData/UpdateDentist/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Appointmentsdata/findAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            return View(selectedappointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Appointmentsdata/deleteAppointment/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

    }
}
