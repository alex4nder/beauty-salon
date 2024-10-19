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

        // ��������� ����������� DatabaseService ����� �����������
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
            // ���������� DatabaseService ��� ��������� ���������
            using (var context = _databaseService.GetLocalDbContext(1)) // ��������, ���������� ������ ��������� ����
            {
                var clients = context.Clients.ToList();

                // ��������� ������ � DataGridView
                dataGridViewClients.DataSource = clients;
            }
        }
    }
}
