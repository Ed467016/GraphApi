using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraphApi.EFCore.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        [Description("Represents a foreign key to ProjectDB-s Project table")]
        public int ProjectId { get; set; }
    }
}
