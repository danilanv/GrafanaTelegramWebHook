using System;
using System.Collections.Generic;

namespace GrafanaTelegramWebHook.Models
{
    public class GrafanaRequest
    {
        public string Title { get; set; }

        public long RuleId { get; set; }

        public string RuleName { get; set; }
        public string RuleUrl { get; set; }
        public string State { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public List<EvalMatch> EvalMatches { get; set; }
    }

    public class EvalMatch
    {
        public string Metric { get; set; }
        public Tags Tags { get; set; }
        public object Value { get; set; }
    }

    public class Tags
    {
    }
}