using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public enum ParsesToEnumAttributeEnum
    {
        Val1,
        Val2
    }

    public class ParsesToEnumAttributeObject
    {
        [ParsesToEnum(EnumType = typeof(ParsesToEnumAttributeEnum), IgnoreCase = true)]
        public string TestVal { get; set; }
    }

    public class ParsesToEnumAttributeTests
    {
        private ParsesToEnumAttributeObject _validObj = new ParsesToEnumAttributeObject()
        {
            TestVal = "val1"
        };

        private ParsesToEnumAttributeObject _invalidObj = new ParsesToEnumAttributeObject()
        {
            TestVal = "this is no good"
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