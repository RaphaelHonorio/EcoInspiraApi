using EcoInspira.Communication.Requests;

namespace EcoInspira.Communication.Responses
{
    public class ResponseRegisteredPostJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long LikesCount;
        public long CommentsCount;
        public IList<RequestCommentJson> Comments { get; set; } = [];
    }
}
