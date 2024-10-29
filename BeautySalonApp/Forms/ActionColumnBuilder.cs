namespace BeautySalonApp.Forms
{
    internal class ActionColumnBuilder
    {
        public static void addActionColumns(DataGridView dataGridView, Action<object, DataGridViewCellEventArgs> cellClickHandler)
        {
            if (!dataGridView.Columns.Contains("Edit") && !dataGridView.Columns.Contains("Delete"))
            {
                dataGridView.CellContentClick -= DataGridView_CellContentClickWrapper;
                dataGridView.CellContentClick += DataGridView_CellContentClickWrapper;
            }

            if (!dataGridView.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Редактировать",
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(editButtonColumn);
            }

            if (!dataGridView.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    HeaderText = "",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(deleteButtonColumn);
            }

            dataGridView.Columns["Edit"].Width = 100;
            dataGridView.Columns["Delete"].Width = 100;

            void DataGridView_CellContentClickWrapper(object sender, DataGridViewCellEventArgs e)
            {
                cellClickHandler?.Invoke(sender, e);
            }
        }
    }
}
