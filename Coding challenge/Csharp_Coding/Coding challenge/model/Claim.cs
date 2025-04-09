using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_challenge.model
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime DateFiled { get; set; }
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Approved", "Rejected"
        public Policy Policy { get; set; }
        public Client Client { get; set; }

        public Claim() { }

        public Claim(int claimId, string claimNumber, DateTime dateFiled,
                    decimal claimAmount, string status, Policy policy, Client client)
        {
            ClaimId = claimId;
            ClaimNumber = claimNumber;
            DateFiled = dateFiled;
            ClaimAmount = claimAmount;
            Status = status;
            Policy = policy;
            Client = client;
        }

        public override string ToString()
        {
            return $"ClaimID: {ClaimId}, Number: {ClaimNumber}\n" +
                   $"Filed: {DateFiled:yyyy-MM-dd}, Amount: {ClaimAmount:C}\n" +
                   $"Status: {Status}\n" +
                   $"Policy: {Policy?.PolicyName} ({Policy?.PolicyId})\n" +
                   $"Client: {Client?.ClientName} ({Client?.ClientId})";
        }
    }
}
