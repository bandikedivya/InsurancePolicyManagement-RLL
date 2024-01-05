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
    public class CustomerModelTests
    {
        [Test]
        public void FirstName_Required()
        {
            // Arrange
            var customer = new CustomerModel
            {
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+123456789",
                UserName = "johndoe",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customer));
        }

        [Test]
        public void LastName_Required()
        {
            // Arrange
            var customer = new CustomerModel
            {
                FirstName = "John",
                Email = "john.doe@example.com",
                PhoneNumber = "+123456789",
                UserName = "johndoe",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customer));
        }

        [Test]
        public void Email_Required()
        {
            // Arrange
            var customer = new CustomerModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+123456789",
                UserName = "johndoe",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customer));
        }

        // Add more tests for other properties (e.g., PhoneNumber, UserName, Password, ConfirmPassword) following a similar pattern.

      
        [Test]
        public void Valid_Model()
        {
            // Arrange
            var customer = new CustomerModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+123456789",
                UserName = "johndoe",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(customer));
        }

        private void ValidateModel(CustomerModel model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = ValidateModelAttributes(model, validationContext);

            if (validationResults.Length > 0)
            {
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                var errorMessage = string.Join("\n", errorMessages);
                throw new ValidationException(errorMessage);
            }
        }

        private ValidationResult[] ValidateModelAttributes(object model, ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults.ToArray();
        }
    }
}
