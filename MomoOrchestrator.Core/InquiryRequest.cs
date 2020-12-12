using System;
using System.Collections.Generic;
using System.Text;

namespace MomoOrchestrator.Core
{
    public class InquiryRequest
    {
        public string transactionId { get; set; }
        public DateTime transactionDate { get; set; }
        public string serviceId { get; set; }
        public string msisdn { get; set; }
        public int numoftransactions { get; set; }
        public int messageType { get; set; }
        public string conversationID { get; set; }
    }
}
