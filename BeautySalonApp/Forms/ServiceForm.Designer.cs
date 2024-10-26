using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeautySalonApp.Forms
{
    partial class ServiceForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            serviceNameTextBox = new TextBox();
            descriptionTextBox = new TextBox();
            priceTextBox = new TextBox();
            durationTextBox = new TextBox();
            saveServiceBtn = new Button();
            SuspendLayout();
            // 
            // serviceNameTextBox
            // 
            serviceNameTextBox.Location = new Point(12, 12);
            serviceNameTextBox.Name = "serviceNameTextBox";
            serviceNameTextBox.PlaceholderText = "Название услуги";
            serviceNameTextBox.Size = new Size(440, 23);
            serviceNameTextBox.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(12, 55);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.PlaceholderText = "Описание";
            descriptionTextBox.Size = new Size(440, 23);
            descriptionTextBox.TabIndex = 1;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(12, 99);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.PlaceholderText = "Цена";
            priceTextBox.Size = new Size(183, 23);
            priceTextBox.TabIndex = 2;
            // 
            // durationTextBox
            // 
            durationTextBox.Location = new Point(210, 99);
            durationTextBox.Name = "durationTextBox";
            durationTextBox.PlaceholderText = "Длительность (мин.)";
            durationTextBox.Size = new Size(242, 23);
            durationTextBox.TabIndex = 3;
            // 
            // saveServiceBtn
            // 
            saveServiceBtn.Location = new Point(339, 300);
            saveServiceBtn.Name = "saveServiceBtn";
            saveServiceBtn.Size = new Size(113, 23);
            saveServiceBtn.TabIndex = 4;
            saveServiceBtn.Text = "Сохранить";
            saveServiceBtn.UseVisualStyleBackColor = true;
            saveServiceBtn.Click += saveServiceBtn_Click; // Добавлено событие клика
            // 
            // ServiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 345);
            Controls.Add(saveServiceBtn);
            Controls.Add(durationTextBox);
            Controls.Add(priceTextBox);
            Controls.Add(descriptionTextBox);
            Controls.Add(serviceNameTextBox);
            Name = "ServiceForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Услуга";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox serviceNameTextBox;
        private TextBox descriptionTextBox;
        private TextBox priceTextBox;
        private TextBox durationTextBox;
        private Button saveServiceBtn;

    }
}
