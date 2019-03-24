using System;
using System.Collections.Generic;
using System.Text;
using GraphApi.Client.Models.Schemas;
using GraphQL.SchemaGenerator.Tests.Helpers;
using GraphQL.SchemaGenerator.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphQL.SchemaGenerator.Tests.Tests
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void QueryProject_Works()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(UserSchema));

            var query = @"{
                getUser {
                   firstName
                }
            }";

            var expected = @"{
                getUser:{
                                firstName: ""Hakob""
                        }
            }";

            GraphAssert.QuerySuccess(schema, query, expected);
        }

        [TestMethod]
        public void GetJoinedData_Works()
        {
            var schemaGenerator = new SchemaGenerator(new MockServiceProvider());
            var schema = schemaGenerator.CreateSchema(typeof(UserSchema));

            var query = @"{
                getProjects {
                    projectName,
                    users {
                        firstName
                    }
                }
            }";

            var expected = @"{
            getProjects:
            [
                {   
                    projectName: ""Alpha"",
                    users:
                    [
                        { firstName:""Tom""}
                    ]
                },
                {
                    projectName:""Betta"",
                    users:
                    [
                        {
                            firstName:""Alice""
                        }
                    ]
                },
                {
                  projectName:""Gamma"",
                  users:
                  [
                    {
                        firstName:""Sam""
                    }
                  ]
                 }
            ]
            }";


            GraphAssert.QuerySuccess(schema, query, expected);
        }
    }
}