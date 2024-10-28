using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class AppointmentForm : Form
    {
        private readonly CustomerService _customerService;
        private readonly OfferingsService _offeringsService;
        private readonly EmployeeService _employeeService;
        private Guid _employeeId;

        public AppointmentForm(Guid employeeId)
        {
            InitializeComponent();

            _customerService = Program.ServiceProvider.GetRequiredService<CustomerService>();
            _offeringsService = Program.ServiceProvider.GetRequiredService<OfferingsService>();
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();

            _employeeId = employeeId;
        }

        private void LoadClients()
        {
            var clients = _customerService.GetCustomers();
            comboBoxClient.DataSource = clients;
            comboBoxClient.DisplayMember = "FirstName"; // Предположим, что у клиента есть свойство FullName
            comboBoxClient.ValueMember = "Id"; // Идентификатор клиента
        }

        private void LoadServices()
        {
            var services = _offeringsService.GetServices();
            comboBoxService.DataSource = services;
            comboBoxService.DisplayMember = "ServiceName"; // Предположим, что у услуги есть свойство Name
            comboBoxService.ValueMember = "Id"; // Идентификатор услуги
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            LoadClients();
            LoadServices();
        }

        private void saveAppoinmentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var appointment = new Appointment
                {
                    CustomerId = (Guid)comboBoxClient.SelectedValue,
                    ServiceId = (Guid)comboBoxService.SelectedValue,
                    EmployeeId = _employeeId,
                    Date = DateTime.Now,
                    StartTime = dateTimePickerStartDate.Value.Date + dateTimePickerStartTime.Value.TimeOfDay,
                    EndTime = dateTimePickerEndDate.Value.Date + dateTimePickerEndTime.Value.TimeOfDay,
                    Status = AppointmentStatus.Created
                };

                _employeeService.AddAppointment(appointment);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
        }
    }
}
