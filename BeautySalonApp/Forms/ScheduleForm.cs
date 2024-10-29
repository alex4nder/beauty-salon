using BeautySalonApp.Exceptions;
using BeautySalonApp.Models;  // Assume Schedule model is in Models namespace
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class ScheduleForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private readonly EmployeeService _employeeService;

        private Schedule _schedule;
        private Employee _employee;
        private bool _isEditMode;

        public ScheduleForm(Guid employeeId, Schedule? schedule = null)
        {
            InitializeComponent();

            _scheduleService = Program.ServiceProvider.GetRequiredService<ScheduleService>();
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();

            if (schedule != null)
            {
                _schedule = schedule;
                _employee = _employeeService.GetEmployeeById(_schedule.EmployeeId);
                _isEditMode = true;
                SetFormTitle();
                PreFillScheduleData();
            }
            else
            {
                _schedule = new Schedule
                {
                    EmployeeId = employeeId,
                    Date = DateTime.Today,
                    StartTime = TimeSpan.Zero,
                    EndTime = TimeSpan.Zero
                };
                _isEditMode = false;
            }
        }

        private void PreFillScheduleData()
        {
            scheduleDatePicker.Value = _schedule.Date;
            startTimePicker.Value = DateTime.Today.Add(_schedule.StartTime);
            endTimePicker.Value = DateTime.Today.Add(_schedule.EndTime);
        }

        private void SetFormTitle()
        {
            if (_isEditMode)
            {
                this.Text = $"Рабочий график сотрудника: {_employee.FirstName} {_employee.LastName}";

            }
        }

        private void scheduleSaveBtn_Click(object sender, EventArgs e)
        {
            _schedule.Date = scheduleDatePicker.Value.Date;
            _schedule.StartTime = startTimePicker.Value.TimeOfDay;
            _schedule.EndTime = endTimePicker.Value.TimeOfDay;

            try
            {
                if (_isEditMode)
                {
                    _scheduleService.UpdateSchedule(_schedule);
                }
                else
                {
                    _scheduleService.AddSchedule(_schedule);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ScheduleDateConflictException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка. Пожалуйста, попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally log the exception message or stack trace for debugging
                Console.WriteLine(ex.Message);
            }

        }
    }
}
