using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devmonster.me.Models
{


    public class VoiceWebhookQuery
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public Result result { get; set; }
        public Status status { get; set; }
        public string sessionId { get; set; }
    }

    public class Result
    {
        public string source { get; set; }
        public string resolvedQuery { get; set; }
        public string action { get; set; }
        public bool actionIncomplete { get; set; }
        public Parameters parameters { get; set; }
        public object[] contexts { get; set; }
        public Metadata metadata { get; set; }
        public Fulfillment fulfillment { get; set; }
        public float score { get; set; }
    }

    public class Parameters
    {
        public string Pokemon { get; set; }
        public string Region { get; set; }
    }

    public class Metadata
    {
        public string intentId { get; set; }
        public string webhookUsed { get; set; }
        public string webhookForSlotFillingUsed { get; set; }
        public string intentName { get; set; }
    }

    public class Fulfillment
    {
        public string source { get; set; }
        public string displayText { get; set; }
        public object[] messages { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string errorType { get; set; }


    }
}