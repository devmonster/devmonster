using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devmonster.me.Models
{
    public class VoiceWebhookResponse
    {
        public string speech { get; set; }
        public string displayText { get; set; }
        public string data { get; set; }
        public string[] contextOut { get; set; }
        public string source { get; set; }
    }
}