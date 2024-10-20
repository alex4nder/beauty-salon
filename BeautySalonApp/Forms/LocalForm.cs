using BeautySalonApp.Services;

namespace BeautySalonApp
{
    public partial class SalonForm : Form
    {
        private readonly RevenueReportService _revenueReportService;
        private int _salonId;

        public SalonForm(SalonService salonService, RevenueReportService revenueReportService)
        {
            _revenueReportService = revenueReportService;

            InitializeComponent();
        }

        public void SetSalonId(int salonId)
        {
            _salonId = salonId;
        }

        private void employeesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeesTab.SelectedTab == reportsTab)
            {
                LoadRevenueReportsData();
            }
        }

        private void LoadRevenueReportsData()
        {
            var revenueReports = _revenueReportService.GetReportsWithDetails(_salonId);

            var revenueReportData = revenueReports.Select(rr => new
            {
                Date = rr.ReportDate,
                TotalRevenue = rr.TotalRevenue,
                NumberOfClients = rr.TotalCustomers,
                MostPopularService = $"{rr.MostPopularService}"
            }).ToList();

            dataGridViewRevenueReports.DataSource = revenueReportData;

            dataGridViewRevenueReports.Columns["Date"].HeaderText = "Дата/время отчета";
            dataGridViewRevenueReports.Columns["TotalRevenue"].HeaderText = "Общая выручка";
            dataGridViewRevenueReports.Columns["NumberOfClients"].HeaderText = "Количество клиентов";
            dataGridViewRevenueReports.Columns["MostPopularService"].HeaderText = "Самый популярный сервис";
        }
    }
}
