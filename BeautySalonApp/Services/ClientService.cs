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

        public List<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClientById(int clientId)
        {
            return _context.Clients.Find(clientId);
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

        public void ClientEdit(Client client)
        {
            var existingClient = _context.Clients.Find(client.Id);
            if (existingClient != null)
            {
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Phone = client.Phone;
                existingClient.Email = client.Email;
                existingClient.DateOfBirth = client.DateOfBirth;
                existingClient.Notes = client.Notes;

                _context.SaveChanges();
            }
        }
    }
}
