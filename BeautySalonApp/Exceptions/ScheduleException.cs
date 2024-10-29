namespace BeautySalonApp.Exceptions
{
    public class ScheduleException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "Произошла ошибка. Пожалуйста, повторите операцию снова.";
        // Default constructor
        public ScheduleException() : base(DEFAULT_ERROR_MESSAGE) { }
        // Constructor that accepts a custom message
        public ScheduleException(string message) : base(message) { }

        // Constructor that accepts a custom message and an inner exception
        public ScheduleException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class ScheduleDateConflictException : ScheduleException
    {
        private const string CREATE_OR_UPDATE_ERROR_DUE_TO_DATE = "Рабочий график для данного сотрудника на этот день уже определен.";
        public ScheduleDateConflictException() : base(CREATE_OR_UPDATE_ERROR_DUE_TO_DATE) { }

    }
}
