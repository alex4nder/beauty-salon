using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class ClientService
    {
        private DatabaseService _databaseService;
        private LocalDbContext _context;
        private readonly CurrentSalonContext _currentSalonContext;

        public ClientService()
        {
            _currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _context = _databaseService.GetLocalDbContext(_currentSalonContext.SalonId);
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
