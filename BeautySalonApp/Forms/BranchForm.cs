using BeautySalonApp.Models;

namespace BeautySalonApp.Forms
{
    public partial class BranchForm : Form
    {
        private Label labelName;
        private TextBox textBoxName;
        private Label labelAddress;
        private TextBox textBoxAddress;
        private Label labelPhone;
        private TextBox textBoxPhone;
        private Button buttonSave;

        private Branch _branch;
        private bool _isEditMode;

        public BranchForm(Branch? branch = null)
        {
            _branch = branch;
            InitializeComponent();


            if (branch == null)
            {
                MessageBox.Show($"Что то произошло не так. Пожалуйста, попробуйте еще раз", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            PreFillData();

        }

        private void PreFillData()
        {
            textBoxName.Text = _branch.Title;
            textBoxAddress.Text = _branch.Location;
            textBoxPhone.Text = _branch.Phone;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            _branch.Title = textBoxName.Text;
            _branch.Location = textBoxAddress.Text;
            _branch.Phone = textBoxPhone.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
