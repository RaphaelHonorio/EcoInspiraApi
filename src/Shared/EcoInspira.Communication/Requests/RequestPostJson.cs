namespace EcoInspira.Communication.Requests
{
    public class RequestPostJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public long LikesCount;
        public long CommentsCount;
        public IList<RequestCommentJson> Comments { get; set; } = [];
    }
}
