using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using KoiOrderingSystem.APIService.Grpcs;

namespace MVCWebApp.Controllers
{
    
    public class TravelGrpcsController : Controller
    {
        private readonly TravelGrpcService_.TravelGrpcService_Client _travelServiceClient;

        public TravelGrpcsController(TravelGrpcService_.TravelGrpcService_Client travelServiceClient)
        {
            _travelServiceClient = travelServiceClient;
        }

        // Action để tạo chuyến đi
        [HttpPost]
        public async Task<IActionResult> Create(TravelRequest travelRequest)
        {
            var response = await _travelServiceClient.CreateTravelAsync(travelRequest);
            if (response.Message.Contains("successfully"))
            {
                // Nếu thành công, bạn có thể điều hướng đến một trang khác hoặc trả về một thông báo
                return RedirectToAction("Index"); // Giả định bạn có action Index để xem danh sách chuyến đi
            }
            ModelState.AddModelError("", "Failed to create travel");
            return View(travelRequest);
        }

        // Action để lấy chuyến đi theo ID
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var response = await _travelServiceClient.GetTravelAsync(new TravelIdRequest { Id = id });
            if (response.Message.Contains("Found travel"))
            {
                // Chuyển đổi và trả về view với dữ liệu chuyến đi
                var travel = new TravelRequest { Id = id }; // Cần lấy dữ liệu thật từ response
                return View(travel);
            }
            return NotFound();
        }

        // Action để cập nhật chuyến đi
        [HttpPost]
        public async Task<IActionResult> Edit(TravelRequest travelRequest)
        {
            var response = await _travelServiceClient.UpdateTravelAsync(travelRequest);
            if (response.Message.Contains("successfully"))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to update travel");
            return View(travelRequest);
        }

        // Action để xóa chuyến đi
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _travelServiceClient.DeleteTravelAsync(new TravelIdRequest { Id = id });
            if (response.Message.Contains("successfully"))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        // Action để lấy danh sách tất cả chuyến đi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _travelServiceClient.ListTravelsAsync(new Empty());
            var travels = response.Travels; // Lấy danh sách chuyến đi từ response
            return View(travels);
        }
    }

}
