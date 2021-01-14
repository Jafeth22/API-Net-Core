using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Api_ConnectionDB.Entities;
using Newtonsoft.Json;
//using System.Web.Http;

namespace Api_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private HttpClient client = new HttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult IndexAsync()
        {
            return View();
        }

        /// <summary>
        /// Return the infomacion of the Marca and send the information to the API, using GET 
        /// </summary>
        /// <param name="id">The id of the Marca</param>
        /// <returns>The result state of the task</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<JsonResult> GetMarca(string id = "")
        {
            try
            {
                id = id ?? "";
                string url = "https://localhost:44349/api/Marcas/" + (id.Trim().Length == 0 ? "" : id);
                var result = await this.client.GetStringAsync(url); //Nos va a devolver el formato en JSON
                var marcasList = JsonConvert.DeserializeObject<List<Marcas>>(result); //Convierte los datos de JSON a una LISTA

                return Json(marcasList);
            }
            catch (Exception e)
            {
                return Json("Server ocurred errors. Please check with admin!");
            }
        }

        /// <summary>
        /// Creates a new Marca and send the information to the API, using POST 
        /// </summary>
        /// <param name="marca">The values of the new Marca</param>
        /// <returns>The result state of the task</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public JsonResult CreateMarca(Marcas marca)
        
        {
            this.client.BaseAddress = new Uri("https://localhost:44349/api/Marcas");
            var result = this.client.PostAsJsonAsync<Marcas>("Marcas", marca);
            //La línea de arriba es lo mismo que la siguiente línea
            //var response = client.PostAsync("api/AgentCollection", new StringContent(new JavaScriptSerializer().Serialize(user), Encoding.UTF8, "application/json")).Result;
            //System.Web.Extensions.dll -> System.Web.Script.Serialization -> JavaScriptSerializer
            result.Wait();

            var status = result.Result;
            if (status.IsSuccessStatusCode)
            {
                return Json(status);
            }
            return Json("Server ocurred errors. Please check with admin!");
        }

        /// <summary>
        /// Deletes a Marca and send the information to the API, using DELETE 
        /// </summary>
        /// <param name="marca">The ID of the marca</param>
        /// <returns>The result state of the task</returns>
        public JsonResult DeleteMarca(string id)
        {
            this.client.BaseAddress = new Uri("https://localhost:44349/api/Marcas");
            var result = this.client.DeleteAsync($"Marcas/{id}");

            var status = result.Result;
            if (status.IsSuccessStatusCode)
            {
                return Json(status);
            }
            return Json("Server ocurred errors. Please check with admin!");
        }

        /// <summary>
        /// To get the values of that Item
        /// </summary>
        /// <param name="id">The item's id</param>
        /// <returns>The Item's values</returns>
        //public ActionResult EditMarca(string id)
        //{
        //    Marcas marca = null;
        //    this.client.BaseAddress = new Uri("https://localhost:44349/api/Marcas");
        //    var result = this.client.GetAsync($"Marcas/{id}");
        //    result.Wait();

        //    var status = result.Result;
        //    if (status.IsSuccessStatusCode)
        //    {
        //        var readTask = status.Content.ReadAsAsync<Marcas>();
        //        readTask.Wait();
        //        marca = readTask.Result;
        //    }
        //    return View(marca);
        //}

        /// <summary>
        /// To update the values of that Marca and sent to the API, using PUT
        /// </summary>
        /// <param name="marca">values to update the Marca</param>
        /// <returns></returns>
        public JsonResult EditMarca(Marcas marca)
        {
            this.client.BaseAddress = new Uri("https://localhost:44349/api/Marcas");
            var result = this.client.PutAsJsonAsync<Marcas>("Marcas", marca);
            result.Wait();

            var status = result.Result;
            if (status.IsSuccessStatusCode)
            {
                return Json(status);
            }
            return Json(marca);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
