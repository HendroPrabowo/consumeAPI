using Microsoft.AspNetCore.Mvc;
using ConsumeAPICore.Helper;
using System.Threading.Tasks;
using ConsumeAPICore.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsumeAPICore.Controllers
{
    public class RequestController : Controller
    {
        RequestAPI _api = new RequestAPI();
        private readonly CarContext car_context = new CarContext();
        // GET: Request
        public async Task<IActionResult> Index()
        {
            List<RequestViewModel> requests = new List<RequestViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("orders");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                requests = JsonConvert.DeserializeObject<List<RequestViewModel>>(result);
            }

            return View(requests);
        }

        public async Task<IActionResult> Accept(int id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("order/approve/" + id);
            if (res.IsSuccessStatusCode)
            {
                RequestViewModel requests = new RequestViewModel();
                HttpResponseMessage res2 = await client.GetAsync("order/" + id);
                if (res2.IsSuccessStatusCode)
                {
                    var result = res2.Content.ReadAsStringAsync().Result;
                    requests = JsonConvert.DeserializeObject<RequestViewModel>(result);
                    setCarToNotAvaiable(requests.cars_id);
                }

                return RedirectToAction("Index", "Request");
            }

            return RedirectToAction("Index", "Request");
        }

        public void setCarToNotAvaiable(int id)
        {
            Car car = car_context.Car.Find(id);
            car.Car_Status = 1;

            car_context.Update(car);
            car_context.SaveChangesAsync();
        }

        public async Task<IActionResult> Reject(int id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("order/reject/" + id);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Request");
            }

            return RedirectToAction("Index", "Request");
        }

        public async Task<IActionResult> Detail(int id)
        {
            RequestViewModel requests = new RequestViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("order/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                requests = JsonConvert.DeserializeObject<RequestViewModel>(result);
            }

            return View(requests);
        }
    }
}