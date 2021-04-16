using System;

namespace ASCore.Utilities.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetDetailedMessage(this Exception exception) =>
            exception.InnerException is not null
                ? exception.Message + Environment.NewLine + exception.InnerException.Message
                : exception.Message;
    }
}
