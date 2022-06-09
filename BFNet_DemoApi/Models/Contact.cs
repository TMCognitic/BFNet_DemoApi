using System.ComponentModel.DataAnnotations;

namespace BFNet_DemoApi.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }
}
