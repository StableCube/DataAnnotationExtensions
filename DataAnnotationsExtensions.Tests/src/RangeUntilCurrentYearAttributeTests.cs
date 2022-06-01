using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class RangeUntilCurrentYearAttributeObject
    {
        [RangeUntilCurrentYear(1945)]
        public int TestVal { get; set; }
    }

    public class RangeUntilCurrentYearAttributeTests
    {
        private RangeUntilCurrentYearAttributeObject _validObj = new RangeUntilCurrentYearAttributeObject()
        {
            TestVal = 1982
        };

        private RangeUntilCurrentYearAttributeObject _invalidObj = new RangeUntilCurrentYearAttributeObject()
        {
            TestVal = 1932
        };

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