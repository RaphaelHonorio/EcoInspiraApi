using System.ComponentModel.DataAnnotations.Schema;

namespace EcoInspira.Domain.Entities
{
    [Table("Post")]
    public class Post : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long LikesCount { get; set; } =0;
        public long CommentsCount { get; set; } =0;
        public long UserId { get; set; }

    //    public IList<Comment> Comments { get; set; } = [];
    }
}
