using BeautySalonApp.Forms;
using BeautySalonApp.Forms.EntityActions;
using BeautySalonApp.Models;
using BeautySalonApp.Models.BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;

namespace BeautySalonApp
{
    public partial class SalonForm : Form
    {
        private readonly GlobalReportService _globalReportService;
        private readonly CustomerFeedbackService _customerFeedbackService;
        private readonly CustomerService _customerService;
        private readonly EmployeeService _employeeService;
        private readonly ManagerService _managerService;
        private OfferingsService _offeringsService;

        private int _branchId;

        public SalonForm()
        {
            var CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();

            _employeeService = new EmployeeService();
            _globalReportService = new GlobalReportService();
            _customerFeedbackService = new CustomerFeedbackService();
            _customerService = new CustomerService();
            _managerService = new ManagerService();
            _offeringsService = new OfferingsService();

            _branchId = CurrentBranchContext.BranchId;

            InitializeComponent();

            // To force the loading of data for the first tab on form load
            employeesTab_SelectedIndexChanged(employeesTab, EventArgs.Empty);
        }



        private void employeesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabLoadActions = new Dictionary<TabPage, Action>
            {
                { reportsTab, LoadReportsData },
                { CustomerFeedbackTab, LoadCustomerFeedbacksData },
                { CustomersTab, LoadCustomersData },
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
                IsPopular = service.IsPopular ? "Популярная услуга" : "Непопулярная услуга"
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

            ActionColumnBuilder.addActionColumns(dataGridViewServices, (sender, e) => DataGridViewServices_CellContentClick(sender, e));
        }

        private void EditService(Guid serviceId)
        {
            new EntityActionConfigurator<Service>()
                .WithFormCreator(s => new ServiceForm(s))
                .WithUpdateAction(s => _offeringsService.ServiceEdit(s))
                .WithLoadData(LoadServicesData)
                .ExecuteEdit(_offeringsService.GetServiceById(serviceId));
        }

        private void DeleteService(Guid serviceId)
        {
            new EntityActionConfigurator<Service>()
                   .WithRemoveAction(_offeringsService.ServiceRemove)
                   .WithLoadData(LoadServicesData)
                   .ExecuteDelete(serviceId);
        }

        private void LoadReportsData()
        {
            var globalReports = _globalReportService.GetReportsWithDetails(_branchId);

            var globalReportData = globalReports.Select(rr => new
            {
                rr.ReportDate,
                rr.StartDate,
                rr.EndDate,
                rr.TotalIncome,
                NumberOfCustomers = rr.ClientsServed,
            }).ToList();

            dataGridViewRevenueReports.DataSource = globalReportData;

            dataGridViewRevenueReports.Columns["ReportDate"].HeaderText = "Дата/время отчета";
            dataGridViewRevenueReports.Columns["StartDate"].HeaderText = "От";
            dataGridViewRevenueReports.Columns["EndDate"].HeaderText = "До";
            dataGridViewRevenueReports.Columns["TotalIncome"].HeaderText = "Общая выручка";
            dataGridViewRevenueReports.Columns["NumberOfCustomers"].HeaderText = "Количество клиентов";
        }

        private void LoadCustomerFeedbacksData()
        {
            var feedbacks = _customerFeedbackService.GetCustomerFeedbacks();

            Dictionary<int, string> emojiRatings = new Dictionary<int, string>
            {
               { 1, "😖" },
               { 2, "😒" },
               { 3, "👌" },
               { 4, "😊" },
               { 5, "😍" }
            };

            var feedbackData = feedbacks.Select(feedback => new
            {
                feedback.FeedbackDate,
                CustomerName = $"{feedback.CustomerFirstName} {feedback.CustomerLastName}",
                feedback.CustomerEmail,
                Rating = $"{feedback.Rating} {emojiRatings[feedback.Rating]}",
                Comment = feedback.Comments,
            }).ToList();

            dataGridViewCustomerFeedback.DataSource = feedbackData;

            dataGridViewCustomerFeedback.Columns["FeedbackDate"].HeaderText = "Дата отзыва";
            dataGridViewCustomerFeedback.Columns["CustomerName"].HeaderText = "Имя клиента";
            dataGridViewCustomerFeedback.Columns["CustomerEmail"].HeaderText = "Эл. почта клиента";
            dataGridViewCustomerFeedback.Columns["Rating"].HeaderText = "Оценка";
            dataGridViewCustomerFeedback.Columns["Comment"].HeaderText = "Комментарий";

            dataGridViewCustomerFeedback.CellFormatting += DataGridViewCustomerFeedback_CellFormatting;

            DataGridViewLinkColumn emailColumn = new DataGridViewLinkColumn();
            emailColumn.HeaderText = "Эл. почта клиента";
            emailColumn.DataPropertyName = "CustomerEmail";
            emailColumn.Name = "CustomerEmail";
            dataGridViewCustomerFeedback.Columns.Remove("CustomerEmail");
            dataGridViewCustomerFeedback.Columns.Insert(2, emailColumn);

            dataGridViewCustomerFeedback.CellContentClick += DataGridViewCustomerFeedback_CellContentClick;
        }

