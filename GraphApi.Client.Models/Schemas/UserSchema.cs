using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphApi.Client.Models.QueryModels;
using GraphQL.SchemaGenerator.Attributes;
using GraphApi.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GraphApi.Client.Models.Schemas
{
    [GraphType]
    public class UserSchema
    {
        private MasterDbContext masterDbContext;
        private ProjectDbContext projectDbContext;

        public UserSchema(MasterDbContext masterDb, ProjectDbContext projectDb)
        {
            this.masterDbContext = masterDb;
            this.projectDbContext = projectDb;
        }

        public UserSchema() {}

        [GraphRoute]
        public async Task<ProjectQueryModel> Project(int id)
        {
            var users = this.masterDbContext.Users.GroupBy(u => u.ProjectId);
            var project = await this.projectDbContext.Projects.FirstOrDefaultAsync(q => q.Id == id);

            if (project != null)
            {
                var result = new ProjectQueryModel
                {
                    ProjectId = project.Id,
                    ProjectName = project.ProjectName,
                    StartingDate = project.StartingDate,
                    Users = (await users?.FirstOrDefaultAsync(q => q.Key == project.Id)).Select(u =>
                    new UserQueryModel
                    {
                        UserId = u.UserId,
                        Address = u.Address,
                        Age = u.Age,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        ProjectId = u.ProjectId
                    }).ToList()
                };

                return result;
            }

            return null;
        }

        [GraphRoute]
        public List<ProjectQueryModel> Projects()
        {
            var projects = this.projectDbContext.Projects.ToList();

            if (projects == null) return null;

            var queryModels = new List<ProjectQueryModel>();
            var users = this.masterDbContext.Users.GroupBy(u => u.ProjectId).ToList();
            if (users == null)
                return null;
            foreach (var userGroup in users.ToList())
            {
                var project = projects.FirstOrDefault(p => p.Id == userGroup.Key);

                if (project != null)
                {
                    queryModels.Add(new ProjectQueryModel
                    {
                        ProjectId = project.Id,
                        ProjectName = project.ProjectName,
                        StartingDate = project.StartingDate,
                        Users = userGroup.Select(u => new UserQueryModel
                        {
                            UserId = u.UserId,
                            Address = u.Address,
                            Age = u.Age,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            ProjectId = u.ProjectId
                        }).ToList()
                    });
                }
            }

            return queryModels;
        }

        [GraphRoute]
        public UserQueryModel GetUser()
        {
            return new UserQueryModel
            {
                UserId = 1,
                Address = "Address",
                FirstName = "Hakob",
                LastName = "Frangulyan"
            };
        }
    }
}