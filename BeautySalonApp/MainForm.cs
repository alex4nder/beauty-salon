using BeautySalonApp.Data;
using BeautySalonApp.Models;
using BeautySalonApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BeautySalonApp
{
    public partial class MainForm : Form
    {
        private readonly DatabaseService _databaseService;
        private DataGridView dataGridViewClients;

        // Внедрение зависимости DatabaseService через конструктор
        public MainForm(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridViewClients = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            Controls.Add(dataGridViewClients);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadClientData();
        }

        private void LoadClientData()
        {
            // Используем DatabaseService для получения контекста
            using (var context = _databaseService.GetLocalDbContext(1)) // Например, используем первую локальную базу
            {
                var clients = context.Clients.ToList();

                // Связываем данные с DataGridView
                dataGridViewClients.DataSource = clients;
            }
        }
    }
}
