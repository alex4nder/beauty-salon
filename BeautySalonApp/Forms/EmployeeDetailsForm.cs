using BeautySalonApp.Forms.EntityActions;
using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeDetailsForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly ScheduleService _scheduleService;
        private readonly Guid _employeeId;

        public EmployeeDetailsForm(Guid employeeId)
        {
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _scheduleService = Program.ServiceProvider.GetRequiredService<ScheduleService>();
            _employeeId = employeeId;

            InitializeComponent();

            employeeDetailsTabControl_SelectedIndexChanged(employeeDetailsTabControl, EventArgs.Empty);
        }

        private void employeeDetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabLoadActions = new Dictionary<TabPage, Action>
            {
                { appointmentsTab, LoadAppointmentsData },
                { scheduleTab, LoadScheduleData }
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
                Status = AppointmentStatusDictionary.Statuses[appointment.Status]
            }).ToList();

            appointmentsDataGridView.DataSource = appointmentsData;

            appointmentsDataGridView.Columns["ClientName"].HeaderText = "Клиент";
            appointmentsDataGridView.Columns["Title"].HeaderText = "Процедура";
            appointmentsDataGridView.Columns["Date"].HeaderText = "Дата записи";
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

        private void LoadScheduleData()
        {
            var scheduleData = _scheduleService.GetEmployeeSchedule(_employeeId)
                .Select(s => new
                {
                    s.Id,
                    Date = s.Date.ToString("dd.MM.yyyy"),
                    Day = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.Date.ToString("dddd", new CultureInfo("ru-RU"))),
                    StartTime = s.StartTime.ToString(@"hh\:mm"),
                    EndTime = s.EndTime.ToString(@"hh\:mm")
                })
                .ToList();

            dataGridViewEmSchedule.DataSource = scheduleData;

            dataGridViewEmSchedule.Columns["Date"].HeaderText = "Дата";
            dataGridViewEmSchedule.Columns["Day"].HeaderText = "День недели";
            dataGridViewEmSchedule.Columns["StartTime"].HeaderText = "Время начала";
            dataGridViewEmSchedule.Columns["EndTime"].HeaderText = "Время окончания";
            dataGridViewEmSchedule.Columns["Id"].Visible = false;

            ActionColumnBuilder.addActionColumns(dataGridViewEmSchedule, (sender, e) => dataGridViewEmSchedule_CellContentClick(sender, e));
        }
        private void dataGridViewEmSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmSchedule.Rows[e.RowIndex];
                Guid scheduleId = Guid.Parse(row.Cells["Id"].Value.ToString());

                if (e.ColumnIndex == dataGridViewEmSchedule.Columns["Edit"].Index)
                {
                    this.Hide();
                    new EntityActionConfigurator<Schedule>()
                        .WithFormCreator(schedule => new ScheduleForm(schedule.EmployeeId, schedule))
                        .WithUpdateAction(schedule => _scheduleService.UpdateSchedule(schedule))
                        .WithLoadData(LoadScheduleData)
                        .ExecuteEdit(_scheduleService.GetScheduleById(scheduleId));
                    this.Show();

                    return;
                }

                if (e.ColumnIndex == dataGridViewEmSchedule.Columns["Delete"].Index)
                {
                    new EntityActionConfigurator<Employee>()
                        .WithRemoveAction(_scheduleService.RemoveSchedule)
                        .WithLoadData(LoadScheduleData)
                        .ExecuteDelete(scheduleId);
                    return;
                }
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
                    UpdateAppointmentStatus(appointmentId, AppointmentStatusEnum.Success);
                }
                else if (e.ColumnIndex == appointmentsDataGridView.Columns["Cancel"].Index)
                {
                    UpdateAppointmentStatus(appointmentId, AppointmentStatusEnum.Cancelled);
                }
            }
        }

        private void UpdateAppointmentStatus(Guid appointmentId, string newStatus)
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

        private void addScheduleBtn_Click(object sender, EventArgs e)
        {
            ScheduleForm form = new ScheduleForm(_employeeId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadScheduleData();
            }
        }
    }
}
