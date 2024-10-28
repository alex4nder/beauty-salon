using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Employee = BeautySalonApp.Models.Employee;

namespace BeautySalonApp.Forms
{
    public partial class EmployeeForm : Form
    {
        private readonly EmployeeService _employeeService;

        private Employee _employee;
        private bool _isEditMode;

        public EmployeeForm(Employee? employee = null)
        {
            InitializeComponent();

            _employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();

            if (employee != null)
            {
                _employee = employee;
                _isEditMode = true;
                PreFillEmployeeData();
            }
            else
            {
                _employee = new Employee
                {
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Position = "",
                };
                _isEditMode = false;
            }
        }

        private void PreFillEmployeeData()
        {
            employeeFirstNameTextBox.Text = _employee.FirstName;
            employeeLastNameTextBox.Text = _employee.LastName;
            employeePhoneTextBox.Text = _employee.Phone;
            employeePositionTextBox.Text = _employee.Position;
        }

        private void saveEmployeeBtn_Click(object sender, EventArgs e)
        {
            _employee.FirstName = employeeFirstNameTextBox.Text;
            _employee.LastName = employeeLastNameTextBox.Text;
            _employee.Phone = employeePhoneTextBox.Text;
            _employee.Position = employeePositionTextBox.Text;

            if (_isEditMode)
            {
                _employeeService.EmployeeEdit(_employee);
            }
            else
            {
                _employeeService.EmployeeAdd(_employee);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
