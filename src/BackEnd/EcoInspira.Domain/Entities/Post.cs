namespace EcoInspira.Domain.Entities
{
    public class Post : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long LikesCount { get; set; } 
        public long CommentsCount { get; set; }
        public long UserId { get; set; }

        public IList<Comment> Comments { get; set; } = [];
    }
}
