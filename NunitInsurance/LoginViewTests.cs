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
    public class LoginViewTests
    {
        [Test]
        public void UserName_Required()
        {
            // Arrange
            var loginView = new LoginView { Password = "TestPassword" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(loginView));
        }

        [Test]
        public void Password_Required()
        {
            // Arrange
            var loginView = new LoginView { UserName = "TestUser" };

            // Act & Assert
            Assert.Throws<ValidationException>(() => ValidateModel(loginView));
        }

        [Test]
        public void Valid_Model()
        {
            // Arrange
            var loginView = new LoginView { UserName = "TestUser", Password = "TestPassword" };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(loginView));
        }

        private void ValidateModel(LoginView model)
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
    }  }
