namespace EcoInspira.Domain.Entities
{
    public class Comment
    {
        public string Content { get; set; } = string.Empty;
        public long PostId { get; set; }
        public long UserId { get; set; }
//        public Post Post { get; set; }
//        public User User { get; set; }
    }
}
