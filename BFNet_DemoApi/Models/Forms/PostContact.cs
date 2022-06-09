using System.ComponentModel.DataAnnotations;

namespace BFNet_DemoApi.Models.Forms
{
    public class PostContact
    {
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
    }
}
