using BeautySalonApp.Data;
using BeautySalonApp.Models;

namespace BeautySalonApp.Services
{
    public class ManagerService
    {
        private readonly GlobalDbContext _globalContext;
        private readonly LocalDbContext _localContext;

        public ManagerService(GlobalDbContext globalContext, LocalDbContext localContext)
        {
            _globalContext = globalContext;
            _localContext = localContext;
        }

        public void ManagerAdd(Manager manager)
        {
            _globalContext.Managers.Add(manager);
            _globalContext.SaveChanges();
        }

        public List<Manager> GetManagers(int salonId)
        {
            return _globalContext.Managers
            .Where(m => m.SalonId == salonId)
            .ToList();
        }

        public Manager GetManagerById(int managerId)
        {
            return _globalContext.Managers.Find(managerId);
        }

        public void ManagerRemove(int managerId)
        {
            var manager = _globalContext.Managers.Find(managerId);
            if (manager != null)
            {
                _globalContext.Managers.Remove(manager);
                _globalContext.SaveChanges();
            }
        }

        public void ManagerEdit(Manager manager)
        {
            var existingManager = _globalContext.Managers.Find(manager.Id);
            if (existingManager != null)
            {
                existingManager.FirstName = manager.FirstName;
                existingManager.LastName = manager.LastName;
                existingManager.Phone = manager.Phone;
                existingManager.Email = manager.Email;

                _globalContext.SaveChanges();
            }
        }
    }
}
