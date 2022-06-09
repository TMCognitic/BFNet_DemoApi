using System.ComponentModel.DataAnnotations;

namespace BFNet_DemoApi.Models.Forms
{
    public class PutContact
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
    }
}
