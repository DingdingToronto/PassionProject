using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject_DentistAppointment.Models;
using System.Web.Script.Serialization;
using PassionProject_DentistAppointment.Models.Viewmodels;
using PassionProject_DentistAppointment.Migrations;

namespace PassionProject_DentistAppointment.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44366/api/");

        }
        // GET: Patient/List
        public ActionResult List()
        {
            //objective: communicate with our Patient data api to retrieve a list of Patients
          

            string url = "Patientsdata/ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            Debug.WriteLine("Number of patients received : ");
            Debug.WriteLine(patients.Count());



            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            DetailsPatient ViewModel = new DetailsPatient();

            string url = "patientsdata/findpatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            Debug.WriteLine("Patient received : ");
            Debug.WriteLine(selectedPatient.PatientName);

            ViewModel.SelectedPatient = selectedPatient;


            url = "dentistsdata/listDentistForPatient/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DentistDto> BookedDentist = response.Content.ReadAsAsync<IEnumerable<DentistDto>>().Result;

            ViewModel.BookedDentist = BookedDentist;


            url = "dentistsdata/listDentistNotForPatient/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DentistDto> AvailableDentist = response.Content.ReadAsAsync<IEnumerable<DentistDto>>().Result;

            ViewModel.AvailableDentist = AvailableDentist;




            return View(ViewModel);
        }



        [HttpPost]
        public ActionResult Associate(int id, int dentistid)
        {
            Debug.WriteLine("Attempting to associate patient :" + id + " with dentistid " + dentistid);


            string url = "dentistsdata/AssociateDentistWithPatient/" + id + "/" + dentistid;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        [HttpGet]
        public ActionResult UnAssociate(int id, int dentistid)
        {
            Debug.WriteLine("Attempting to unassociate patient :" + id + " with dentist: " + dentistid);

            //call our api to associate animal with keeper
            string url = "DentistsData/UnAssociateDentistWithPatient/" + id + "/" + dentistid;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }




        public ActionResult Error()
        {

            return View();
        }
        // GET: Dentist/new
        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient Patient)
        {
            Debug.WriteLine("the json payload is :");
          

            string url = "Patientsdata/addPatient";


            string jsonpayload = jss.Serialize(Patient);

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

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {

            string url = "Patientsdata/findPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            PatientDto selectedpatient = response.Content.ReadAsAsync<PatientDto>().Result;
            Debug.WriteLine("Patient received : ");


            return View(selectedpatient);
        }

        // POST: patient/Update/5
        [HttpPost]
        public ActionResult Update(int id, Patient patient)
        {
          
            try
            {
                Debug.WriteLine("The new  info is:");
                Debug.WriteLine(patient.PatientID);


                //serialize into JSON
                //Send the request to the API

                string url = "patientsdata/Updatepatient/" + id;


                string jsonpayload = jss.Serialize(patient);
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


        // GET: Dentist/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "patientsdata/findpatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedpatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedpatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Patientsdata/deletePatient/" + id;
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
