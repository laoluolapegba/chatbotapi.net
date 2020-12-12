using System;
using System.Collections.Generic;
using System.Text;

namespace MomoOrchestrator.Core
{
    public class AirtimeTopupRequest
    {
        public string transactionId { get; set; }
        public DateTime transactionDate { get; set; }
        public string serviceId { get; set; }
        public string msisdn { get; set; }
        public int messageType { get; set; }
        public decimal amount { get; set; }
        public string receiverMsisdn { get; set; }
    }
}
