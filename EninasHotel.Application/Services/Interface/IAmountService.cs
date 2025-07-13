using EninasHotel.Domain.Entities;

namespace EninasHotel.Application.Services.Interface
{
    public interface IAmountService
    {
        IEnumerable<Amount> GetAllAmounts();
        IQueryable<Amount> GetQueryAmounts();
        Amount GetAmountById(Guid id);
        void CreateAmount(Amount amount);
        void UpdateAmount(Amount amount);
        bool DeleteAmount(Guid id);
        //IEnumerable<Amount> GetVillasAvailabilityByDate(int nights, DateOnly checkInDate);
        //bool IsVillaAvailableByDate(int villaId, int nights, DateOnly checkInDate);
    }
}
