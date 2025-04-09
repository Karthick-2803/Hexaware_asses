using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_challenge.model
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyType { get; set; }
        public decimal CoverageAmount { get; set; }

        public Policy() { }

        public Policy(int policyId, string policyName, string policyType, decimal coverageAmount)
        {
            PolicyId = policyId;
            PolicyName = policyName;
            PolicyType = policyType;
            CoverageAmount = coverageAmount;
        }

        public override string ToString()
        {
            return $"PolicyID: {PolicyId}, Name: {PolicyName}, Type: {PolicyType}, Coverage: {CoverageAmount:C}";
        }
    }
}
