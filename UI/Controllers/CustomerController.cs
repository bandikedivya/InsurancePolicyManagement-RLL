using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using DAL;
using UI.Models;

//this controller manages various customer-related tasks such as displaying customer information, applying for policies, viewing applied policies, asking questions, and displaying categories.
namespace UI.Controllers
{
    public class CustomerController : Controller
        //controller named CustomerController for managing interactions related to customers
    {
        private InsuranceDbContext dbContext;  //Constructer="InsuranceDbContext" which is responsible for interacting with the database
        public CustomerController()
        {
            dbContext = new InsuranceDbContext(); // Initialize your DbContext
        }
        // GET: Customer
        public ActionResult Dashboard()  //These r the actions for displaying information 
        {
            return View();  //Returns a view related to the customer dashboard.
        }

        public ActionResult GetAllCustomers() //These r the actions for displaying information 
        {
            var customers = dbContext.Customers.ToList();  //Retrieves all customers from the database and displays them.
            return View(customers);
        }

        // Action method to get all users
        public ActionResult GetAllUsers()    //These r the actions for displaying information
        {
            var users = dbContext.Customers.ToList(); // Retrieves all users and displays them.
            return View(users);
        }




        public ActionResult ViewPoliciesToApply()   //These r the actions for displaying information
        {
            List<Policy> policies = dbContext.Policies.ToList();  //Retrieves all available policies from the database and displays them.
            return View(policies);
        }

        
        public ActionResult Apply(int policyId)   //Action for applying policies
        {
            
            int customerId = 1;

            // Check if the customer has already applied for the policy
            bool alreadyApplied = dbContext.AppliedPolicies
                .Any(ap => ap.CustomerId == customerId && ap.AppliedPolicyId == policyId);

            if (!alreadyApplied)
            {
                // Retrieve the policy details
                Policy policy = dbContext.Policies.FirstOrDefault(p => p.PolicyId == policyId);

                if (policy != null)
                {
                    // Create an AppliedPolicy object
                    AppliedPolicy appliedPolicy = new AppliedPolicy
                    {
                        PolicyNumber = policy.PolicyNumber,
                        AppliedDate = DateTime.Now,
                        Category = policy.Category,
                        CustomerId = customerId
                    };

                    // Add the applied policy to the database
                    dbContext.AppliedPolicies.Add(appliedPolicy);
                    dbContext.SaveChanges();
                }
                else
                {
                    // Handle the case where the policy with the specified ID doesn't exist
                    // You might want to add logging or return an appropriate response to the user.
                }

            }

            // Redirect to the action that shows applied policies
            return RedirectToAction("AppliedPolicies");
        }


        public ActionResult AppliedPolicies()   //Actions for displaying Applied Policies. "Retrieves policies that a specific customer has applied for and displays them."
        {
            List<AppliedPolicy> appliedPolicies;   //Declares a list of AppliedPolicy objects named appliedPolicies. This list will hold the applied policies retrieved from the database.

            using (var dbContext = new InsuranceDbContext())
            {
                // Retrieve applied policies from the database
                appliedPolicies = dbContext.AppliedPolicies.ToList(); //this line retrieves all applied policies from the AppliedPolicies table in the database and converts them to a list
            }

            return View(appliedPolicies);
        }




        public ActionResult Categories()  //Action for Displaying Categories. "Retrieves insurance categories from the database and displays them."
        {
            var categories = dbContext.Categories.ToList();
            return View(categories);
        }


        

        public ActionResult AskQuestion()   //This is Actions for Asking Questions : Displays a form for customers to ask questions.
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AskQuestion(QuestionView questionView)  //AskQuestion [HttpPost] Action: Handles the submission of questions.  
          
        {
            if (ModelState.IsValid)
            {
                // Create a new Questions entity
                Questions newQuestion = new Questions
                {
                    Question = questionView.Question,
                    Date = questionView.Date,
                    Answer = questionView.Answer,
                    CustomerId = questionView.CustomerId
                };

                // Add the new question to the database
                dbContext.Questions.Add(newQuestion);
                dbContext.SaveChanges();

                // Redirect to a success page or display a success message
                return RedirectToAction("Success");
            }

            // If ModelState is not valid, return to the AskQuestion view with the validation errors
            return View(questionView);
        }


        public ActionResult Success() //Displays a success page.
        {
            return View();
        }

   

        protected override void Dispose(bool disposing)   //This is a Method, it ensures proper disposal of resources, particularly the InsuranceDbContext, when the controller is disposed.
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }




        public ActionResult AskCustomerId()  //Action for Displaying Questions by Customer ID
        {
            return View();
        }

        // Displays a form to input a customer ID.
        [HttpPost]
        public ActionResult DisplayQuestionsByCustomerId(int? customerId)  //Action, Retrieves and displays questions associated with the specified customer ID.
        {
            // Check if customerId is null
            if (!customerId.HasValue)
            {
                // Handle the case when customerId is null, for example, redirect to an error page or return a specific view
                return RedirectToAction("Error");
            }

            // Retrieve all questions associated with the specified customerId
            var questions = dbContext.Questions.Where(q => q.CustomerId == customerId.Value).ToList();

            // Pass the list of questions and customer ID to the view
            ViewBag.CustomerId = customerId.Value;
            return View(questions);
        }

    }

}
