using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace BeautySalonApp
{
    public partial class SalonForm : Form
    {
        private readonly RevenueReportService _revenueReportService;
        private readonly ClientFeedbackService _clientFeedbackService;
        private readonly ClientService _clientService;

        private int _salonId;

        public SalonForm()
        {
            _revenueReportService = Program.ServiceProvider.GetRequiredService<RevenueReportService>();
            _clientFeedbackService = Program.ServiceProvider.GetRequiredService<ClientFeedbackService>();
            _clientService = Program.ServiceProvider.GetRequiredService<ClientService>();

            InitializeComponent();
        }

        public void SetSalonId(int salonId)
        {
            _salonId = salonId;
        }

        private void employeesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabLoadActions = new Dictionary<TabPage, Action>
            {
                { reportsTab, LoadRevenueReportsData },
                { clientFeedbackTab, LoadClientFeedbacksData }
            };

            if (tabLoadActions.TryGetValue(employeesTab.SelectedTab, out var loadAction))
            {
                loadAction();
            }
        }

        private void LoadRevenueReportsData()
        {
            var revenueReports = _revenueReportService.GetReportsWithDetails(_salonId);

            var revenueReportData = revenueReports.Select(rr => new
            {
                ReportDate = rr.ReportDate,
                ReportPeriodStartDate = rr.ReportPeriodStartDate,
                ReportPeriodEndDate = rr.ReportPeriodEndDate,
                TotalRevenue = rr.TotalRevenue,
                NumberOfClients = rr.TotalCustomers,
                MostPopularService = $"{rr.MostPopularService}"
            }).ToList();

            dataGridViewRevenueReports.DataSource = revenueReportData;

            dataGridViewRevenueReports.Columns["ReportDate"].HeaderText = "Дата/время отчета";
            dataGridViewRevenueReports.Columns["ReportPeriodStartDate"].HeaderText = "От";
            dataGridViewRevenueReports.Columns["ReportPeriodEndDate"].HeaderText = "До";
            dataGridViewRevenueReports.Columns["TotalRevenue"].HeaderText = "Общая выручка";
            dataGridViewRevenueReports.Columns["NumberOfClients"].HeaderText = "Количество клиентов";
            dataGridViewRevenueReports.Columns["MostPopularService"].HeaderText = "Самая популярная услуга";
        }

        private void generateRevenueReportBtn_Click(object sender, EventArgs e)
        {
            DateTime startDate = revenueReportDateFrom.Value;
            DateTime endDate = revenueReportDateTo.Value;

            _revenueReportService.GenerateAndSaveRevenueReport(startDate, endDate, _salonId);

            LoadRevenueReportsData();
        }

        private void LoadClientFeedbacksData()
        {
            var feedbacks = _clientFeedbackService.GetClientFeedbacks(_salonId);

            Dictionary<int, string> emojiRatings = new Dictionary<int, string>
            {
               { 1, "⭐" },
               { 2, "⭐⭐" },
               { 3, "⭐⭐⭐" },
               { 4, "⭐⭐⭐⭐" },
               { 5, "⭐⭐⭐⭐⭐" }
            };

            var feedbackData = feedbacks.Select(feedback => new
            {
                FeedbackDate = feedback.FeedbackDate,
                ClientName = $"{feedback.ClientFirstName} {feedback.ClientLastName}",
                ClientEmail = feedback.ClientEmail,
                Service = feedback.Service,
                Rating = $"{feedback.Rating} {emojiRatings[feedback.Rating]}",
                Comment = feedback.Comments,
            }).ToList();

            dataGridViewClientFeedback.DataSource = feedbackData;

            dataGridViewClientFeedback.Columns["FeedbackDate"].HeaderText = "Дата отзыва";
            dataGridViewClientFeedback.Columns["ClientName"].HeaderText = "Имя клиента";
            dataGridViewClientFeedback.Columns["ClientEmail"].HeaderText = "Эл. почта клиента";
            dataGridViewClientFeedback.Columns["Service"].HeaderText = "Услуга";
            dataGridViewClientFeedback.Columns["Rating"].HeaderText = "Оценка";
            dataGridViewClientFeedback.Columns["Comment"].HeaderText = "Комментарий";

            dataGridViewClientFeedback.CellFormatting += DataGridViewClientFeedback_CellFormatting;

            DataGridViewLinkColumn emailColumn = new DataGridViewLinkColumn();
            emailColumn.HeaderText = "Эл. почта клиента";
            emailColumn.DataPropertyName = "ClientEmail";
            emailColumn.Name = "ClientEmail";
            dataGridViewClientFeedback.Columns.Remove("ClientEmail");
            dataGridViewClientFeedback.Columns.Insert(2, emailColumn);

            dataGridViewClientFeedback.CellContentClick += DataGridViewClientFeedback_CellContentClick;
        }

        private void DataGridViewClientFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewClientFeedback.Columns["ClientEmail"].Index && e.RowIndex >= 0)
            {
                string email = dataGridViewClientFeedback.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                try
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = $"mailto:{email}",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии почтового клиента: {ex.Message}");
                }
            }
        }

        private void DataGridViewClientFeedback_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewClientFeedback.Columns[e.ColumnIndex].Name == "Rating" && e.Value != null)
            {
                string ratingText = e.Value.ToString();
                int ratingValue;

                if (int.TryParse(ratingText.Split(' ')[0], out ratingValue))
                {
                    switch (ratingValue)
                    {
                        case 1:
                            e.CellStyle.ForeColor = Color.Red;
                            break;
                        case 2:
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            break;
                        case 3:
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            break;
                        case 4:
                            e.CellStyle.ForeColor = Color.DarkGreen;
                            break;
                        case 5:
                            e.CellStyle.ForeColor = Color.Green;
                            break;
                    }
                }
            }
        }
    }
}
