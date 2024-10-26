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
        private readonly EmployeeService _employeeService;
        private readonly ManagerService _managerService;

        private int _salonId;

        public SalonForm()
        {
            _revenueReportService = Program.ServiceProvider.GetRequiredService<RevenueReportService>();
            _clientFeedbackService = Program.ServiceProvider.GetRequiredService<ClientFeedbackService>();
            _clientService = Program.ServiceProvider.GetRequiredService<ClientService>();
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _managerService = Program.ServiceProvider.GetRequiredService<ManagerService>();

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
                { clientsTab, LoadClientsData },
                { employeeTab, LoadEmployeesData },
                { managersTab, LoadManagersData }
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

            addActionColumns(dataGridViewClients, (sender, e) => DataGridViewClient_CellContentClick(sender, e));
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

        private void LoadEmployeesData()
        {
            var employees = _employeeService.GetEmployees();

            var employeesData = employees.Select(employee => new
            {
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.Position,
                employee.Phone,
                employee.WorkBookNumber,
            }).ToList();

            dataGridViewEmployees.DataSource = employeesData;

            dataGridViewEmployees.Columns["FirstName"].HeaderText = "Имя";
            dataGridViewEmployees.Columns["LastName"].HeaderText = "Фамилия";
            dataGridViewEmployees.Columns["Position"].HeaderText = "Должность";
            dataGridViewEmployees.Columns["Phone"].HeaderText = "Номер телефона";
            dataGridViewEmployees.Columns["WorkBookNumber"].HeaderText = "Номер трудовой книжки";

            dataGridViewEmployees.Columns["Id"].Visible = false;

            dataGridViewEmployees.CellFormatting += DataGridViewEmployee_CellFormatting;

            addActionColumns(dataGridViewEmployees, (sender, e) => DataGridViewEmployee_CellContentClick(sender, e));
        }

        private void EditEmployee(int employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee != null)
            {
                using (EmployeeForm employeeForm = new EmployeeForm(employee))
                {
                    if (employeeForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadEmployeesData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Сотрудник не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            if (MessageBox.Show("Вы действительно хотите удалить сотрудника?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _employeeService.EmployeeRemove(employeeId);
                    LoadEmployeesData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadManagersData()
        {
            var managers = _managerService.GetManagers(_salonId);

            var managersData = managers.Select(manager => new
            {
                manager.Id,
                manager.FirstName,
                manager.LastName,
                manager.Email,
                manager.Phone,
            }).ToList();

            dataGridViewManagers.DataSource = managersData;

            dataGridViewManagers.Columns["FirstName"].HeaderText = "Имя";
            dataGridViewManagers.Columns["LastName"].HeaderText = "Фамилия";
            dataGridViewManagers.Columns["Email"].HeaderText = "Адрес эл. почты";
            dataGridViewManagers.Columns["Phone"].HeaderText = "Номер телефона";

            dataGridViewManagers.Columns["Id"].Visible = false;

            DataGridViewLinkColumn emailColumn = new DataGridViewLinkColumn();
            emailColumn.HeaderText = "Эл. почта";
            emailColumn.DataPropertyName = "Email";
            emailColumn.Name = "Email";
            dataGridViewManagers.Columns.Remove("Email");
            dataGridViewManagers.Columns.Insert(3, emailColumn);

            addActionColumns(dataGridViewManagers, (sender, e) => DataGridViewManager_CellContentClick(sender, e));
        }

        private void EditManager(int managerId)
        {
            var manager = _managerService.GetManagerById(managerId);
            if (manager != null)
            {
                using (ManagerForm managerForm = new ManagerForm(_salonId, manager))
                {
                    if (managerForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadManagersData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Менеджер не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteManager(int managerId)
        {
            if (MessageBox.Show("Вы действительно хотите удалить менеджера?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _managerService.ManagerRemove(managerId);
                    LoadManagersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении менеджера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void generateRevenueReportBtn_Click(object sender, EventArgs e)
        {
            DateTime startDate = revenueReportDateFrom.Value;
            DateTime endDate = revenueReportDateTo.Value;

            _revenueReportService.GenerateAndSaveRevenueReport(startDate, endDate, _salonId);

            LoadRevenueReportsData();
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

        private void DataGridViewClient_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewClients.Columns[e.ColumnIndex].Name == "Phone")
            {
                formatPhoneNumber(e);
            }
        }

        private void DataGridViewEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewEmployees.Columns[e.ColumnIndex].Name == "Phone")
            {
                formatPhoneNumber(e);
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

        private void DataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmployees.Rows[e.RowIndex];
                int employeeId = Convert.ToInt32(row.Cells["Id"].Value);

                if (e.ColumnIndex == dataGridViewEmployees.Columns["Edit"].Index)
                {
                    EditEmployee(employeeId);
                }
                else if (e.ColumnIndex == dataGridViewEmployees.Columns["Delete"].Index)
                {
                    DeleteEmployee(employeeId);
                }
            }
        }

        private void DataGridViewManager_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewManagers.Rows[e.RowIndex];
                int managerId = Convert.ToInt32(row.Cells["Id"].Value);

                if (e.ColumnIndex == dataGridViewManagers.Columns["Edit"].Index)
                {
                    EditManager(managerId);
                }
                else if (e.ColumnIndex == dataGridViewManagers.Columns["Delete"].Index)
                {
                    DeleteManager(managerId);
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

        private void formatPhoneNumber(DataGridViewCellFormattingEventArgs e)
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

        private void addActionColumns(DataGridView dataGridView, Action<object, DataGridViewCellEventArgs> cellClickHandler)
        {
            if (!dataGridView.Columns.Contains("Edit") && !dataGridView.Columns.Contains("Delete"))
            {
                dataGridView.CellContentClick -= DataGridView_CellContentClickWrapper;
                dataGridView.CellContentClick += DataGridView_CellContentClickWrapper;
            }

            if (!dataGridView.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Редактировать",
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(editButtonColumn);
            }

            if (!dataGridView.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(deleteButtonColumn);
            }

            void DataGridView_CellContentClickWrapper(object sender, DataGridViewCellEventArgs e)
            {
                cellClickHandler?.Invoke(sender, e);
            }
        }

        private void addEmployeeBtn_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            if (employeeForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeesData();
            }
        }

        private void addManagerBtn_Click(object sender, EventArgs e)
        {
            ManagerForm managerForm = new ManagerForm(_salonId);
            if (managerForm.ShowDialog() == DialogResult.OK)
            {
                LoadManagersData();
            }
        }

        private void dataGridViewEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeDetailsForm employeeDetailsForm = new EmployeeDetailsForm();
            employeeDetailsForm.ShowDialog();
        }
    }
}
