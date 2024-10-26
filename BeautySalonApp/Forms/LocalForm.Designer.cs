namespace BeautySalonApp
{
    partial class SalonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            employeesTab = new TabControl();
            servicesTab = new TabPage();
            dataGridViewServices = new DataGridView();
            employeeTab = new TabPage();
            addEmployeeBtn = new Button();
            dataGridViewEmployees = new DataGridView();
            managersTab = new TabPage();
            addManagerBtn = new Button();
            dataGridViewManagers = new DataGridView();
            reportsTab = new TabPage();
            label2 = new Label();
            label1 = new Label();
            revenueReportDateFrom = new DateTimePicker();
            revenueReportDateTo = new DateTimePicker();
            generateRevenueReportBtn = new Button();
            dataGridViewRevenueReports = new DataGridView();
            clientsTab = new TabPage();
            addClientBtn = new Button();
            dataGridViewClients = new DataGridView();
            clientFeedbackTab = new TabPage();
            dataGridViewClientFeedback = new DataGridView();
            addServiceBtn = new Button();
            employeesTab.SuspendLayout();
            servicesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewServices).BeginInit();
            employeeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployees).BeginInit();
            managersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewManagers).BeginInit();
            reportsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).BeginInit();
            clientsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).BeginInit();
            clientFeedbackTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientFeedback).BeginInit();
            SuspendLayout();
            // 
            // employeesTab
            // 
            employeesTab.Controls.Add(servicesTab);
            employeesTab.Controls.Add(employeeTab);
            employeesTab.Controls.Add(managersTab);
            employeesTab.Controls.Add(reportsTab);
            employeesTab.Controls.Add(clientsTab);
            employeesTab.Controls.Add(clientFeedbackTab);
            employeesTab.Dock = DockStyle.Fill;
            employeesTab.Location = new Point(0, 0);
            employeesTab.Name = "employeesTab";
            employeesTab.SelectedIndex = 0;
            employeesTab.Size = new Size(976, 501);
            employeesTab.TabIndex = 0;
            employeesTab.SelectedIndexChanged += employeesTab_SelectedIndexChanged;
            // 
            // servicesTab
            // 
            servicesTab.Controls.Add(addServiceBtn);
            servicesTab.Controls.Add(dataGridViewServices);
            servicesTab.Location = new Point(4, 24);
            servicesTab.Name = "servicesTab";
            servicesTab.Padding = new Padding(3);
            servicesTab.Size = new Size(968, 473);
            servicesTab.TabIndex = 0;
            servicesTab.Text = "Предоставляемые услуги";
            servicesTab.UseVisualStyleBackColor = true;
            // 
            // dataGridViewServices
            // 
            dataGridViewServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewServices.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewServices.Location = new Point(8, 40);
            dataGridViewServices.Name = "dataGridViewServices";
            dataGridViewServices.Size = new Size(954, 407);
            dataGridViewServices.TabIndex = 1;
            // 
            // employeeTab
            // 
            employeeTab.Controls.Add(addEmployeeBtn);
            employeeTab.Controls.Add(dataGridViewEmployees);
            employeeTab.Location = new Point(4, 24);
            employeeTab.Name = "employeeTab";
            employeeTab.Padding = new Padding(3);
            employeeTab.Size = new Size(968, 473);
            employeeTab.TabIndex = 1;
            employeeTab.Text = "Сотрудники";
            employeeTab.UseVisualStyleBackColor = true;
            // 
            // addEmployeeBtn
            // 
            addEmployeeBtn.Location = new Point(765, 9);
            addEmployeeBtn.Name = "addEmployeeBtn";
            addEmployeeBtn.Size = new Size(195, 23);
            addEmployeeBtn.TabIndex = 1;
            addEmployeeBtn.Text = "Добавить нового сотрудника";
            addEmployeeBtn.UseVisualStyleBackColor = true;
            addEmployeeBtn.Click += addEmployeeBtn_Click;
            // 
            // dataGridViewEmployees
            // 
            dataGridViewEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEmployees.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployees.Location = new Point(6, 39);
            dataGridViewEmployees.Name = "dataGridViewEmployees";
            dataGridViewEmployees.Size = new Size(954, 426);
            dataGridViewEmployees.TabIndex = 0;
            dataGridViewEmployees.CellContentDoubleClick += dataGridViewEmployees_CellContentDoubleClick;
            // 
            // managersTab
            // 
            managersTab.Controls.Add(addManagerBtn);
            managersTab.Controls.Add(dataGridViewManagers);
            managersTab.Location = new Point(4, 24);
            managersTab.Name = "managersTab";
            managersTab.Padding = new Padding(3);
            managersTab.Size = new Size(968, 473);
            managersTab.TabIndex = 2;
            managersTab.Text = "Менеджеры";
            managersTab.UseVisualStyleBackColor = true;
            // 
            // addManagerBtn
            // 
            addManagerBtn.Location = new Point(762, 8);
            addManagerBtn.Name = "addManagerBtn";
            addManagerBtn.Size = new Size(198, 23);
            addManagerBtn.TabIndex = 1;
            addManagerBtn.Text = "Добавить нового менеджера";
            addManagerBtn.UseVisualStyleBackColor = true;
            addManagerBtn.Click += addManagerBtn_Click;
            // 
            // dataGridViewManagers
            // 
            dataGridViewManagers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewManagers.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewManagers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewManagers.Location = new Point(6, 37);
            dataGridViewManagers.Name = "dataGridViewManagers";
            dataGridViewManagers.Size = new Size(954, 428);
            dataGridViewManagers.TabIndex = 0;
            // 
            // reportsTab
            // 
            reportsTab.Controls.Add(label2);
            reportsTab.Controls.Add(label1);
            reportsTab.Controls.Add(revenueReportDateFrom);
            reportsTab.Controls.Add(revenueReportDateTo);
            reportsTab.Controls.Add(generateRevenueReportBtn);
            reportsTab.Controls.Add(dataGridViewRevenueReports);
            reportsTab.Location = new Point(4, 24);
            reportsTab.Name = "reportsTab";
            reportsTab.Padding = new Padding(3);
            reportsTab.Size = new Size(968, 473);
            reportsTab.TabIndex = 3;
            reportsTab.Text = "Отчеты о доходах";
            reportsTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(565, 12);
            label2.Name = "label2";
            label2.Size = new Size(22, 15);
            label2.TabIndex = 5;
            label2.Text = "До";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(332, 12);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 4;
            label1.Text = "От";
            // 
            // revenueReportDateFrom
            // 
            revenueReportDateFrom.Format = DateTimePickerFormat.Short;
            revenueReportDateFrom.Location = new Point(359, 8);
            revenueReportDateFrom.Name = "revenueReportDateFrom";
            revenueReportDateFrom.Size = new Size(200, 23);
            revenueReportDateFrom.TabIndex = 3;
            // 
            // revenueReportDateTo
            // 
            revenueReportDateTo.Format = DateTimePickerFormat.Short;
            revenueReportDateTo.Location = new Point(593, 8);
            revenueReportDateTo.Name = "revenueReportDateTo";
            revenueReportDateTo.Size = new Size(200, 23);
            revenueReportDateTo.TabIndex = 2;
            revenueReportDateTo.Tag = "";
            // 
            // generateRevenueReportBtn
            // 
            generateRevenueReportBtn.Location = new Point(799, 7);
            generateRevenueReportBtn.Name = "generateRevenueReportBtn";
            generateRevenueReportBtn.Size = new Size(161, 23);
            generateRevenueReportBtn.TabIndex = 1;
            generateRevenueReportBtn.Text = "Получить отчет";
            generateRevenueReportBtn.UseVisualStyleBackColor = true;
            generateRevenueReportBtn.Click += generateRevenueReportBtn_Click;
            // 
            // dataGridViewRevenueReports
            // 
            dataGridViewRevenueReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRevenueReports.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewRevenueReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRevenueReports.Location = new Point(8, 36);
            dataGridViewRevenueReports.Name = "dataGridViewRevenueReports";
            dataGridViewRevenueReports.Size = new Size(952, 429);
            dataGridViewRevenueReports.TabIndex = 0;
            // 
            // clientsTab
            // 
            clientsTab.Controls.Add(addClientBtn);
            clientsTab.Controls.Add(dataGridViewClients);
            clientsTab.Location = new Point(4, 24);
            clientsTab.Name = "clientsTab";
            clientsTab.Padding = new Padding(3);
            clientsTab.Size = new Size(968, 473);
            clientsTab.TabIndex = 4;
            clientsTab.Text = "Клиенты";
            clientsTab.UseVisualStyleBackColor = true;
            // 
            // addClientBtn
            // 
            addClientBtn.Location = new Point(780, 7);
            addClientBtn.Name = "addClientBtn";
            addClientBtn.Size = new Size(180, 23);
            addClientBtn.TabIndex = 1;
            addClientBtn.Text = "Добавить нового клиента";
            addClientBtn.UseVisualStyleBackColor = true;
            addClientBtn.Click += addClientBtn_Click;
            // 
            // dataGridViewClients
            // 
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClients.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClients.Location = new Point(8, 36);
            dataGridViewClients.Name = "dataGridViewClients";
            dataGridViewClients.Size = new Size(952, 429);
            dataGridViewClients.TabIndex = 0;
            // 
            // clientFeedbackTab
            // 
            clientFeedbackTab.Controls.Add(dataGridViewClientFeedback);
            clientFeedbackTab.Location = new Point(4, 24);
            clientFeedbackTab.Name = "clientFeedbackTab";
            clientFeedbackTab.Padding = new Padding(3);
            clientFeedbackTab.Size = new Size(968, 473);
            clientFeedbackTab.TabIndex = 5;
            clientFeedbackTab.Text = "Отзывы";
            clientFeedbackTab.UseVisualStyleBackColor = true;
            // 
            // dataGridViewClientFeedback
            // 
            dataGridViewClientFeedback.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClientFeedback.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewClientFeedback.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientFeedback.Location = new Point(5, 6);
            dataGridViewClientFeedback.Name = "dataGridViewClientFeedback";
            dataGridViewClientFeedback.Size = new Size(957, 460);
            dataGridViewClientFeedback.TabIndex = 0;
            // 
            // addServiceBtn
            // 
            addServiceBtn.Location = new Point(751, 11);
            addServiceBtn.Name = "addServiceBtn";
            addServiceBtn.Size = new Size(195, 23);
            addServiceBtn.TabIndex = 2;
            addServiceBtn.Text = "Добавить новую услугу";
            addServiceBtn.UseVisualStyleBackColor = true;
            addServiceBtn.Click += addServiceBtn_Click;
            // 
            // SalonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 501);
            Controls.Add(employeesTab);
            Name = "SalonForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Салон";
            employeesTab.ResumeLayout(false);
            servicesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewServices).EndInit();
            employeeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployees).EndInit();
            managersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewManagers).EndInit();
            reportsTab.ResumeLayout(false);
            reportsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).EndInit();
            clientsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).EndInit();
            clientFeedbackTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientFeedback).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl employeesTab;
        private TabPage servicesTab;
        private TabPage employeeTab;
        private TabPage managersTab;
        private TabPage reportsTab;
        private TabPage clientsTab;
        private TabPage clientFeedbackTab;
        private DataGridView dataGridViewRevenueReports;
        private Button generateRevenueReportBtn;
        private DateTimePicker revenueReportDateTo;
        private Label label2;
        private Label label1;
        private DateTimePicker revenueReportDateFrom;
        private DataGridView dataGridViewClientFeedback;
        private DataGridView dataGridViewClients;
        private Button addClientBtn;
        private Button addEmployeeBtn;
        private DataGridView dataGridViewEmployees;
        private Button addManagerBtn;
        private DataGridView dataGridViewManagers;
        private DataGridView dataGridViewServices;
        private Button addServiceBtn;
    }
}