using System;

namespace Insight.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }

    public class CustomValidationException : Exception
    {
        public CustomValidationException()
            : base()
        {
        }

        public CustomValidationException(string message)
            : base(message)
        {
        }

        public CustomValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CustomValidationException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
