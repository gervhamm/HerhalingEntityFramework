namespace HerhalingEntityFramework.Entities
{
    public class Comment
    {
        public ulong Id { get; set; } // Primary Key
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ulong BlogId { get; set; } // Foreign Key

        // Navigation property for the related blog
        public Blog? Blog { get; set; }
    }
}
