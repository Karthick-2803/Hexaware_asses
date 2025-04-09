using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding_challenge.Exceptions;
using Coding_challenge.model;
using Coding_challenge.Util;
using Microsoft.Data.SqlClient;

namespace Coding_challenge.dao
{

    public class PolicyServiceImpl : IPolicyService
    {
        public bool CreatePolicy(Policy policy)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO Policy 
                             (policyId, policyName, PolicyType, coverageAmount)
                             VALUES 
                             (@PolicyId, @PolicyName, @PolicyType, @CoverageAmount)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PolicyId", policy.PolicyId);
                        cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                        cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType);
                        cmd.Parameters.AddWithValue("@CoverageAmount", policy.CoverageAmount);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex) //when (ex.Number == 2627) 
                {
                    throw new PolicyNotFoundException($"Policy with ID {policy.PolicyId} already exists.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating policy: {ex.Message}");
                    throw;
                }
            }
        }

        public Policy GetPolicy(int policyId)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                try
                {
                    string query = "SELECT * FROM Policy WHERE policyId = @PolicyId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PolicyId", policyId);
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new PolicyNotFoundException($"Policy with ID {policyId} not found.");
                            }

                            return new Policy(
                                (int)reader["policyId"],
                                reader["policyName"].ToString(),
                                reader["PolicyType"].ToString(),
                                (decimal)reader["coverageAmount"]
                            );
                        }
                    }
                }
                catch (PolicyNotFoundException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving policy: {ex.Message}");
                    throw;
                }
            }
        }

        public List<Policy> GetAllPolicies()
        {
            List<Policy> policies = new List<Policy>();

            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                try
                {
                    string query = "SELECT * FROM Policy";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                policies.Add(new Policy(
                                    (int)reader["policyId"],
                                    reader["policyName"].ToString(),
                                    reader["PolicyType"].ToString(),
                                    (decimal)reader["coverageAmount"]
                                ));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving policies: {ex.Message}");
                    throw;
                }
            }

            return policies;
        }
        public bool UpdatePolicy(Policy policy)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                try
                {
                    string query = @"UPDATE Policy SET 
                             policyName = @PolicyName,
                             PolicyType = @PolicyType,
                             coverageAmount = @CoverageAmount
                             WHERE policyId = @PolicyId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PolicyId", policy.PolicyId);
                        cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                        cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType);
                        cmd.Parameters.AddWithValue("@CoverageAmount", policy.CoverageAmount);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new PolicyNotFoundException($"Policy with ID {policy.PolicyId} not found.");
                        }

                        return true;
                    }
                }
                catch (PolicyNotFoundException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating policy: {ex.Message}");
                    throw;
                }
            }
        }

        public bool DeletePolicy(string policyName)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                try
                {
                    string query = "DELETE FROM Policy WHERE policyName = @PolicyName";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PolicyName", policyName);
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new PolicyNotFoundException($"Policy with name '{policyName}' not found.");
                        }

                        return true;
                    }
                }
                catch (PolicyNotFoundException)
                {
                    throw;
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    throw new PolicyNotFoundException($"Policy '{policyName}' cannot be deleted as it is referenced by other records.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting policy: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
