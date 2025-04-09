using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding_challenge.model;

namespace Coding_challenge.dao
{
    public interface IPolicyService
    {
        bool CreatePolicy(Policy policy);
        Policy GetPolicy(int policyId);
        List<Policy> GetAllPolicies();
        bool UpdatePolicy(Policy policy);
        bool DeletePolicy(string policyName);
    }
}
