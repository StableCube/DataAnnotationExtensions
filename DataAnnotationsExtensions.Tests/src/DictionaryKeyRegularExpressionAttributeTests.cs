using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace System.ComponentModel.DataAnnotations.Tests
{
    public class DictionaryRegularExpressionAttributeObject
    {
        [DictionaryRegularExpression("([a-zA-Z0-9_-]+)")]
        public Dictionary<string, string> TestVal { get; set; }
    }

    public class DictionaryKeyRegularExpressionAttributeTests
    {
        private DictionaryRegularExpressionAttributeObject _validObj = new DictionaryRegularExpressionAttributeObject()
        {
            TestVal = new Dictionary<string, string>()
            {
                { "this-is-fine", "this-is-fine" }
            }
        };

        private DictionaryRegularExpressionAttributeObject _invalidObj = new DictionaryRegularExpressionAttributeObject()
        {
            TestVal = new Dictionary<string, string>()
            {
                { "this is bad!", "this is bad!" }
            }
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