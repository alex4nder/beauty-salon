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
            tabPage2 = new TabPage();
            managersTab = new TabPage();
            reportsTab = new TabPage();
            label2 = new Label();
            label1 = new Label();
            revenueReportDateFrom = new DateTimePicker();
            revenueReportDateTo = new DateTimePicker();
            generateRevenueReportBtn = new Button();
            dataGridViewRevenueReports = new DataGridView();
            clientsTab = new TabPage();
            clientFeedbackTab = new TabPage();
            dataGridViewClientFeedback = new DataGridView();
            employeesTab.SuspendLayout();
            reportsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).BeginInit();
            clientFeedbackTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientFeedback).BeginInit();
            SuspendLayout();
            // 
            // employeesTab
            // 
            employeesTab.Controls.Add(servicesTab);
            employeesTab.Controls.Add(tabPage2);
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
            servicesTab.Location = new Point(4, 24);
            servicesTab.Name = "servicesTab";
            servicesTab.Padding = new Padding(3);
            servicesTab.Size = new Size(968, 473);
            servicesTab.TabIndex = 0;
            servicesTab.Text = "Предоставляемые услуги";
            servicesTab.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(968, 473);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Сотрудники";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // managersTab
            // 
            managersTab.Location = new Point(4, 24);
            managersTab.Name = "managersTab";
            managersTab.Padding = new Padding(3);
            managersTab.Size = new Size(968, 473);
            managersTab.TabIndex = 2;
            managersTab.Text = "Менеджеры";
            managersTab.UseVisualStyleBackColor = true;
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
            clientsTab.Location = new Point(4, 24);
            clientsTab.Name = "clientsTab";
            clientsTab.Padding = new Padding(3);
            clientsTab.Size = new Size(968, 473);
            clientsTab.TabIndex = 4;
            clientsTab.Text = "Клиенты";
            clientsTab.UseVisualStyleBackColor = true;
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
            // SalonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 501);
            Controls.Add(employeesTab);
            Name = "SalonForm";
            Text = "Салон";
            employeesTab.ResumeLayout(false);
            reportsTab.ResumeLayout(false);
            reportsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).EndInit();
            clientFeedbackTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientFeedback).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl employeesTab;
        private TabPage servicesTab;
        private TabPage tabPage2;
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
    }
}