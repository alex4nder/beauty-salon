namespace BeautySalonApp.Forms
{
    partial class ManagerForm
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
            managerFirstNameTextBox = new TextBox();
            managerLastNameTextBox = new TextBox();
            managerPhoneTextBox = new TextBox();
            managerEmailTextBox = new TextBox();
            saveManagerBtn = new Button();
            SuspendLayout();
            // 
            // managerFirstNameTextBox
            // 
            managerFirstNameTextBox.Location = new Point(12, 12);
            managerFirstNameTextBox.Name = "managerFirstNameTextBox";
            managerFirstNameTextBox.PlaceholderText = "Имя";
            managerFirstNameTextBox.Size = new Size(179, 23);
            managerFirstNameTextBox.TabIndex = 0;
            // 
            // managerLastNameTextBox
            // 
            managerLastNameTextBox.Location = new Point(207, 12);
            managerLastNameTextBox.Name = "managerLastNameTextBox";
            managerLastNameTextBox.PlaceholderText = "Фамилия";
            managerLastNameTextBox.Size = new Size(182, 23);
            managerLastNameTextBox.TabIndex = 1;
            // 
            // managerPhoneTextBox
            // 
            managerPhoneTextBox.Location = new Point(12, 55);
            managerPhoneTextBox.Name = "managerPhoneTextBox";
            managerPhoneTextBox.PlaceholderText = "Номер телефона";
            managerPhoneTextBox.Size = new Size(179, 23);
            managerPhoneTextBox.TabIndex = 2;
            // 
            // managerEmailTextBox
            // 
            managerEmailTextBox.Location = new Point(207, 55);
            managerEmailTextBox.Name = "managerEmailTextBox";
            managerEmailTextBox.PlaceholderText = "Адрес эл. почты";
            managerEmailTextBox.Size = new Size(182, 23);
            managerEmailTextBox.TabIndex = 3;
            // 
            // saveManagerBtn
            // 
            saveManagerBtn.Location = new Point(268, 256);
            saveManagerBtn.Name = "saveManagerBtn";
            saveManagerBtn.Size = new Size(121, 23);
            saveManagerBtn.TabIndex = 4;
            saveManagerBtn.Text = "Сохранить";
            saveManagerBtn.UseVisualStyleBackColor = true;
            saveManagerBtn.Click += saveManagerBtn_Click;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 291);
            Controls.Add(saveManagerBtn);
            Controls.Add(managerEmailTextBox);
            Controls.Add(managerPhoneTextBox);
            Controls.Add(managerLastNameTextBox);
            Controls.Add(managerFirstNameTextBox);
            Name = "ManagerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Менеджер";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox managerFirstNameTextBox;
        private TextBox managerLastNameTextBox;
        private TextBox managerPhoneTextBox;
        private TextBox managerEmailTextBox;
        private Button saveManagerBtn;
    }
}