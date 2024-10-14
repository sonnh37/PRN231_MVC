using Grpc.Core;
using KoiOrderingSystem.Data.Models;
using KoiOrderingSystem.Service;


namespace KoiOrderingSystem.APIService.Grpcs
{
    public class TravelGrpcService : TravelGrpcService_.TravelGrpcService_Base
    {
        private readonly List<TravelRequest> travels = new List<TravelRequest>();
        private readonly ITravelService _travelService;
        public TravelGrpcService() => _travelService ??= new TravelService();


        public override Task<TravelReply> CreateTravel(TravelRequest request, ServerCallContext context)
        {
            travels.Add(request);
            return Task.FromResult(new TravelReply { Message = "Travel created successfully" });
        }

        public override Task<TravelReply> GetTravel(TravelIdRequest request, ServerCallContext context)
        {
            var travel = travels.FirstOrDefault(t => t.Id == request.Id);
            if (travel != null)
            {
                return Task.FromResult(new TravelReply { Message = $"Found travel with id {request.Id}" });
            }
            return Task.FromResult(new TravelReply { Message = "Travel not found" });
        }

        // Implement UpdateTravel, DeleteTravel, and ListTravels similarly
        public override Task<TravelReply> UpdateTravel(TravelRequest request, ServerCallContext context)
        {
            var travel = travels.FirstOrDefault(t => t.Id == request.Id);
            if (travel != null)
            {
                //travel.Destination = request.Destination;
                //travel.Description = request.Description;
                //travel.StartDate = request.StartDate;
                //travel.EndDate = request.EndDate;
                return Task.FromResult(new TravelReply { Message = "Travel updated successfully" });
            }
            return Task.FromResult(new TravelReply { Message = "Travel not found" });
        }

        public override Task<TravelReply> DeleteTravel(TravelIdRequest request, ServerCallContext context)
        {
            var travel = travels.FirstOrDefault(t => t.Id == request.Id);
            if (travel != null)
            {
                travels.Remove(travel);
                return Task.FromResult(new TravelReply { Message = "Travel deleted successfully" });
            }
            return Task.FromResult(new TravelReply { Message = "Travel not found" });
        }

        public override async Task<TravelListReply> ListTravels(Empty request, ServerCallContext context)
        {
            try
            {
                // Lấy danh sách chuyến đi từ dịch vụ
                var br = await _travelService.GetAll();

                // Kiểm tra xem br có null không và nếu không có Data, tạo một danh sách rỗng
                if (br == null || br.Data == null)
                {
                    return new TravelListReply { Travels = { } }; // Trả về danh sách rỗng
                }

                // Chuyển đổi danh sách chuyến đi thành TravelRequest
                var travelList = br.Data as List<Travel> ?? new List<Travel>(); // Nếu không chuyển đổi được, tạo danh sách rỗng

                var travelRequests = travelList.Select(travel => new TravelRequest
                {
                    Id = travel.Id.ToString(),
                    Name = travel.Name,
                    Location = travel.Location,
                    Note = travel.Note,
                }).ToList();

                // Trả về danh sách chuyến đi
                return new TravelListReply { Travels = { travelRequests } };
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                context.Status = new Status(StatusCode.Unknown, ex.Message);
                return new TravelListReply(); // Trả về danh sách rỗng hoặc thông báo lỗi
            }
        }

    }
}
