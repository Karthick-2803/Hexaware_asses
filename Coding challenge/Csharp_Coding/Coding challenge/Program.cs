using Coding_challenge.dao;
using Coding_challenge.Exceptions;
using Coding_challenge.model;

namespace Coding_challenge
{
    internal class Program
    {
        public class MainModule
        {
            public static void Main(string[] args)
            {
                IPolicyService policyService = new PolicyServiceImpl();

                while (true)
                {
                    Console.WriteLine("\nInsurance Management System");
                    Console.WriteLine("1. Create Policy");
                    Console.WriteLine("2. View Policy");
                    Console.WriteLine("3. View All Policies");
                    Console.WriteLine("4. Update Policy");
                    Console.WriteLine("5. Delete Policy");
                    Console.WriteLine("6. Exit");
                    Console.Write("Enter choice: ");

                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                CreateNewPolicy(policyService);
                                break;
                            case "2":
                                ViewPolicy(policyService);
                                break;
                            case "3":
                                ViewAllPolicies(policyService);
                                break;
                            case "4":
                                UpdatePolicy(policyService);
                                break;
                            case "5":
                                DeletePolicy(policyService);
                                break;
                            case "6":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch (PolicyNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error: {ex.Message}");
                    }
                }
            }

            private static void CreateNewPolicy(IPolicyService policyService)
            {
                Console.WriteLine("\nCreate New Policy");

                Policy policy = new Policy();
                Console.Write("Enter Policy ID: ");
                policy.PolicyId = int.Parse(Console.ReadLine());
                Console.Write("Enter Policy Name: ");
                policy.PolicyName = Console.ReadLine();
                Console.Write("Enter Policy Type: ");
                policy.PolicyType = Console.ReadLine();
                Console.Write("Enter Coverage Amount: ");
                policy.CoverageAmount = decimal.Parse(Console.ReadLine());

                if (policyService.CreatePolicy(policy))
                {
                    Console.WriteLine("Policy created successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to create policy.");
                }
            }

            private static void ViewPolicy(IPolicyService policyService)
            {
                Console.Write("\nEnter Policy ID to view: ");
                int policyId = int.Parse(Console.ReadLine());
                Policy policy = policyService.GetPolicy(policyId);
                Console.WriteLine($"\nPolicy Details:\n{policy}");
            }

            private static void ViewAllPolicies(IPolicyService policyService)
            {
                Console.WriteLine("\nAll Policies:");
                List<Policy> policies = policyService.GetAllPolicies();
                foreach (var policy in policies)
                {
                    Console.WriteLine(policy);
                }
            }

            private static void UpdatePolicy(IPolicyService policyService)
            {
                Console.WriteLine("\nUpdate Policy");
                Policy policy = new Policy();

                Console.Write("Enter Policy ID to update: ");
                policy.PolicyId = int.Parse(Console.ReadLine());
                Console.Write("Enter New Policy Name: ");
                policy.PolicyName = Console.ReadLine();
                Console.Write("Enter New Policy Type: ");
                policy.PolicyType = Console.ReadLine();
                Console.Write("Enter New Coverage Amount: ");
                policy.CoverageAmount = decimal.Parse(Console.ReadLine());

                if (policyService.UpdatePolicy(policy))
                {
                    Console.WriteLine("Policy updated successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to update policy.");
                }
            }

            private static void DeletePolicy(IPolicyService policyService)
            {
                Console.Write("\nEnter Policy Name to delete: ");
                string policyName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(policyName))
                {
                    Console.WriteLine("Policy name cannot be empty or null.");
                    return;
                }

                try
                {
                    bool isDeleted = policyService.DeletePolicy(policyName);

                    Console.WriteLine(isDeleted
                        ? "Policy deleted successfully!"
                        : "Failed to delete policy.");
                }
                catch (PolicyNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }
    }
}
