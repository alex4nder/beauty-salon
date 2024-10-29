﻿namespace BeautySalonApp.Forms
{
    partial class CustomerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerForm));
            saveClientBtn = new Button();
            clientFirstNameTextBox = new TextBox();
            clientLastNameTextBox = new TextBox();
            clientPhoneTextBox = new TextBox();
            clientEmailTextBox = new TextBox();
            clientDateOfBirthDateTimePicker = new DateTimePicker();
            label2 = new Label();
            SuspendLayout();
            // 
            // saveClientBtn
            // 
            saveClientBtn.Location = new Point(139, 158);
            saveClientBtn.Name = "saveClientBtn";
            saveClientBtn.Size = new Size(123, 34);
            saveClientBtn.TabIndex = 0;
            saveClientBtn.Text = "Сохранить";
            saveClientBtn.UseVisualStyleBackColor = true;
            saveClientBtn.Click += saveClientBtn_Click;
            // 
            // clientFirstNameTextBox
            // 
            clientFirstNameTextBox.Location = new Point(12, 12);
            clientFirstNameTextBox.Name = "clientFirstNameTextBox";
            clientFirstNameTextBox.PlaceholderText = "Имя";
            clientFirstNameTextBox.Size = new Size(183, 23);
            clientFirstNameTextBox.TabIndex = 1;
            // 
            // clientLastNameTextBox
            // 
            clientLastNameTextBox.Location = new Point(206, 12);
            clientLastNameTextBox.Name = "clientLastNameTextBox";
            clientLastNameTextBox.PlaceholderText = "Фамилия";
            clientLastNameTextBox.Size = new Size(183, 23);
            clientLastNameTextBox.TabIndex = 2;
            // 
            // clientPhoneTextBox
            // 
            clientPhoneTextBox.Location = new Point(12, 53);
            clientPhoneTextBox.Name = "clientPhoneTextBox";
            clientPhoneTextBox.PlaceholderText = "Номер телефона";
            clientPhoneTextBox.Size = new Size(183, 23);
            clientPhoneTextBox.TabIndex = 3;
            // 
            // clientEmailTextBox
            // 
            clientEmailTextBox.Location = new Point(206, 53);
            clientEmailTextBox.Name = "clientEmailTextBox";
            clientEmailTextBox.PlaceholderText = "Адрес эл. почты";
            clientEmailTextBox.Size = new Size(183, 23);
            clientEmailTextBox.TabIndex = 4;
            // 
            // clientDateOfBirthDateTimePicker
            // 
            clientDateOfBirthDateTimePicker.Location = new Point(14, 105);
            clientDateOfBirthDateTimePicker.Name = "clientDateOfBirthDateTimePicker";
            clientDateOfBirthDateTimePicker.Size = new Size(375, 23);
            clientDateOfBirthDateTimePicker.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 86);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 8;
            label2.Text = "Дата рождения:";
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 204);
            Controls.Add(label2);
            Controls.Add(clientDateOfBirthDateTimePicker);
            Controls.Add(clientEmailTextBox);
            Controls.Add(clientPhoneTextBox);
            Controls.Add(clientLastNameTextBox);
            Controls.Add(clientFirstNameTextBox);
            Controls.Add(saveClientBtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CustomerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Клиент";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button saveClientBtn;
        private TextBox clientFirstNameTextBox;
        private TextBox clientLastNameTextBox;
        private TextBox clientPhoneTextBox;
        private TextBox clientEmailTextBox;
        private DateTimePicker clientDateOfBirthDateTimePicker;
        private Label label2;
    }
}