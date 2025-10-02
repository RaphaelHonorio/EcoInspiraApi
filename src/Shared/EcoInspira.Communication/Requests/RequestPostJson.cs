namespace EcoInspira.Communication.Requests
{
    public class RequestPostJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public long LikesCount { get; set; } = 0;
        public long CommentsCount { get; set; } = 0;
    }
}
