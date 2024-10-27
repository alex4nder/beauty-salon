using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly SalonService _salonService;
        private readonly DatabaseService _databaseService;
        private int _salonId;

        public MainForm()
        {
            _salonService = Program.ServiceProvider.GetRequiredService<SalonService>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

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
                var currentSalonContext = Program.ServiceProvider.GetRequiredService<CurrentSalonContext>();
                currentSalonContext.SalonId = _salonId;

                SalonForm salonForm = new SalonForm();
                //salonForm.SetSalonId(_salonId);
                //salonForm.setDbContext();
                salonForm.Text = $"����� - {selectedRow.Cells["name"].Value.ToString()}";
                salonForm.ShowDialog();

            }
        }

        //private void UpdateServices()
        //{
        //    // �������� ������ � ������, ������� ������ �������� � ������ ��������� �������
        //    var dbService = Program.ServiceProvider.GetRequiredService<DatabaseService>();
        //    var employeeService = Program.ServiceProvider.GetRequiredService<EmployeeService>();

        //    // ��������� �������� � ��������
        //    // ���� ��� EmployeeService ����� ����� ��� ���������� ���������, �������� ���
        //    employeeService.UpdateDbContext(dbService.GetLocalDbContext(_salonId));

        //    // ���� ���� ������ �������, ������� ����� ������� �� salonId,
        //    // �������� �� ����� ����������� �������

        //    // ��������, ���� � ��� ���� ClientService, ������� ���� ��������� � ����������
        //    var clientService = Program.ServiceProvider.GetRequiredService<ClientService>();
        //    clientService.UpdateDbContext(dbService.GetLocalDbContext(_salonId));

        //    // ��������� ��� ������ �������� �� ���� �������������
        //}

    }
}
