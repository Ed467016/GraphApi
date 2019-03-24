using GraphQL.SchemaGenerator.Tests.Helpers;
using GraphQL.SchemaGenerator.Tests.Mocks;
using GraphQL.SchemaGenerator.Tests.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphQL.SchemaGenerator.Tests.Tests
{
    [TestClass]
    public class GenericQueryTests
    {
        [TestMethod]
        public void BasicExample_Works()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(GenericsSchema));

            var query = @"
                {
                    echoGenerics{data}
                }
            ";

            var expected = @"{
              echoGenerics: {
                data: """"
              }
            }";

           GraphAssert.QuerySuccess(schema, query, expected);
        }

        [TestMethod]
        public void BasicInputExample_Works()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(GenericsSchema));

            var query = @"
                {
                    echoGenerics(
                        int1:{data:2}
                        string1:{data:""test""},
                    )
                    {data}
                }
            ";

            var expected = @"{
              echoGenerics: {
                data: ""2test""
              }
            }";

            GraphAssert.QuerySuccess(schema, query, expected);
        }

        [TestMethod]
        public void BasicClassInputExample_Works()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(GenericsSchema));

            var query = @"
                {
                    echoClassGenerics{
                        list{innerInt}
                    }
                    echoClassGenerics2{
                        list{inner2Int}
                    }
                }
            ";

            var expected = @"{
              echoClassGenerics: {
                list: [{innerInt:1}]
              },
             echoClassGenerics2: {
                list: [{inner2Int:2}]
              }
            }";

            GraphAssert.QuerySuccess(schema, query, expected);
        }

        [TestMethod]
        public void Schema_HasUniqueTypes()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(GenericsSchema));

            var query = @"
                {
                    __schema{
                        queryType {
                          name,
                          fields{
                            name
                            type{
                              name
                              kind
                            }
                          }
                        }
                      }
                }
            ";

            var expected = @"{
              __schema: {
                queryType: {
                            name: ""RootQueries"",
                  fields: [
                    {
                      name: ""echoGenerics"",
                      type: {
                        name: ""EchoGeneric__String"",
                        kind: ""OBJECT""
                      }
            },
                    {
                      name: ""echoClassGenerics"",
                      type: {
                        name: ""EchoGenericList__Inner"",
                        kind: ""OBJECT""
                      }
                    },
                    {
                      name: ""echoClassGenerics2"",
                      type: {
                        name: ""EchoGenericList__Inner2"",
                        kind: ""OBJECT""
                      }
                    }
                  ]
                }
              }
            }";

            GraphAssert.QuerySuccess(schema, query, expected);
        }
    }
}
