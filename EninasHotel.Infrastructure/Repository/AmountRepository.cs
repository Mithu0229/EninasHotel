using EninasHotel.Application.Common.Interfaces;
using EninasHotel.Domain.Entities;
using EninasHotel.Infrastructure.Data;

namespace EninasHotel.Infrastructure.Repository
{
    public class AmountRepository : Repository<Amount>, IAmountRepository
    {
        private readonly ApplicationDbContext _db;

        public AmountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Amount entity)
        {
            _db.Amounts.Update(entity);
        }

        public IQueryable<Amount> GetQueryAmounts()
        {
            return _db.Amounts.AsQueryable();
        }

    }
}
