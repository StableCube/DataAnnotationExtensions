using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public enum RequiredEnumAttributeEnum
    {
        Val1 = 10,
        Val2 = 20
    }

    public class RequiredEnumAttributeObject
    {
        [RequiredEnum]
        public RequiredEnumAttributeEnum TestVal { get; set; }
    }

    public class RequiredEnumAttributeTests
    {
        private RequiredEnumAttributeObject _validObj = new RequiredEnumAttributeObject()
        {
            TestVal = RequiredEnumAttributeEnum.Val1
        };

        private RequiredEnumAttributeObject _invalidObj = new RequiredEnumAttributeObject();

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