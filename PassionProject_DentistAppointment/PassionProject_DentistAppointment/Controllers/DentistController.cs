using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject_DentistAppointment.Models;
using System.Web.Script.Serialization;


namespace PassionProject_DentistAppointment.Controllers
{
    public class DentistController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        static DentistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44366/api/");

        }
        // GET: Dentist/List
        public ActionResult List()
        {
            //objective: communicate with our dentist data api to retrieve a list of dentists
            //curl https://localhost:44366/api/DentistsData/ListDentists


            string url = "dentistsdata/ListDentists";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<DentistDto> dentists = response.Content.ReadAsAsync<IEnumerable<DentistDto>>().Result;
            Debug.WriteLine("Number of dentists received : ");
            Debug.WriteLine(dentists.Count());



            return View(dentists);
        }

        // GET: Dentist/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our dentist data api to retrieve one dentist
            //curl https://localhost:44366/api/dentistdata/finddentist/{id}

            string url = "dentistsdata/finddentist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DentistDto selecteddentist = response.Content.ReadAsAsync<DentistDto>().Result;
            Debug.WriteLine("dentist received : ");
            Debug.WriteLine(selecteddentist.Specialization);


            return View(selecteddentist);
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

        // POST: Dentist/Create
        [HttpPost]
        public ActionResult Create(Dentist dentist)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(animal.AnimalName);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/adddentist 
            string url = "dentistsdata/adddentist";


            string jsonpayload = jss.Serialize(dentist);

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

        // GET: Dentist/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the animal information

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44366/api/dentistdata/finddentist/{id}

            string url = "dentistsdata/finddentist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DentistDto selecteddentist = response.Content.ReadAsAsync<DentistDto>().Result;
            Debug.WriteLine("dentist received : ");
            

            return View(selecteddentist);
        }

        // POST: dentist/Update/5
        [HttpPost]
        public ActionResult Update(int id, Dentist dentist)
        {
            
            try
            {
                Debug.WriteLine("The new  info is:");
                Debug.WriteLine(dentist.DentistID);
               

                //serialize into JSON
                //Send the request to the API

                string url = "dentistsdata/UpdateDentist/" + id;


                string jsonpayload = jss.Serialize(dentist);
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
            string url = "dentistsdata/finddentist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DentistDto selecteddentist = response.Content.ReadAsAsync<DentistDto>().Result;
            return View(selecteddentist);
        }

        // POST: Dentist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "dentistsdata/deletedentist/" + id;
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
