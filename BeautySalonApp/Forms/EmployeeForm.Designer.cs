namespace BeautySalonApp.Forms
{
    partial class EmployeeForm
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
            employeeFirstNameTextBox = new TextBox();
            employeeLastNameTextBox = new TextBox();
            employeePhoneTextBox = new TextBox();
            employeePositionTextBox = new TextBox();
            employeeWbnTextBox = new TextBox();
            saveEmployeeBtn = new Button();
            SuspendLayout();
            // 
            // employeeFirstNameTextBox
            // 
            employeeFirstNameTextBox.Location = new Point(12, 12);
            employeeFirstNameTextBox.Name = "employeeFirstNameTextBox";
            employeeFirstNameTextBox.PlaceholderText = "Имя";
            employeeFirstNameTextBox.Size = new Size(183, 23);
            employeeFirstNameTextBox.TabIndex = 0;
            // 
            // employeeLastNameTextBox
            // 
            employeeLastNameTextBox.Location = new Point(210, 12);
            employeeLastNameTextBox.Name = "employeeLastNameTextBox";
            employeeLastNameTextBox.PlaceholderText = "Фамилия";
            employeeLastNameTextBox.Size = new Size(179, 23);
            employeeLastNameTextBox.TabIndex = 1;
            // 
            // employeePhoneTextBox
            // 
            employeePhoneTextBox.Location = new Point(12, 55);
            employeePhoneTextBox.Name = "employeePhoneTextBox";
            employeePhoneTextBox.PlaceholderText = "Номер телефона";
            employeePhoneTextBox.Size = new Size(183, 23);
            employeePhoneTextBox.TabIndex = 2;
            // 
            // employeePositionTextBox
            // 
            employeePositionTextBox.Location = new Point(210, 55);
            employeePositionTextBox.Name = "employeePositionTextBox";
            employeePositionTextBox.PlaceholderText = "Должность";
            employeePositionTextBox.Size = new Size(179, 23);
            employeePositionTextBox.TabIndex = 3;
            // 
            // employeeWbnTextBox
            // 
            employeeWbnTextBox.Location = new Point(12, 99);
            employeeWbnTextBox.Name = "employeeWbnTextBox";
            employeeWbnTextBox.PlaceholderText = "Номер трудовой книдки";
            employeeWbnTextBox.Size = new Size(377, 23);
            employeeWbnTextBox.TabIndex = 4;
            // 
            // saveEmployeeBtn
            // 
            saveEmployeeBtn.Location = new Point(276, 256);
            saveEmployeeBtn.Name = "saveEmployeeBtn";
            saveEmployeeBtn.Size = new Size(113, 23);
            saveEmployeeBtn.TabIndex = 5;
            saveEmployeeBtn.Text = "Сохранить";
            saveEmployeeBtn.UseVisualStyleBackColor = true;
            saveEmployeeBtn.Click += saveEmployeeBtn_Click;
            // 
            // EmployeeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 291);
            Controls.Add(saveEmployeeBtn);
            Controls.Add(employeeWbnTextBox);
            Controls.Add(employeePositionTextBox);
            Controls.Add(employeePhoneTextBox);
            Controls.Add(employeeLastNameTextBox);
            Controls.Add(employeeFirstNameTextBox);
            Name = "EmployeeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EmployeeForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox employeeFirstNameTextBox;
        private TextBox employeeLastNameTextBox;
        private TextBox employeePhoneTextBox;
        private TextBox employeePositionTextBox;
        private TextBox employeeWbnTextBox;
        private Button saveEmployeeBtn;
    }
}