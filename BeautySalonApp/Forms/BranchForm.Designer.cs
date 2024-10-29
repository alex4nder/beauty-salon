namespace BeautySalonApp.Forms
{
    partial class BranchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchForm));
            labelName = new Label();
            textBoxName = new TextBox();
            labelAddress = new Label();
            textBoxAddress = new TextBox();
            labelPhone = new Label();
            textBoxPhone = new TextBox();
            buttonSave = new Button();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.Location = new Point(20, 30);
            labelName.Name = "labelName";
            labelName.Size = new Size(100, 23);
            labelName.TabIndex = 0;
            labelName.Text = "Название:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(120, 30);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(268, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelAddress
            // 
            labelAddress.Location = new Point(20, 70);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(100, 23);
            labelAddress.TabIndex = 2;
            labelAddress.Text = "Адрес:";
            // 
            // textBoxAddress
            // 
            textBoxAddress.Location = new Point(120, 70);
            textBoxAddress.Name = "textBoxAddress";
            textBoxAddress.Size = new Size(268, 23);
            textBoxAddress.TabIndex = 3;
            // 
            // labelPhone
            // 
            labelPhone.Location = new Point(20, 110);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(100, 23);
            labelPhone.TabIndex = 4;
            labelPhone.Text = "Телефон:";
            // 
            // textBoxPhone
            // 
            textBoxPhone.Location = new Point(120, 110);
            textBoxPhone.Name = "textBoxPhone";
            textBoxPhone.Size = new Size(268, 23);
            textBoxPhone.TabIndex = 5;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(120, 170);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(268, 31);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Сохранить";
            buttonSave.Click += ButtonSave_Click;
            // 
            // BranchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(400, 229);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Controls.Add(labelAddress);
            Controls.Add(textBoxAddress);
            Controls.Add(labelPhone);
            Controls.Add(textBoxPhone);
            Controls.Add(buttonSave);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BranchForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Редактирование салона";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}