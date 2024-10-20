using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeautySalonApp.Services
{
    public class SalonService
    {
        private readonly GlobalDbContext _context;

     
        public SalonService(GlobalDbContext context)
        {
            _context = context;
        }

        // Метод для получения списка салонов с их адресами
        public List<Salon> GetSalons()
        {
            using (var context = _context)
            {
                return context.Salons.Include(s => s.Address).ToList();
            }
        }
    }
}
