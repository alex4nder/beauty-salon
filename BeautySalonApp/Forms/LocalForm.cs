using BeautySalonApp.Forms;
using BeautySalonApp.Forms.EntityActions;
using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;

namespace BeautySalonApp
{
    public partial class SalonForm : Form
    {
        private readonly RevenueReportService _revenueReportService;
        private readonly ClientFeedbackService _clientFeedbackService;
        private readonly ClientService _clientService;
        private readonly EmployeeService _employeeService;
        private readonly ManagerService _managerService;
        private readonly OfferingsService _offeringsService;
        private int _salonId;

        public SalonForm()
        {
            _revenueReportService = Program.ServiceProvider.GetRequiredService<RevenueReportService>();
            _clientFeedbackService = Program.ServiceProvider.GetRequiredService<ClientFeedbackService>();
            _clientService = Program.ServiceProvider.GetRequiredService<ClientService>();
            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();
            _managerService = Program.ServiceProvider.GetRequiredService<ManagerService>();
            _offeringsService = Program.ServiceProvider.GetRequiredService<OfferingsService>();
            InitializeComponent();

            // To force the loading of data for the first tab on form load
            employeesTab_SelectedIndexChanged(employeesTab, EventArgs.Empty);
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
                { managersTab, LoadManagersData },
                { servicesTab, LoadServicesData }
            };

            if (tabLoadActions.TryGetValue(employeesTab.SelectedTab, out var loadAction))
            {
                loadAction();
            }
        }

        private void LoadServicesData()
        {
            var services = _offeringsService.GetServices();

            var serviceData = services.Select(service => new
            {
                service.Id,
                service.ServiceName,
                service.Description,
                service.Price,
                service.Duration,
                IsPopular = service.IsPopular ? "Горячий спрос" : "Не пользуется спросом"
            }).ToList();

            dataGridViewServices.DataSource = serviceData;

            dataGridViewServices.Columns["ServiceName"].HeaderText = "Название услуги";
            dataGridViewServices.Columns["Description"].HeaderText = "Описание";
            dataGridViewServices.Columns["Price"].HeaderText = "Цена";
            dataGridViewServices.Columns["Duration"].HeaderText = "Продолжительность (мин)";
            dataGridViewServices.Columns["IsPopular"].HeaderText = "Популярность";

            dataGridViewServices.Columns["Id"].Visible = false;

            dataGridViewServices.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dataGridViewServices.Columns["Price"].Index && e.Value is decimal price)
                {
                    var rubleCulture = new CultureInfo("ru-RU");
                    e.Value = string.Format(rubleCulture, "{0:C}", price);
                    e.FormattingApplied = true;
                }
            };

            addActionColumns(dataGridViewServices, (sender, e) => DataGridViewServices_CellContentClick(sender, e));
        }

        private void EditService(int serviceId)
        {
            new EntityOperationBuilder<Service>()
                .WithFormCreator(s => new ServiceForm(s))
                .WithUpdateAction(s => _offeringsService.ServiceEdit(s))
                .WithLoadData(LoadServicesData)
                .ExecuteEdit(_offeringsService.GetServiceById(serviceId));
        }

        private void DeleteService(int serviceId)
        {
            new EntityOperationBuilder<Service>()
                   .WithRemoveAction(_offeringsService.ServiceRemove)
                   .WithLoadData(LoadServicesData)
                   .ExecuteDelete(serviceId);
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
            new EntityOperationBuilder<Models.Client>()
                   .WithFormCreator(c => new ClientForm(c))
                   .WithUpdateAction(c => _clientService.ClientEdit(c))
                   .WithLoadData(LoadClientsData)
                   .ExecuteEdit(_clientService.GetClientById(clientId));
        }

        private void DeleteClient(int clientId)
        {
            new EntityOperationBuilder<Models.Client>()
                    .WithRemoveAction(_clientService.ClientRemove)
                    .WithLoadData(LoadClientsData)
                    .ExecuteDelete(clientId);
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
            new EntityOperationBuilder<Employee>()
                .WithFormCreator(e => new EmployeeForm(e))
                .WithUpdateAction(e => _employeeService.EmployeeEdit(e))
                .WithLoadData(LoadEmployeesData)
                .ExecuteEdit(_employeeService.GetEmployeeById(employeeId));
        }

        private void DeleteEmployee(int employeeId)
        {
            new EntityOperationBuilder<Employee>()
                  .WithRemoveAction(_employeeService.EmployeeRemove)
                  .WithLoadData(LoadEmployeesData)
                  .ExecuteDelete(employeeId);
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
            new EntityOperationBuilder<Manager>()
                .WithFormCreator(m => new ManagerForm(_salonId, manager))
                .WithUpdateAction(m => _managerService.ManagerEdit(m))
                .WithLoadData(LoadManagersData)
                .ExecuteEdit(manager);
        }

        private void DeleteManager(int managerId)
        {
            new EntityOperationBuilder<Manager>()
                  .WithRemoveAction(_managerService.ManagerRemove)
                  .WithLoadData(LoadManagersData)
                  .ExecuteDelete(managerId);
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
        private void DataGridViewServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewServices.Rows[e.RowIndex];
                int serviceId = Convert.ToInt32(row.Cells["Id"].Value);

                if (e.ColumnIndex == dataGridViewServices.Columns["Edit"].Index)
                {
                    EditService(serviceId);
                }
                else if (e.ColumnIndex == dataGridViewServices.Columns["Delete"].Index)
                {
                    DeleteService(serviceId);
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmployees.Rows[e.RowIndex];
                int employeeId = Convert.ToInt32(row.Cells["Id"].Value);
                EmployeeDetailsForm employeeDetailsForm = new EmployeeDetailsForm(employeeId);
                employeeDetailsForm.ShowDialog();
            }
        }

        private void addServiceBtn_Click(object sender, EventArgs e)
        {
            ServiceForm serviceForm = new ServiceForm();
            if (serviceForm.ShowDialog() == DialogResult.OK)
            {
                LoadServicesData();
            }
        }
    }
}
