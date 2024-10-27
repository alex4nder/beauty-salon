using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeDetailsForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeePerformanceService _employeePerformanceService;
        private readonly int _employeeId;
        private readonly int _salonId;

        public EmployeeDetailsForm(int employeeId, int salonId)
        {
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _employeePerformanceService = Program.ServiceProvider.GetRequiredService<EmployeePerformanceService>();
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
                { performanceTab, LoadEmployeePerformanceData }
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
                int appointmentId = Convert.ToInt32(row.Cells["Id"].Value);

                if (e.ColumnIndex == appointmentsDataGridView.Columns["Close"].Index)
                {
                    UpdateAppointmentStatus(appointmentId, "завершено");
                }
                else if (e.ColumnIndex == appointmentsDataGridView.Columns["Cancel"].Index)
                {
                    UpdateAppointmentStatus(appointmentId, "отменено");
                }
            }
        }

        private void UpdateAppointmentStatus(int appointmentId, string newStatus)
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

        private void LoadEmployeePerformanceData()
        {
            try
            {
                List<EmployeePerformance> performances = _employeePerformanceService.GetEmployeePerformanceReports(_salonId, _employeeId);

                dataGridViewEmPerformance.DataSource = performances.Select(ep => new
                {
                    SalonId = ep.SalonId,
                    EvaluationDate = ep.EvaluationDate.ToShortDateString(),
                    TotalAppointments = ep.TotalAppointments,
                    TotalRevenue = ep.TotalRevenue
                }).ToList();

                dataGridViewEmPerformance.Columns["SalonId"].Visible = false;

                dataGridViewEmPerformance.Columns["EvaluationDate"].HeaderText = "Дата проведения оценки";
                dataGridViewEmPerformance.Columns["TotalAppointments"].HeaderText = "Всего оказано услуг";
                dataGridViewEmPerformance.Columns["TotalRevenue"].HeaderText = "Всего выручки";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }
    }
}
