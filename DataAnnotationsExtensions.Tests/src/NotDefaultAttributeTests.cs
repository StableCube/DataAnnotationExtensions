using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class NotDefaultAttributeObject
    {
        [NotDefault]
        public DateTimeOffset TestVal { get; set; }
    }

    public class NotDefaultAttributeTests
    {
        private NotDefaultAttributeObject _validObj;
        private NotDefaultAttributeObject _invalidObj;

        public NotDefaultAttributeTests()
        {
            _validObj = new NotDefaultAttributeObject()
            {
                TestVal = DateTimeOffset.UtcNow
            };

            _invalidObj = new NotDefaultAttributeObject();
        }

        [Fact]
        public void Should_Validate_Valid_Object()
        {
            var context = new ValidationContext(_validObj);
            var isValid = Validator.TryValidateObject(_validObj, context, null, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Should_Not_Validate_Invalid_Object()
        {
            var context = new ValidationContext(_invalidObj);
            var isValid = Validator.TryValidateObject(_invalidObj, context, null, true);

            Assert.False(isValid);
        }
    }
}