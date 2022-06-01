using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class DateYearRangeAttributeObject
    {
        [DateYearRange(1982, 1992)]
        public DateTime DateTimeData { get; set; }
    }

    public class DateYearRangeAttributeTests
    {
        private DateYearRangeAttributeObject _validObj = new DateYearRangeAttributeObject()
        {
            DateTimeData = new DateTime(1989, 1, 1)
        };

        private DateYearRangeAttributeObject _invalidObj = new DateYearRangeAttributeObject()
        {
            DateTimeData = new DateTime(1945, 1, 1)
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