using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class MinimumOneElementAttributeObject
    {
        [MinimumOneElement]
        public string[] TestVal { get; set; }
    }

    public class MinimumOneElementAttributeTests
    {
        private MinimumOneElementAttributeObject _validObj;
        private MinimumOneElementAttributeObject _invalidObj;

        public MinimumOneElementAttributeTests()
        {
            _validObj = new MinimumOneElementAttributeObject()
            {
                TestVal = new string[] { "Hi I am Val" }
            };

            _invalidObj = new MinimumOneElementAttributeObject()
            {
                TestVal = new string[0]
            };
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