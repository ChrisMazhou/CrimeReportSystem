using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TCR.Lib.BL;

namespace CrimeReportSystem.BL.Entities.Logging
{
    public class SystemLog
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime EventTime { get; set; }

        public string Sender { get; set; }

        public long? UserIdentityId { get; set; }

        public LogEventType EventType { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string InnerException { get; set; }

        public string InnerExceptionStackTrace { get; set; }

    }
}