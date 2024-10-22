using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly SalonService _salonService;

        public MainForm()
        {
            _salonService = Program.ServiceProvider.GetRequiredService<SalonService>();

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

            dataGridViewSalons.Columns["Name"].HeaderText = "Название филиала";
            dataGridViewSalons.Columns["Address"].HeaderText = "Адрес";
            dataGridViewSalons.Columns["PhoneNumber"].HeaderText = "Номер телефона";

            dataGridViewSalons.Columns["Id"].Visible = false;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Действия",
                Text = "Перейти",
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

        private void dataGridViewSalons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewSalons.Columns["actionButtonColumn"].Index)
            {
                var selectedRow = dataGridViewSalons.Rows[e.RowIndex];

                SalonForm salonForm = new SalonForm();
                salonForm.SetSalonId(e.RowIndex + 1);
                salonForm.Text = $"Салон - {selectedRow.Cells["name"].Value.ToString()}";
                salonForm.ShowDialog();
            }
        }
    }
}
