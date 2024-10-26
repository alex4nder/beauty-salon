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
            EmployeeDetailsTabControl = new TabControl();
            appointmentsTab = new TabPage();
            performanceTab = new TabPage();
            EmployeeDetailsTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // EmployeeDetailsTabControl
            // 
            EmployeeDetailsTabControl.Controls.Add(appointmentsTab);
            EmployeeDetailsTabControl.Controls.Add(performanceTab);
            EmployeeDetailsTabControl.Location = new Point(1, 2);
            EmployeeDetailsTabControl.Name = "EmployeeDetailsTabControl";
            EmployeeDetailsTabControl.SelectedIndex = 0;
            EmployeeDetailsTabControl.Size = new Size(975, 498);
            EmployeeDetailsTabControl.TabIndex = 0;
            // 
            // appointmentsTab
            // 
            appointmentsTab.Location = new Point(4, 24);
            appointmentsTab.Name = "appointmentsTab";
            appointmentsTab.Padding = new Padding(3);
            appointmentsTab.Size = new Size(967, 470);
            appointmentsTab.TabIndex = 0;
            appointmentsTab.Text = "Записи";
            appointmentsTab.UseVisualStyleBackColor = true;
            // 
            // performanceTab
            // 
            performanceTab.Location = new Point(4, 24);
            performanceTab.Name = "performanceTab";
            performanceTab.Padding = new Padding(3);
            performanceTab.Size = new Size(967, 470);
            performanceTab.TabIndex = 1;
            performanceTab.Text = "Производительность сотрудника";
            performanceTab.UseVisualStyleBackColor = true;
            // 
            // EmployeeDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 501);
            Controls.Add(EmployeeDetailsTabControl);
            Name = "EmployeeDetailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Сотрудник";
            EmployeeDetailsTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl EmployeeDetailsTabControl;
        private TabPage appointmentsTab;
        private TabPage performanceTab;
    }
}