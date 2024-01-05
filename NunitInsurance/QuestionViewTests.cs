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
    public class QuestionViewTests
    {
        [Test]
        public void QuestionId_NotRequired()
        {
            // Arrange
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void Question_LengthWithinLimit()
        {
            // Arrange
            var questionView = new QuestionView
            {
                Question = new string('A', 255), // Max length allowed
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void Date_DefaultsToCurrentDate()
        {
            // Arrange
            var questionView = new QuestionView
            {
                Question = "Test question",
                Answer = "Test answer",
                CustomerId = 123
            };

            // Act
            DateTime currentDate = DateTime.Now;

            // Assert
            Assert.That(questionView.Date.Date, Is.EqualTo(currentDate.Date));
        }

        [Test]
        public void Answer_LengthWithinLimit()
        {
            // Arrange
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = new string('A', 255), // Max length allowed
                CustomerId = 123
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }

        [Test]
        public void CustomerId_Required()
        {
            // Arrange
            var questionView = new QuestionView
            {
                Question = "Test question",
                Date = DateTime.Now,
                Answer = "Test answer",
                CustomerId = 123 // Provide a valid CustomerId
            };

            // Act & Assert
            Assert.DoesNotThrow(() => ValidateModel(questionView));
        }


        private void ValidateModel(QuestionView model)
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
