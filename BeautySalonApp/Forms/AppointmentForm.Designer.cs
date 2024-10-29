namespace BeautySalonApp.Forms
{
    partial class AppointmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentForm));
            comboBoxClient = new ComboBox();
            comboBoxService = new ComboBox();
            dateTimePickerStartDate = new DateTimePicker();
            dateTimePickerEndDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            saveAppoinmentBtn = new Button();
            dateTimePickerStartTime = new DateTimePicker();
            dateTimePickerEndTime = new DateTimePicker();
            SuspendLayout();
            // 
            // comboBoxClient
            // 
            comboBoxClient.FormattingEnabled = true;
            comboBoxClient.Location = new Point(12, 23);
            comboBoxClient.Name = "comboBoxClient";
            comboBoxClient.Size = new Size(376, 23);
            comboBoxClient.TabIndex = 0;
            // 
            // comboBoxService
            // 
            comboBoxService.FormattingEnabled = true;
            comboBoxService.Location = new Point(12, 76);
            comboBoxService.Name = "comboBoxService";
            comboBoxService.Size = new Size(376, 23);
            comboBoxService.TabIndex = 1;
            // 
            // dateTimePickerStartDate
            // 
            dateTimePickerStartDate.CustomFormat = "\"dd/MM/yyyy HH:mm\"";
            dateTimePickerStartDate.Format = DateTimePickerFormat.Short;
            dateTimePickerStartDate.Location = new Point(12, 132);
            dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            dateTimePickerStartDate.Size = new Size(226, 23);
            dateTimePickerStartDate.TabIndex = 2;
            // 
            // dateTimePickerEndDate
            // 
            dateTimePickerEndDate.Format = DateTimePickerFormat.Short;
            dateTimePickerEndDate.Location = new Point(13, 187);
            dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            dateTimePickerEndDate.Size = new Size(225, 23);
            dateTimePickerEndDate.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 3);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 4;
            label1.Text = "Клиент";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 58);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 5;
            label2.Text = "Процедура";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 114);
            label3.Name = "label3";
            label3.Size = new Size(139, 15);
            label3.TabIndex = 6;
            label3.Text = "Дата начала процедуры";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 169);
            label4.Name = "label4";
            label4.Size = new Size(160, 15);
            label4.TabIndex = 7;
            label4.Text = "Дата окончания процедуры";
            // 
            // saveAppoinmentBtn
            // 
            saveAppoinmentBtn.Location = new Point(244, 276);
            saveAppoinmentBtn.Name = "saveAppoinmentBtn";
            saveAppoinmentBtn.Size = new Size(144, 23);
            saveAppoinmentBtn.TabIndex = 8;
            saveAppoinmentBtn.Text = "Сохранить";
            saveAppoinmentBtn.UseVisualStyleBackColor = true;
            saveAppoinmentBtn.Click += saveAppoinmentBtn_Click;
            // 
            // dateTimePickerStartTime
            // 
            dateTimePickerStartTime.CustomFormat = "\"dd/MM/yyyy HH:mm\"";
            dateTimePickerStartTime.Format = DateTimePickerFormat.Time;
            dateTimePickerStartTime.Location = new Point(244, 132);
            dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            dateTimePickerStartTime.ShowUpDown = true;
            dateTimePickerStartTime.Size = new Size(140, 23);
            dateTimePickerStartTime.TabIndex = 9;
            // 
            // dateTimePickerEndTime
            // 
            dateTimePickerEndTime.CustomFormat = "\"dd/MM/yyyy HH:mm\"";
            dateTimePickerEndTime.Format = DateTimePickerFormat.Time;
            dateTimePickerEndTime.Location = new Point(244, 187);
            dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            dateTimePickerEndTime.ShowUpDown = true;
            dateTimePickerEndTime.Size = new Size(140, 23);
            dateTimePickerEndTime.TabIndex = 10;
            // 
            // AppointmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 311);
            Controls.Add(dateTimePickerEndTime);
            Controls.Add(dateTimePickerStartTime);
            Controls.Add(saveAppoinmentBtn);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dateTimePickerEndDate);
            Controls.Add(dateTimePickerStartDate);
            Controls.Add(comboBoxService);
            Controls.Add(comboBoxClient);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AppointmentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Запись на процедуру";
            Load += AppointmentForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxClient;
        private ComboBox comboBoxService;
        private DateTimePicker dateTimePickerStartDate;
        private DateTimePicker dateTimePickerEndDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button saveAppoinmentBtn;
        private DateTimePicker dateTimePickerStartTime;
        private DateTimePicker dateTimePickerEndTime;
    }
}