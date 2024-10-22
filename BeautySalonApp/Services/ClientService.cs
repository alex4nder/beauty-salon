using BeautySalonApp.Data;
using BeautySalonApp.Models;

namespace BeautySalonApp.Services
{
    public class ClientService
    {
        private readonly LocalDbContext _context;

        public ClientService(LocalDbContext context)
        {
            _context = context;
        }

        public void ClientAdd(Client client)
        {
            _context.Clients.Add(client);

            _context.SaveChanges();
        }

        public List<Client> ClientList()
        {
            return _context.Clients.ToList();
        }

        public void ClientRemove(int clientId)
        {
            var client = _context.Clients.Find(clientId);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }
    }
}
