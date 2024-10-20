namespace BeautySalonApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridViewSalons = new DataGridView();
            label1 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalons).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewSalons
            // 
            dataGridViewSalons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSalons.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewSalons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSalons.Location = new Point(12, 30);
            dataGridViewSalons.Name = "dataGridViewSalons";
            dataGridViewSalons.Size = new Size(740, 328);
            dataGridViewSalons.TabIndex = 0;
            dataGridViewSalons.CellClick += dataGridViewSalons_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(447, 15);
            label1.TabIndex = 1;
            label1.Text = "Для продолжения работы с приложением выберите один из филиалов салона:";
            // 
            // button1
            // 
            button1.Location = new Point(659, 364);
            button1.Name = "button1";
            button1.Size = new Size(93, 23);
            button1.TabIndex = 2;
            button1.Text = "Закрыть";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(764, 395);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(dataGridViewSalons);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Салон красоты - Главная";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewSalons;
        private Label label1;
        private Button button1;
    }
}
