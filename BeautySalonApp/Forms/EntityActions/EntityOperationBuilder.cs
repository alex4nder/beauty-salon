namespace BeautySalonApp.Forms.EntityActions
{
    public class EntityActionConfigurator<T>
    {
        private Func<T, Form> _createForm;
        private Action<T> _updateAction;
        private Action<Guid> _removeAction;
        private Action _loadData;

        public EntityActionConfigurator<T> WithFormCreator(Func<T, Form> createForm)
        {
            _createForm = createForm;
            return this;
        }

        public EntityActionConfigurator<T> WithUpdateAction(Action<T> updateAction)
        {
            _updateAction = updateAction;
            return this;
        }

        public EntityActionConfigurator<T> WithLoadData(Action loadData)
        {
            _loadData = loadData;
            return this;
        }

        public EntityActionConfigurator<T> WithRemoveAction(Action<Guid> removeAction)
        {
            _removeAction = removeAction;
            return this;
        }

        public void ExecuteEdit(T entity)
        {
            if (entity != null)
            {
                using (var entityForm = _createForm(entity))
                {
                    if (entityForm.ShowDialog() == DialogResult.OK)
                    {
                        _updateAction(entity);
                        _loadData();
                    }
                }
            }
            else
            {
                ShowErrorMessage("Запись не найдена");
            }
        }

        public void ExecuteDelete(Guid entityId)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _removeAction(entityId);
                    _loadData();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка при удалении записи: {ex.Message}");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
