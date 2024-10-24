using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Client = BeautySalonApp.Models.Client;

namespace BeautySalonApp.Forms
{
    public partial class ClientForm : Form
    {
        private readonly ClientService _clientService;

        private Client _client;
        private bool _isEditMode;

        public ClientForm(Client? client = null)
        {
            InitializeComponent();

            _clientService = Program.ServiceProvider.GetRequiredService<ClientService>();

            if (client != null)
            {
                _client = client;
                _isEditMode = true;
                PreFillClientData();
            }
            else
            {
                _client = new Client
                {
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    DateOfBirth = DateTime.Now
                };
                _isEditMode = false;
            }
        }

        private void PreFillClientData()
        {
            clientFirstNameTextBox.Text = _client.FirstName;
            clientLastNameTextBox.Text = _client.LastName;
            clientPhoneTextBox.Text = _client.Phone;
            clientEmailTextBox.Text = _client.Email;
            clientDateOfBirthDateTimePicker.Value = _client.DateOfBirth;
            clientNotesRichTextBox.Text = _client.Notes;
        }

        private void saveClientBtn_Click(object sender, EventArgs e)
        {
            _client.FirstName = clientFirstNameTextBox.Text;
            _client.LastName = clientLastNameTextBox.Text;
            _client.Phone = clientPhoneTextBox.Text;
            _client.Email = clientEmailTextBox.Text;
            _client.DateOfBirth = clientDateOfBirthDateTimePicker.Value;
            _client.Notes = clientNotesRichTextBox.Text;

            if (_isEditMode)
            {
                _clientService.ClientEdit(_client);
            }
            else
            {
                _clientService.ClientAdd(_client);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
