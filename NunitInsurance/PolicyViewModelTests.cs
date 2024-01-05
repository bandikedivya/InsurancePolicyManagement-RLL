using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace NunitInsurance
{
    [TestFixture]
    public class PolicyViewModelTests
    {
        [Test]
        public void PolicyNumber_Required()
        {
            // Arrange
            var policyViewModel = new PolicyViewModel
            {
                AppliedDate = DateTime.Now,
                Category = "Health"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(policyViewModel));
        }

        [Test]
        public void AppliedDate_Required()
        {
            // Arrange
            var policyViewModel = new PolicyViewModel
            {
                PolicyNumber = "ABC123",
                Category = "Auto",
                AppliedDate = DateTime.Now // Provide a valid AppliedDate
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(policyViewModel));
        }

        [Test]
        public void Valid_Model()
        {
            // Arrange
            var policyViewModel = new PolicyViewModel
            {
                PolicyNumber = "ABC123",
                AppliedDate = DateTime.Now,
                Category = "Life"
            };

            // Act & Assert
            Assert.That(() => ValidateModel(policyViewModel), Throws.Nothing);
        }

        private void ValidateModel(PolicyViewModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
    }
}
