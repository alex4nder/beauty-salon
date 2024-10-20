using BeautySalonApp.Services;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly SalonService _salonService;
        private DataGridView dataGridViewSalons;

        // Внедрение зависимости DatabaseService через конструктор
        public MainForm(SalonService salonService)
        {
            _salonService = salonService;
            InitializeComponent();
            InitializeDataGridView();

            dataGridViewSalons.CellDoubleClick += DataGridView_CellDoubleClick;
        }

        private void InitializeDataGridView()
        {
            dataGridViewSalons = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            Controls.Add(dataGridViewSalons);
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

            dataGridViewSalons.Columns["Name"].HeaderText = "Название Салона";
            dataGridViewSalons.Columns["Address"].HeaderText = "Адрес";
            dataGridViewSalons.Columns["PhoneNumber"].HeaderText = "Номер Телефона";

            dataGridViewSalons.Columns["Id"].Visible = false;
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the double-clicked cell is not a header
            if (e.RowIndex >= 0)
            {
                // Get the selected row data if needed
                var selectedRow = dataGridViewSalons.Rows[e.RowIndex];

                // Create an instance of the DetailForm and pass the selected data if needed
                SalonForm salonForm = new SalonForm();

                // You can pass data to the new form if needed
                // For example:
                // detailForm.SetData(selectedRow.Cells["ColumnName"].Value.ToString());

                // Show the new form
                salonForm.ShowDialog(); // Use Show() if you want a modeless dialog
            }
        }

    }
}
