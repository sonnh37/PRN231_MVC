using KoiOrderingSystem.Common;
using KoiOrderingSystem.Data.Models;
using KoiOrderingSystem.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVCWebApp.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public IActionResult Index()
        {
            return View();
        }

        // GET: Ajax/GetTravels
        public async Task<JsonResult> GetTravels()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Travels"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Travel>>(result.Data.ToString());
                            return Json(new { message = "Data retrieved successfully", data });
                        }
                    }
                }
            }
            return Json(new { message = "No data found", data = new List<Travel>() });
        }

        public IActionResult Details(Guid? id)
        {
            var model = new Travel { Id = id.Value }; // Tạo mô hình mới với ID
            return View(model);
        }

        // GET: Ajax/GetDetailsDetails/5
        public async Task<JsonResult> GetTravelDetails(Guid? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Travels/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Travel>(result.Data.ToString());
                            return Json(new
                            {
                                success = true,
                                data
                            });
                        }
                    }
                }
            }
            return Json(new
            {
                success = false,
                message = "Travel not found."
            });
        }
    }
}
