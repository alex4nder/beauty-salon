using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeDetailsForm : Form
    {
        private readonly EmployeePerformanceService _employeePerformanceService;
        private readonly int _employeeId;
        public EmployeeDetailsForm(int employeeId)
        {
            _employeePerformanceService = Program.ServiceProvider.GetRequiredService<EmployeePerformanceService>();
            _employeeId = employeeId;

            InitializeComponent();
        }

        private void EmployeeDetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabLoadActions = new Dictionary<TabPage, Action>
            {
                { performanceTab, LoadEmployeePerformanceData },
            };

            if (tabLoadActions.TryGetValue(EmployeeDetailsTabControl.SelectedTab, out var loadAction))
            {
                loadAction();
            }
        }

        private void LoadEmployeePerformanceData()
        {
            try
            {
                // FIXME
                int salonId = 1;
                List<EmployeePerformance> performances = _employeePerformanceService.GetEmployeePerformanceReports(salonId, _employeeId);

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
