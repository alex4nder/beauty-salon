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
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "BranchForm";
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Редактирование салона";

            // Название салона
            this.labelName = new Label();
            this.labelName.Text = "Название:";
            this.labelName.Location = new Point(20, 30);
            this.Controls.Add(this.labelName);

            this.textBoxName = new TextBox();
            this.textBoxName.Location = new Point(120, 30);
            this.textBoxName.Width = 200;
            this.Controls.Add(this.textBoxName);

            // Адрес
            this.labelAddress = new Label();
            this.labelAddress.Text = "Адрес:";
            this.labelAddress.Location = new Point(20, 70);
            this.Controls.Add(this.labelAddress);

            this.textBoxAddress = new TextBox();
            this.textBoxAddress.Location = new Point(120, 70);
            this.textBoxAddress.Width = 200;
            this.Controls.Add(this.textBoxAddress);

            // Телефон
            this.labelPhone = new Label();
            this.labelPhone.Text = "Телефон:";
            this.labelPhone.Location = new Point(20, 110);
            this.Controls.Add(this.labelPhone);

            this.textBoxPhone = new TextBox();
            this.textBoxPhone.Location = new Point(120, 110);
            this.textBoxPhone.Width = 200;
            this.Controls.Add(this.textBoxPhone);

            // Кнопка сохранения
            this.buttonSave = new Button();
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Location = new Point(120, 160);
            this.buttonSave.Click += new EventHandler(this.ButtonSave_Click);
            this.Controls.Add(this.buttonSave);
        }

        #endregion
    }
}