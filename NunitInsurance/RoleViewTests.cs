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
    public class RoleViewTests
    {
        [Test]
        public void Valid_RoleView()
        {
            // Arrange
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = "Admin"
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_Required()
        {
            // Arrange
            var roleView = new RoleView
            {
                RoleId = 1
                // Name is intentionally not set
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_MaxLength()
        {
            // Arrange
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = new string('A', 50) // Max length allowed
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(roleView));
        }

        [Test]
        public void Name_ExceedsMaxLength()
        {
            // Arrange
            var roleView = new RoleView
            {
                RoleId = 1,
                Name = new string('A', 51) // Exceeds max length
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(roleView));
        }

        private void ValidateModel(RoleView model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
        }
    }
}
