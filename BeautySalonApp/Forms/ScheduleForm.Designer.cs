namespace BeautySalonApp.Forms
{
    partial class ScheduleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleForm));
            scheduleDatePicker = new DateTimePicker();
            startTimePicker = new DateTimePicker();
            endTimePicker = new DateTimePicker();
            scheduleDateLabel = new Label();
            label2 = new Label();
            label3 = new Label();
            scheduleSaveBtn = new Button();
            SuspendLayout();
            // 
            // scheduleDatePicker
            // 
            scheduleDatePicker.Location = new Point(185, 52);
            scheduleDatePicker.Name = "scheduleDatePicker";
            scheduleDatePicker.Size = new Size(227, 23);
            scheduleDatePicker.TabIndex = 0;
            // 
            // startTimePicker
            // 
            startTimePicker.Format = DateTimePickerFormat.Time;
            startTimePicker.Location = new Point(185, 108);
            startTimePicker.Name = "startTimePicker";
            startTimePicker.Size = new Size(227, 23);
            startTimePicker.TabIndex = 1;
            // 
            // endTimePicker
            // 
            endTimePicker.Format = DateTimePickerFormat.Time;
            endTimePicker.Location = new Point(185, 171);
            endTimePicker.Name = "endTimePicker";
            endTimePicker.Size = new Size(227, 23);
            endTimePicker.TabIndex = 2;
            // 
            // scheduleDateLabel
            // 
            scheduleDateLabel.AutoSize = true;
            scheduleDateLabel.Location = new Point(12, 58);
            scheduleDateLabel.Name = "scheduleDateLabel";
            scheduleDateLabel.Size = new Size(32, 15);
            scheduleDateLabel.TabIndex = 3;
            scheduleDateLabel.Text = "Дата";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 114);
            label2.Name = "label2";
            label2.Size = new Size(128, 15);
            label2.TabIndex = 4;
            label2.Text = "Время начала работы";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 179);
            label3.Name = "label3";
            label3.Size = new Size(149, 15);
            label3.TabIndex = 5;
            label3.Text = "Время окончания работы";
            // 
            // scheduleSaveBtn
            // 
            scheduleSaveBtn.BackColor = SystemColors.ButtonHighlight;
            scheduleSaveBtn.Location = new Point(158, 243);
            scheduleSaveBtn.Name = "scheduleSaveBtn";
            scheduleSaveBtn.Size = new Size(113, 33);
            scheduleSaveBtn.TabIndex = 6;
            scheduleSaveBtn.Text = "Подтвердить";
            scheduleSaveBtn.UseVisualStyleBackColor = false;
            scheduleSaveBtn.Click += scheduleSaveBtn_Click;
            // 
            // ScheduleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(437, 288);
            Controls.Add(scheduleSaveBtn);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(scheduleDateLabel);
            Controls.Add(endTimePicker);
            Controls.Add(startTimePicker);
            Controls.Add(scheduleDatePicker);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ScheduleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ScheduleForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker scheduleDatePicker;
        private DateTimePicker startTimePicker;
        private DateTimePicker endTimePicker;
        private Label scheduleDateLabel;
        private Label label2;
        private Label label3;
        private Button scheduleSaveBtn;
    }
}