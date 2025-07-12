using EninasHotel.Domain.Entities;

namespace EninasHotel.Application.Common.Interfaces
{
    public interface IAmountRepository:IRepository<Amount>
    {
        void Update(Amount entity);
        void Save();
    }
}
