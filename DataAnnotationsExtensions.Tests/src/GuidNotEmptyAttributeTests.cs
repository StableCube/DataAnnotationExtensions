using System;
using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class GuidNotEmptyAttributeObject
    {
        [GuidNotEmpty]
        public Guid TestVal { get; set; }
    }

    public class GuidNotEmptyAttributeTests
    {
        private GuidNotEmptyAttributeObject _validObj;
        private GuidNotEmptyAttributeObject _invalidObj;

        public GuidNotEmptyAttributeTests()
        {
            _validObj = new GuidNotEmptyAttributeObject()
            {
                TestVal = Guid.NewGuid()
            };

            _invalidObj = new GuidNotEmptyAttributeObject();
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