        private void LoadCustomersData()
        {
            var globalReports = _customerService.GetCustomers();

            var globalReportData = globalReports.Select(Customer => new
            {
                Customer.Id,
                Customer.FirstName,
                Customer.LastName,
                Customer.Email,
                Customer.Phone,
                Customer.Birthday,
            }).ToList();

            dataGridViewCustomers.DataSource = globalReportData;

            dataGridViewCustomers.Columns["FirstName"].HeaderText = "Имя";
            dataGridViewCustomers.Columns["LastName"].HeaderText = "Фамилия";
            dataGridViewCustomers.Columns["Email"].HeaderText = "Эл. почта";
            dataGridViewCustomers.Columns["Phone"].HeaderText = "Номер телефона";
            dataGridViewCustomers.Columns["Birthday"].HeaderText = "Дата рождения";

            dataGridViewCustomers.Columns["Id"].Visible = false;

            DataGridViewLinkColumn emailColumn = new DataGridViewLinkColumn();
            emailColumn.HeaderText = "Эл. почта";
            emailColumn.DataPropertyName = "Email";
            emailColumn.Name = "Email";
            dataGridViewCustomers.Columns.Remove("Email");
            dataGridViewCustomers.Columns.Insert(3, emailColumn);

            dataGridViewCustomers.CellFormatting += DataGridViewCustomer_CellFormatting;

            ActionColumnBuilder.addActionColumns(dataGridViewCustomers, (sender, e) => DataGridViewCustomer_CellContentClick(sender, e));
        }

        private void EditCustomer(Guid CustomerId)
        {
            new EntityActionConfigurator<Customer>()
                   .WithFormCreator(c => new CustomerForm(c))
                   .WithUpdateAction(c => _customerService.CustomerEdit(c))
                   .WithLoadData(LoadCustomersData)
                   .ExecuteEdit(_customerService.GetCustomerById(CustomerId));
        }

        private void DeleteCustomer(Guid CustomerId)
        {
            new EntityActionConfigurator<Customer>()
                    .WithRemoveAction(_customerService.CustomerRemove)
                    .WithLoadData(LoadCustomersData)
                    .ExecuteDelete(CustomerId);
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
            }).ToList();

            dataGridViewEmployees.DataSource = employeesData;

            dataGridViewEmployees.Columns["FirstName"].HeaderText = "Имя";
            dataGridViewEmployees.Columns["LastName"].HeaderText = "Фамилия";
            dataGridViewEmployees.Columns["Position"].HeaderText = "Должность";
            dataGridViewEmployees.Columns["Phone"].HeaderText = "Номер телефона";

            dataGridViewEmployees.Columns["Id"].Visible = false;

            dataGridViewEmployees.CellFormatting += DataGridViewEmployee_CellFormatting;

            if (!dataGridViewEmployees.Columns.Contains("Details"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Details",
                    HeaderText = "",
                    Text = "Детали",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewEmployees.Columns.Add(editButtonColumn);
            }

            ActionColumnBuilder.addActionColumns(dataGridViewEmployees, (sender, e) => DataGridViewEmployee_CellContentClick(sender, e));

            dataGridViewEmployees.Columns["Details"].Width = 100;
        }

