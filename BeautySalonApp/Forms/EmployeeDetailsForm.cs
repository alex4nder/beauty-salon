using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeDetailsForm : Form
    {
        private readonly EmployeeService _employeeService;
        private int _employeeId;

        public EmployeeDetailsForm(int employeeId)
        {
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _employeeId = employeeId;
            InitializeComponent();

            employeeDetailsTabControl_SelectedIndexChanged(employeeDetailsTabControl, EventArgs.Empty);
        }

        private void employeeDetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabLoadActions = new Dictionary<TabPage, Action>
            {
                { appointmentsTab, LoadAppointmentsData },
            };

            if (tabLoadActions.TryGetValue(employeeDetailsTabControl.SelectedTab, out var loadAction))
            {
                loadAction();
            }
        }

        private void LoadAppointmentsData()
        {
            var appointments = _employeeService.GetAppointments(_employeeId);

            var appointmentsData = appointments.Select(appointment => new
            {
                appointment.Id,
                ClientName = $"{appointment.Client.FirstName} {appointment.Client.LastName}",
                appointment.Service.ServiceName,
                appointment.AppointmentDate,
                appointment.StartTime,
                appointment.EndTime,
                appointment.Status,
            }).ToList();

            appointmentsDataGridView.DataSource = appointmentsData;

            appointmentsDataGridView.Columns["ClientName"].HeaderText = "Клиент";
            appointmentsDataGridView.Columns["ServiceName"].HeaderText = "Процедура";
            appointmentsDataGridView.Columns["AppointmentDate"].HeaderText = "Дата записи";
            appointmentsDataGridView.Columns["StartTime"].HeaderText = "Дата начала процедуры";
            appointmentsDataGridView.Columns["EndTime"].HeaderText = "Дата окончания процедуры";
            appointmentsDataGridView.Columns["Status"].HeaderText = "Статус";

            appointmentsDataGridView.Columns["Id"].Visible = false;

            //addActionColumns(dataGridViewClients, (sender, e) => DataGridViewClient_CellContentClick(sender, e));
        }

        //private void EditClient(int clientId)
        //{
        //    new EntityOperationBuilder<Models.Client>()
        //           .WithFormCreator(c => new ClientForm(c))
        //           .WithUpdateAction(c => _clientService.ClientEdit(c))
        //           .WithLoadData(LoadClientsData)
        //           .ExecuteEdit(_clientService.GetClientById(clientId));
        //}

        //private void DeleteClient(int clientId)
        //{
        //    new EntityOperationBuilder<Models.Client>()
        //            .WithRemoveAction(_clientService.ClientRemove)
        //            .WithLoadData(LoadClientsData)
        //            .ExecuteDelete(clientId);
        //}
    }
}
