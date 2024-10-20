using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Services
{
    public class SalonService
    {
        private readonly GlobalDbContext _globalContext;

        public SalonService(GlobalDbContext globalContext)
        {
            _globalContext = globalContext;
        }

        // Метод для получения списка салонов с их адресами
        public List<Salon> GetSalons()
        {
            return _globalContext.Salons.Include(s => s.Address).ToList();
        }
    }
}
