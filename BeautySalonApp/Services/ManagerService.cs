using BeautySalonApp.Data;
using BeautySalonApp.Models;

namespace BeautySalonApp.Services
{
    public class ManagerService : User
    {
        private readonly GlobalDbContext _context;

        private Manager Manager;

        public ManagerService(Manager manager, GlobalDbContext context)
        {
            this.Manager = manager;
            _context = context;
        }


        public override bool Login()
        {
            var manager = _context.Managers.Find(this.Manager.Login);

            if (manager != null)
            {
                return true;
            }

            return false;
        }
    }
}
