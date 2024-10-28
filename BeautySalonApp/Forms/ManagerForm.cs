using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Manager = BeautySalonApp.Models.Manager;


namespace BeautySalonApp.Forms
{
    public partial class ManagerForm : Form
    {
        private readonly ManagerService _managerService;

        private Manager _manager;
        private bool _isEditMode;
        private int _salonId;

        public ManagerForm(int salonId, Manager? manager = null)
        {
            InitializeComponent();

            _salonId = salonId;
            _managerService = Program.ServiceProvider.GetRequiredService<ManagerService>();

            if (manager != null)
            {
                _manager = manager;
                _isEditMode = true;
                PreFillManagerData();
            }
            else
            {
                _manager = new Manager
                {
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Email = "",
                    BranchId = _salonId,
                };
                _isEditMode = false;
            }
        }

        private void PreFillManagerData()
        {
            managerFirstNameTextBox.Text = _manager.FirstName;
            managerLastNameTextBox.Text = _manager.LastName;
            managerPhoneTextBox.Text = _manager.Phone;
            managerEmailTextBox.Text = _manager.Email;
        }

        private void saveManagerBtn_Click(object sender, EventArgs e)
        {
            _manager.FirstName = managerFirstNameTextBox.Text;
            _manager.LastName = managerLastNameTextBox.Text;
            _manager.Phone = managerPhoneTextBox.Text;
            _manager.Email = managerEmailTextBox.Text;
            _manager.BranchId = _salonId;

            if (_isEditMode)
            {
                _managerService.ManagerEdit(_manager);
            }
            else
            {
                _managerService.ManagerAdd(_manager);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
