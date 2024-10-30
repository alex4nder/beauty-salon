namespace BeautySalonApp.Exceptions
{
    public class ScheduleException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "��������� ������. ����������, ��������� �������� �����.";
        public ScheduleException() : base(DEFAULT_ERROR_MESSAGE) { }
        public ScheduleException(string message) : base(message) { }
        public ScheduleException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class ScheduleDateConflictException : ScheduleException
    {
        private const string CREATE_OR_UPDATE_ERROR_DUE_TO_DATE = "������� ������ ��� ������� ���������� �� ���� ���� ��� ���������.";
        public ScheduleDateConflictException() : base(CREATE_OR_UPDATE_ERROR_DUE_TO_DATE) { }

    }
}
