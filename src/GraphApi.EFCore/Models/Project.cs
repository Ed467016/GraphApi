using System;
using System.ComponentModel.DataAnnotations;

namespace GraphApi.EFCore.Models
{
    public class Project 
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartingDate { get; set; }
    }
}
