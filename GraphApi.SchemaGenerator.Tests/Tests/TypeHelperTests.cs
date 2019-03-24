using System.Collections.Generic;
using System.Linq;
using GraphQL.SchemaGenerator.Helpers;
using GraphQL.SchemaGenerator.Tests.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphQL.SchemaGenerator.Tests.Tests
{
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void GetFullName_With_NestedDictionary_IsSafe()
        {
            var data = new SchemaResponse()
            {
                NestDictionary = new Dictionary<string, IDictionary<string, Episode>>()
            };
            var type = data.NestDictionary.GetType();

            var sut = TypeHelper.GetFullName(type);

            var prohibitedCharacters = new List<char> {'~', ' ', ','};

            Assert.IsNotNull(sut);
            Assert.IsTrue(!sut.Any(t=> prohibitedCharacters.Contains(t)));
        }
    }
}
