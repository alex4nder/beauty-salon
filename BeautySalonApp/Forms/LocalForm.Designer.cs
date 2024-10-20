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
            dataGridViewRevenueReports = new DataGridView();
            clientsTab = new TabPage();
            reviewsTab = new TabPage();
            employeesTab.SuspendLayout();
            reportsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).BeginInit();
            SuspendLayout();
            // 
            // employeesTab
            // 
            employeesTab.Controls.Add(servicesTab);
            employeesTab.Controls.Add(tabPage2);
            employeesTab.Controls.Add(managersTab);
            employeesTab.Controls.Add(reportsTab);
            employeesTab.Controls.Add(clientsTab);
            employeesTab.Controls.Add(reviewsTab);
            employeesTab.Dock = DockStyle.Fill;
            employeesTab.Location = new Point(0, 0);
            employeesTab.Name = "employeesTab";
            employeesTab.SelectedIndex = 0;
            employeesTab.Size = new Size(800, 450);
            employeesTab.TabIndex = 0;
            employeesTab.SelectedIndexChanged += employeesTab_SelectedIndexChanged;
            // 
            // servicesTab
            // 
            servicesTab.Location = new Point(4, 24);
            servicesTab.Name = "servicesTab";
            servicesTab.Padding = new Padding(3);
            servicesTab.Size = new Size(792, 422);
            servicesTab.TabIndex = 0;
            servicesTab.Text = "Предоставляемые услуги";
            servicesTab.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Сотрудники";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // managersTab
            // 
            managersTab.Location = new Point(4, 24);
            managersTab.Name = "managersTab";
            managersTab.Padding = new Padding(3);
            managersTab.Size = new Size(792, 422);
            managersTab.TabIndex = 2;
            managersTab.Text = "Менеджеры";
            managersTab.UseVisualStyleBackColor = true;
            // 
            // reportsTab
            // 
            reportsTab.Controls.Add(dataGridViewRevenueReports);
            reportsTab.Location = new Point(4, 24);
            reportsTab.Name = "reportsTab";
            reportsTab.Padding = new Padding(3);
            reportsTab.Size = new Size(792, 422);
            reportsTab.TabIndex = 3;
            reportsTab.Text = "Отчеты о доходах";
            reportsTab.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRevenueReports
            // 
            dataGridViewRevenueReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRevenueReports.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewRevenueReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRevenueReports.Location = new Point(8, 36);
            dataGridViewRevenueReports.Name = "dataGridViewRevenueReports";
            dataGridViewRevenueReports.Size = new Size(776, 378);
            dataGridViewRevenueReports.TabIndex = 0;
            // 
            // clientsTab
            // 
            clientsTab.Location = new Point(4, 24);
            clientsTab.Name = "clientsTab";
            clientsTab.Padding = new Padding(3);
            clientsTab.Size = new Size(792, 422);
            clientsTab.TabIndex = 4;
            clientsTab.Text = "Клиенты";
            clientsTab.UseVisualStyleBackColor = true;
            // 
            // reviewsTab
            // 
            reviewsTab.Location = new Point(4, 24);
            reviewsTab.Name = "reviewsTab";
            reviewsTab.Padding = new Padding(3);
            reviewsTab.Size = new Size(792, 422);
            reviewsTab.TabIndex = 5;
            reviewsTab.Text = "Отзывы";
            reviewsTab.UseVisualStyleBackColor = true;
            // 
            // SalonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(employeesTab);
            Name = "SalonForm";
            Text = "Салон";
            employeesTab.ResumeLayout(false);
            reportsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewRevenueReports).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl employeesTab;
        private TabPage servicesTab;
        private TabPage tabPage2;
        private TabPage managersTab;
        private TabPage reportsTab;
        private TabPage clientsTab;
        private TabPage reviewsTab;
        private DataGridView dataGridViewRevenueReports;
    }
}