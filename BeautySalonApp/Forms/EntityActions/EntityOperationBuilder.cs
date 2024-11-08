﻿namespace BeautySalonApp.Forms.EntityActions
{
    public class EntityOperationBuilder<T>
    {
        private Func<T, Form> _createForm;
        private Action<T> _updateAction;
        private Action<int> _removeAction;
        private Action _loadData;

        public EntityOperationBuilder<T> WithFormCreator(Func<T, Form> createForm)
        {
            _createForm = createForm;
            return this;
        }

        public EntityOperationBuilder<T> WithUpdateAction(Action<T> updateAction)
        {
            _updateAction = updateAction;
            return this;
        }

        public EntityOperationBuilder<T> WithLoadData(Action loadData)
        {
            _loadData = loadData;
            return this;
        }

        public EntityOperationBuilder<T> WithRemoveAction(Action<int> removeAction)
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
                ShowErrorMessage("Сущность не найдена");
            }
        }

        public void ExecuteDelete(int entityId)
        {
            if (MessageBox.Show("Вы действительно хотите удалить сущность?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _removeAction(entityId);
                    _loadData();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка при удалении сущности: {ex.Message}");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
