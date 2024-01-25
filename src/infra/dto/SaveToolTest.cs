using System.ComponentModel.DataAnnotations;
using NUnit.Framework;




namespace Testes.src.infra.dto
{
    public class SaveToolDtoInfraTest
    {
        [Test]
        [Category("unit")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ItShouldValidateAndNotThrow(int tagCount)
        {
            // Arrange
            var dto = new SaveToolDtoInfra
            {
                link = "https://example.com",
                description = "Valid description",
                title = "Valid title",
                tags = []
            };
            for (int i = 0; i < tagCount; i++)
            {
                dto.tags.Add("tag" + i);
            }

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true);

            // Assert
            Assert.IsTrue(isValid, "Validation should pass for a valid DTO");
        }

        [Test]
        [Category("unit")]
        public void ItShouldThrowErrorBecauseTagListIsEmptyOrNull()
        {
            // Arrange
            var dto = new SaveToolDtoInfra
            {
                link = "https://example.com",
                description = "Valid description",
                title = "Valid title",
                tags = new List<string> { "" }
            };
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(isValid, Is.False, "Validation should fail for an invalid DTO");
                Assert.That(validationResults, Has.Count.EqualTo(1), "All validation errors should be captured");
            });
        }

        [Test]
        [Category("unit")]
        public void ValidationShouldFailedBecauseObjectIsNull()
        {
            // Arrange
            var dto = new SaveToolDtoInfra
            {
                // Missing required fields
            };
            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(isValid, Is.False, "Validation should fail for an invalid DTO");
                Assert.That(validationResults, Has.Count.EqualTo(4), "All validation errors should be captured");
            });
        }
    }
}