using HotelListing.Data;
using HotelListing.IRepository;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        private IGenericRepository<Country> _countryRepository;
        private IGenericRepository<Hotel> _hotelRepository;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Country> Countries => _countryRepository ??= new GenericRepository<Country>(_dbContext);

        public IGenericRepository<Hotel> Hotels => _hotelRepository ??= new GenericRepository<Hotel>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}