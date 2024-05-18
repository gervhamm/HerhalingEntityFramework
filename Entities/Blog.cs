using System.ComponentModel.DataAnnotations;

namespace HerhalingEntityFramework.Entities
{
    public class Blog
    {
        public ulong Id { get; set; } // Primary Key
        [Required(ErrorMessage ="Required")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage ="Required")]
        public string Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property for the related comments
        public ICollection<Comment>? Comments { get; set; }
    }
}
