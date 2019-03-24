namespace GraphApi.Client.Models.QueryModels
{
    public class UserQueryModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int ProjectId { get; set; }
    }
}