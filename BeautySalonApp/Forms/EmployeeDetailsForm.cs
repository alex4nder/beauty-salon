using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeDetailsForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly Guid _employeeId;
        private readonly int _salonId;

        public EmployeeDetailsForm(Guid employeeId, int salonId)
        {
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _employeeId = employeeId;
            _salonId = salonId;

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
                ClientName = $"{appointment.Customer.FirstName} {appointment.Customer.LastName}",
                appointment.Service.Title,
                appointment.Date,
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

            if (!appointmentsDataGridView.Columns.Contains("Close") && !appointmentsDataGridView.Columns.Contains("Cancel"))
            {
                appointmentsDataGridView.CellContentClick -= DataGridViewAppoinments_CellContentClick;
                appointmentsDataGridView.CellContentClick += DataGridViewAppoinments_CellContentClick;
            }

            if (!appointmentsDataGridView.Columns.Contains("Close") && !appointmentsDataGridView.Columns.Contains("Cancel"))
            {
                DataGridViewButtonColumn closeAppointmentButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Close",
                    HeaderText = "",
                    Text = "Завершить",
                    UseColumnTextForButtonValue = true
                };

                DataGridViewButtonColumn cancelAppointmentButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Cancel",
                    HeaderText = "",
                    Text = "Отменить",
                    UseColumnTextForButtonValue = true
                };

                appointmentsDataGridView.Columns.Add(closeAppointmentButtonColumn);
                appointmentsDataGridView.Columns.Add(cancelAppointmentButtonColumn);
            }
        }

        private void DataGridViewAppoinments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = appointmentsDataGridView.Rows[e.RowIndex];
                Guid appointmentId = Guid.Parse(row.Cells["Id"].Value.ToString());

                if (e.ColumnIndex == appointmentsDataGridView.Columns["Close"].Index)
                {
                    UpdateAppointmentStatus(appointmentId, AppointmentStatus.Success);
                }
                else if (e.ColumnIndex == appointmentsDataGridView.Columns["Cancel"].Index)
                {
                    UpdateAppointmentStatus(appointmentId, AppointmentStatus.Cancelled);
                }
            }
        }

        private void UpdateAppointmentStatus(Guid appointmentId, AppointmentStatus newStatus)
        {
            try
            {
                _employeeService.UpdateAppointmentStatus(appointmentId, newStatus);
                MessageBox.Show("Статус записи успешно обновлён.");
                LoadAppointmentsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }

        private void addAppointmentBtn_Click(object sender, EventArgs e)
        {
            AppointmentForm appointmentForm = new AppointmentForm(_employeeId);
            if (appointmentForm.ShowDialog() == DialogResult.OK)
            {
                LoadAppointmentsData();
            }
        }
    }
}
