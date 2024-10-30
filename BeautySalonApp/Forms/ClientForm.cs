using BeautySalonApp.Models.BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;

        private Customer _customer;
        private bool _isEditMode;

        public CustomerForm(Customer? customer = null)
        {
            InitializeComponent();

            _customerService = Program.ServiceProvider.GetRequiredService<CustomerService>();

            if (customer != null)
            {
                _customer = customer;
                _isEditMode = true;
                PreFillClientData();
            }
            else
            {
                _customer = new Customer
                {
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Birthday = DateTime.Now
                };
                _isEditMode = false;
            }
            SetFormTitle();
        }

        private void SetFormTitle()
        {
            this.Text = _isEditMode ? GetEditModeTitle() : GetCreateModeTitle();
        }

        private string GetEditModeTitle()
        {
            return $"Клиент: {_customer.FirstName} {_customer.LastName}";
        }

        private string GetCreateModeTitle()
        {
            return "Добавление клиента";
        }
        private void PreFillClientData()
        {
            clientFirstNameTextBox.Text = _customer.FirstName;
            clientLastNameTextBox.Text = _customer.LastName;
            clientPhoneTextBox.Text = _customer.Phone;
            clientEmailTextBox.Text = _customer.Email;
            clientDateOfBirthDateTimePicker.Value = _customer.Birthday;
        }

        private void saveClientBtn_Click(object sender, EventArgs e)
        {
            _customer.FirstName = clientFirstNameTextBox.Text;
            _customer.LastName = clientLastNameTextBox.Text;
            _customer.Phone = clientPhoneTextBox.Text;
            _customer.Email = clientEmailTextBox.Text;
            _customer.Birthday = clientDateOfBirthDateTimePicker.Value;

            if (_isEditMode)
            {
                _customerService.CustomerEdit(_customer);
            }
            else
            {
                _customerService.CustomerAdd(_customer);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
