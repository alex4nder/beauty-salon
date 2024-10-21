using BeautySalonApp.Services;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly SalonService _salonService;
        private readonly RevenueReportService _revenueReportService;

        public MainForm(SalonService salonService, RevenueReportService revenueReportService)
        {
            _salonService = salonService;
            _revenueReportService = revenueReportService;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSalonData();
        }

        private void LoadSalonData()
        {
            var salons = _salonService.GetSalons();
            var salonData = salons.Select(s => new
            {
                s.Id,
                Name = s.SalonName,
                Address = $"{s.Address.AddressLine}, {s.Address.City}, {s.Address.State}, {s.Address.PostalCode}",
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
                UseColumnTextForButtonValue = true, // ���������, ��� ����� ������ ����� ���������� ��� ���� �����
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

        private void dataGridViewSalons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewSalons.Columns["actionButtonColumn"].Index)
            {
                var selectedRow = dataGridViewSalons.Rows[e.RowIndex];

                SalonForm salonForm = new SalonForm(_salonService, _revenueReportService);
                salonForm.SetSalonId(e.RowIndex + 1);
                salonForm.Text = $"����� - {selectedRow.Cells["name"].Value.ToString()}";
                salonForm.ShowDialog();
            }
        }
    }
}
