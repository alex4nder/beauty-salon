using BeautySalonApp.Forms;
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
                { clientFeedbackTab, LoadClientFeedbacksData },
                { clientsTab, LoadClientsData }
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
                rr.ReportDate,
                rr.ReportPeriodStartDate,
                rr.ReportPeriodEndDate,
                rr.TotalRevenue,
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
                feedback.FeedbackDate,
                ClientName = $"{feedback.ClientFirstName} {feedback.ClientLastName}",
                feedback.ClientEmail,
                feedback.Service,
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

        private void LoadClientsData()
        {
            var revenueReports = _clientService.GetClients();

            var revenueReportData = revenueReports.Select(client => new
            {
                client.Id,
                client.FirstName,
                client.LastName,
                client.Email,
                client.Phone,
                client.DateOfBirth,
                client.Notes
            }).ToList();

            dataGridViewClients.DataSource = revenueReportData;

            dataGridViewClients.Columns["FirstName"].HeaderText = "Имя";
            dataGridViewClients.Columns["LastName"].HeaderText = "Фамилия";
            dataGridViewClients.Columns["Email"].HeaderText = "Эл. почта";
            dataGridViewClients.Columns["Phone"].HeaderText = "Номер телефона";
            dataGridViewClients.Columns["DateOfBirth"].HeaderText = "Дата рождения";
            dataGridViewClients.Columns["Notes"].HeaderText = "Примечания";

            dataGridViewClients.Columns["Id"].Visible = false;

            DataGridViewLinkColumn emailColumn = new DataGridViewLinkColumn();
            emailColumn.HeaderText = "Эл. почта";
            emailColumn.DataPropertyName = "Email";
            emailColumn.Name = "Email";
            dataGridViewClients.Columns.Remove("Email");
            dataGridViewClients.Columns.Insert(3, emailColumn);

            dataGridViewClients.CellFormatting += DataGridViewClient_CellFormatting;

            if (!dataGridViewClients.Columns.Contains("Edit") && !dataGridViewClients.Columns.Contains("Delete"))
            {
                dataGridViewClients.CellContentClick += DataGridViewClient_CellContentClick;
            }

            if (!dataGridViewClients.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "Редактировать",
                    Text = "Редактировать",
                    UseColumnTextForButtonValue = true
                };

                dataGridViewClients.Columns.Add(editButtonColumn);
            }

            if (!dataGridViewClients.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "Удалить",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };

                dataGridViewClients.Columns.Add(deleteButtonColumn);
            }
        }

        private void EditClient(int clientId)
        {
            var client = _clientService.GetClientById(clientId);
            if (client != null)
            {
                using (ClientForm clientForm = new ClientForm(client))
                {
                    if (clientForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadClientsData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Клиент не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteClient(int clientId)
        {
            if (MessageBox.Show("Вы действительно хотите удалить клиента?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _clientService.ClientRemove(clientId);
                    LoadClientsData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenEmailClient(string email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = $"mailto:{email}",
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("У клиента отсутствует адрес электронной почты.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии почтового клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewClients.Rows[e.RowIndex];
                int clientId = Convert.ToInt32(row.Cells["Id"].Value);

                if (e.ColumnIndex == dataGridViewClients.Columns["Edit"].Index)
                {
                    EditClient(clientId);
                }
                else if (e.ColumnIndex == dataGridViewClients.Columns["Delete"].Index)
                {
                    DeleteClient(clientId);
                }
                else if (e.ColumnIndex == dataGridViewClients.Columns["Email"].Index)
                {
                    OpenEmailClient(row.Cells[e.ColumnIndex].Value.ToString());
                }
            }
        }

        private void DataGridViewClient_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewClients.Columns[e.ColumnIndex].Name == "Phone")
            {
                if (e.Value != null)
                {
                    string rawPhoneNumber = e.Value.ToString();

                    if (rawPhoneNumber.Length == 11 && rawPhoneNumber.StartsWith("8"))
                    {
                        string formattedPhoneNumber = $"+7 ({rawPhoneNumber.Substring(1, 3)}) {rawPhoneNumber.Substring(4, 3)}-{rawPhoneNumber.Substring(7, 2)}-{rawPhoneNumber.Substring(9, 2)}";
                        e.Value = formattedPhoneNumber;
                    }
                }
            }
        }

        private void addClientBtn_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm();
            if (clientForm.ShowDialog() == DialogResult.OK)
            {
                LoadClientsData();
            }
        }
    }
}
