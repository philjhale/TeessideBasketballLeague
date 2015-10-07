using System;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Error : Entity
    {
        public virtual string Message { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual DateTime DateStamp { get; set; }
        public virtual string Username { get; set; }

        public Error() {}

        public Error(string message, string stackTrace, string username)
        {
            Message = message;
            StackTrace = stackTrace;
            Username = username;
            DateStamp = DateTime.Now;
        }
    }
}

