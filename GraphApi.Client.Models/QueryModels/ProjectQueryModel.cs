using System;
using System.Collections.Generic;

namespace GraphApi.Client.Models.QueryModels
{
    public class ProjectQueryModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartingDate { get; set; }
        public List<UserQueryModel> Users { get; set; }
    }
}
