using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class StringRangeAttributeObject
    {
        [StringRange(AllowableValues = new [] { "Val1", "Val2" }, IgnoreCase=true)]
        public string StringData { get; set; }
    }

    public class StringRangeAttributeTests
    {
        private StringRangeAttributeObject _validObj = new StringRangeAttributeObject()
        {
            StringData = "Val1"
        };

        private StringRangeAttributeObject _invalidObj = new StringRangeAttributeObject()
        {
            StringData = "SomethingIsNotRight"
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