        private void EditEmployee(Guid employeeId)
        {
            new EntityActionConfigurator<Employee>()
                .WithFormCreator(e => new EmployeeForm(e))
                .WithUpdateAction(e => _employeeService.EmployeeEdit(e))
                .WithLoadData(LoadEmployeesData)
                .ExecuteEdit(_employeeService.GetEmployeeById(employeeId));
        }

        private void DeleteEmployee(Guid employeeId)
        {
            new EntityActionConfigurator<Employee>()
                  .WithRemoveAction(_employeeService.EmployeeRemove)
                  .WithLoadData(LoadEmployeesData)
                  .ExecuteDelete(employeeId);
        }

        private void LoadManagersData()
        {
            var managers = _managerService.GetManagers(_branchId);

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

            ActionColumnBuilder.addActionColumns(dataGridViewManagers, (sender, e) => DataGridViewManager_CellContentClick(sender, e));
        }

        private void EditManager(Guid managerId)
        {
            var manager = _managerService.GetManagerById(managerId);
            new EntityActionConfigurator<Manager>()
                .WithFormCreator(m => new ManagerForm(_branchId, manager))
                .WithUpdateAction(m => _managerService.ManagerEdit(m))
                .WithLoadData(LoadManagersData)
                .ExecuteEdit(manager);
        }

        private void DeleteManager(Guid managerId)
        {
            new EntityActionConfigurator<Manager>()
                  .WithRemoveAction(_managerService.ManagerRemove)
                  .WithLoadData(LoadManagersData)
                  .ExecuteDelete(managerId);
        }

        private void generateRevenueReportBtn_Click(object sender, EventArgs e)
        {
            DateTime startDate = revenueReportDateFrom.Value;
            DateTime endDate = revenueReportDateTo.Value;

            _globalReportService.UpdateGlobalReports(_branchId, startDate, endDate);

            LoadReportsData();
        }

        private void DataGridViewCustomerFeedback_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewCustomerFeedback.Columns[e.ColumnIndex].Name == "Rating" && e.Value != null)
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

        private void DataGridViewCustomer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewCustomers.Columns[e.ColumnIndex].Name == "Phone")
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

        private void OpenEmailCustomer(string email)
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

        private void DataGridViewCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewCustomers.Rows[e.RowIndex];
                Guid CustomerId = Guid.Parse(row.Cells["Id"].Value.ToString());

                if (e.ColumnIndex == dataGridViewCustomers.Columns["Edit"].Index)
                {
                    EditCustomer(CustomerId);
                }
                else if (e.ColumnIndex == dataGridViewCustomers.Columns["Delete"].Index)
                {
                    DeleteCustomer(CustomerId);
                }
                else if (e.ColumnIndex == dataGridViewCustomers.Columns["Email"].Index)
                {
                    OpenEmailCustomer(row.Cells[e.ColumnIndex].Value.ToString());
                }
            }
        }

        private void DataGridViewCustomerFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewCustomerFeedback.Columns["CustomerEmail"].Index && e.RowIndex >= 0)
            {
                string email = dataGridViewCustomerFeedback.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

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
                Guid employeeId = Guid.Parse(row.Cells["Id"].Value.ToString());

                if (e.ColumnIndex == dataGridViewEmployees.Columns["Edit"].Index)
                {
                    EditEmployee(employeeId);
                }
                else if (e.ColumnIndex == dataGridViewEmployees.Columns["Delete"].Index)
                {
                    DeleteEmployee(employeeId);
                }
                else if (e.ColumnIndex == dataGridViewEmployees.Columns["Details"].Index)
                {
                    this.Hide();
                    using (EmployeeDetailsForm employeeDetailsForm = new EmployeeDetailsForm(employeeId))
                    {
                        employeeDetailsForm.ShowDialog();
                    }
                    this.Show();
                }
            }
        }

        private void DataGridViewManager_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewManagers.Rows[e.RowIndex];
                Guid managerId = Guid.Parse(row.Cells["Id"].Value.ToString());

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
                Guid serviceId = Guid.Parse(row.Cells["Id"].Value.ToString());

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

        private void addCustomerBtn_Click(object sender, EventArgs e)
        {
            CustomerForm CustomerForm = new CustomerForm();
            if (CustomerForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomersData();
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
            ManagerForm managerForm = new ManagerForm(_branchId);
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
                Guid employeeId = Guid.Parse(row.Cells["Id"].Value.ToString());

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
