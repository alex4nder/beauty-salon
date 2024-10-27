namespace BeautySalonApp.Forms
{
    partial class EmployeeDetailsForm
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
            employeeDetailsTabControl = new TabControl();
            appointmentsTab = new TabPage();
            addAppointmentBtn = new Button();
            appointmentsDataGridView = new DataGridView();
            performanceTab = new TabPage();
            dataGridViewEmPerformance = new DataGridView();
            employeeDetailsTabControl.SuspendLayout();
            appointmentsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)appointmentsDataGridView).BeginInit();
            performanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmPerformance).BeginInit();
            SuspendLayout();
            // 
            // employeeDetailsTabControl
            // 
            employeeDetailsTabControl.Controls.Add(appointmentsTab);
            employeeDetailsTabControl.Controls.Add(performanceTab);
            employeeDetailsTabControl.Location = new Point(1, 2);
            employeeDetailsTabControl.Name = "EmployeeDetailsTabControl";
            employeeDetailsTabControl.SelectedIndex = 0;
            employeeDetailsTabControl.Size = new Size(975, 498);
            employeeDetailsTabControl.TabIndex = 0;
            employeeDetailsTabControl.SelectedIndexChanged += employeeDetailsTabControl_SelectedIndexChanged;
            // 
            // appointmentsTab
            // 
            appointmentsTab.Controls.Add(addAppointmentBtn);
            appointmentsTab.Controls.Add(appointmentsDataGridView);
            appointmentsTab.Location = new Point(4, 24);
            appointmentsTab.Name = "appointmentsTab";
            appointmentsTab.Padding = new Padding(3);
            appointmentsTab.Size = new Size(967, 470);
            appointmentsTab.TabIndex = 0;
            appointmentsTab.Text = "Записи";
            appointmentsTab.UseVisualStyleBackColor = true;
            // 
            // addAppointmentBtn
            // 
            addAppointmentBtn.Location = new Point(789, 7);
            addAppointmentBtn.Name = "addAppointmentBtn";
            addAppointmentBtn.Size = new Size(172, 23);
            addAppointmentBtn.TabIndex = 1;
            addAppointmentBtn.Text = "Новая запись";
            addAppointmentBtn.UseVisualStyleBackColor = true;
            addAppointmentBtn.Click += addAppointmentBtn_Click;
            // 
            // appointmentsDataGridView
            // 
            appointmentsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            appointmentsDataGridView.BackgroundColor = SystemColors.ButtonHighlight;
            appointmentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentsDataGridView.Location = new Point(3, 36);
            appointmentsDataGridView.Name = "appointmentsDataGridView";
            appointmentsDataGridView.Size = new Size(961, 431);
            appointmentsDataGridView.TabIndex = 0;
            // 
            // performanceTab
            // 
            performanceTab.Controls.Add(dataGridViewEmPerformance);
            performanceTab.Location = new Point(4, 24);
            performanceTab.Name = "performanceTab";
            performanceTab.Padding = new Padding(3);
            performanceTab.Size = new Size(967, 470);
            performanceTab.TabIndex = 1;
            performanceTab.Text = "Производительность сотрудника";
            performanceTab.UseVisualStyleBackColor = true;
            // 
            // dataGridViewEmPerformance
            // 
            dataGridViewEmPerformance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEmPerformance.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewEmPerformance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmPerformance.Location = new Point(3, 36);
            dataGridViewEmPerformance.Name = "dataGridViewEmPerformance";
            dataGridViewEmPerformance.Size = new Size(961, 431);
            dataGridViewEmPerformance.TabIndex = 1;
            // 
            // EmployeeDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 501);
            Controls.Add(employeeDetailsTabControl);
            Name = "EmployeeDetailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Сотрудник";
            employeeDetailsTabControl.ResumeLayout(false);
            appointmentsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)appointmentsDataGridView).EndInit();
            performanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmPerformance).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl employeeDetailsTabControl;
        private TabPage appointmentsTab;
        private TabPage performanceTab;
        private DataGridView appointmentsDataGridView;
        private Button addAppointmentBtn;
        private DataGridView dataGridViewEmPerformance;
    }
}