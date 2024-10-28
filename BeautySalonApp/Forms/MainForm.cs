using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly BranchService _salonService;
        private readonly DatabaseService _databaseService;
        private int _salonId;

        public MainForm()
        {
            _salonService = Program.ServiceProvider.GetRequiredService<BranchService>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSalonData();
        }

        private void LoadSalonData()
        {
            var salons = _salonService.GetBranches();
            var salonData = salons.Select(s => new
            {
                s.Id,
                Name = s.Title,
                Address = s.Location,
                PhoneNumber = s.Phone
            }).ToList();

            dataGridViewSalons.DataSource = salonData;

            dataGridViewSalons.Columns["Name"].HeaderText = "�������� �������";
            dataGridViewSalons.Columns["Address"].HeaderText = "�����";
            dataGridViewSalons.Columns["PhoneNumber"].HeaderText = "����� ��������";

            dataGridViewSalons.Columns["Id"].Visible = false;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "��������",
                Text = "�������",
                UseColumnTextForButtonValue = true,
                Width = 100,
                Name = "actionButtonColumn"
            };

            dataGridViewSalons.Columns.Add(buttonColumn);

            Controls.Add(dataGridViewSalons);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public int GetSalonId()
        {
            return _salonId;
        }

        private void dataGridViewSalons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewSalons.Columns["actionButtonColumn"].Index)
            {
                var selectedRow = dataGridViewSalons.Rows[e.RowIndex];
                _salonId = e.RowIndex + 1;

                //_databaseService.GetLocalDbContext(salonId);
                var CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
                CurrentBranchContext.BranchId = _salonId;

                SalonForm salonForm = new SalonForm();
                //salonForm.SetSalonId(_salonId);
                //salonForm.setDbContext();
                salonForm.Text = $"����� - {selectedRow.Cells["name"].Value.ToString()}";
                salonForm.ShowDialog();

            }
        }
    }
}
