using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GraphQL.Execution;
using GraphQL.SchemaGenerator.Tests.Helpers;
using GraphQL.SchemaGenerator.Tests.Mocks;
using GraphQL.SchemaGenerator.Tests.Schemas;
using GraphQL.Validation;
using GraphQL.Validation.Complexity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphQL.SchemaGenerator.Tests.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public async Task LargeLists_Perform_UnderThreshold()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());

            var schema = schemaGenerator.CreateSchema(typeof(PerformanceSchema));

            var query = @"{
                  testList{
                    date
                    enum
                    value
                    nullValue
                    decimalValue
                    timeSpan
                    byteArray
                    stringValue
                    values{
                        value{
                            complicatedResponse{
                            echo
                            data
                            }
                        }
                    }
                 }
            }";

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var result = await DocumentOperations.ExecuteOperationsAsync(schema, null, query, validate:false);
            
            stopwatch.Stop();


            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 2);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public async Task Methods_Perform_Async()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());

            var schema = schemaGenerator.CreateSchema(typeof(PerformanceSchema));

            var query = @"{
                 slow1:slowCall{
                    date                   
                 }
                 slow2:slowCall{
                    date                   
                 }
                 slow3:slowCall{
                    date                   
                 }

            }";

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var result = await DocumentOperations.ExecuteOperationsAsync(schema, null, query, validate: false);

            stopwatch.Stop();


            Assert.IsTrue(stopwatch.Elapsed.TotalSeconds < 2);
            Assert.IsNull(result.Errors);
        }

    }
}
