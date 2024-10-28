using BeautySalonApp.Forms;
using BeautySalonApp.Forms.EntityActions;
using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly BranchService _branchService;
        private readonly DatabaseService _databaseService;
        private int _branchId;

        public MainForm()
        {
            _branchService = Program.ServiceProvider.GetRequiredService<BranchService>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadBranchesData();
        }

        private void LoadBranchesData()
        {
            var branches = _branchService.GetBranches();
            var branchesData = branches.Select(s => new
            {
                s.Id,
                Title = s.Title,
                Address = s.Location,
                PhoneNumber = s.Phone
            }).ToList();

            dataGridViewSalons.DataSource = branchesData;

            dataGridViewSalons.Columns["Title"].HeaderText = "Наименование салона";
            dataGridViewSalons.Columns["Address"].HeaderText = "Местоположение";
            dataGridViewSalons.Columns["PhoneNumber"].HeaderText = "Номер телефона";

            dataGridViewSalons.Columns["Id"].Visible = false;

            const string actionButtonColumnName = "actionButtonColumn";

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Действие",
                Text = "Редактировать",
                UseColumnTextForButtonValue = true,
                Width = 100,
                Name = actionButtonColumnName
            };

            if (!dataGridViewSalons.Columns.Contains(actionButtonColumnName))
            {
                dataGridViewSalons.Columns.Add(buttonColumn);
            }

            Controls.Add(dataGridViewSalons);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBranch(int branchId)
        {
            new EntityOperationBuilder<Branch>()
                .WithFormCreator(branch => new BranchForm(branch))
                .WithUpdateAction(branch => _branchService.BranchEdit(branch))
                .WithLoadData(LoadBranchesData)
                .ExecuteEdit(_branchService.GetBranchById(branchId));
        }

        private void dataGridViewSalons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewSalons.Rows[e.RowIndex];
            int branchId = Convert.ToInt32(row.Cells["Id"].Value.ToString());

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewSalons.Columns["actionButtonColumn"].Index)
            {
                EditBranch(branchId);
                return;
            }

            if (e.RowIndex >= 0)
            {
                var CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
                var selectedRow = dataGridViewSalons.Rows[e.RowIndex];
                _branchId = e.RowIndex + 1;

                CurrentBranchContext.BranchId = _branchId;

                this.Hide();

                using (SalonForm salonForm = new SalonForm())
                {
                    salonForm.Text = $"Филиал салона - {selectedRow.Cells["Title"].Value.ToString()}";
                    salonForm.ShowDialog();
                }

                this.Show();
            }
        }
    }
}
