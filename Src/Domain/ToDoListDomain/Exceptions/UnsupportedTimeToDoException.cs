using System;

namespace Domain.ToDoListDomain.Exceptions
{
    public class UnsupportedTimeToDoException : Exception
    {
        public UnsupportedTimeToDoException()
            : base($"Time to do can not be for the past.")
        {
        }
    }
}
