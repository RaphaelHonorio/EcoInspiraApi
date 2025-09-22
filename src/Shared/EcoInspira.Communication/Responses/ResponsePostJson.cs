namespace EcoInspira.Communication.Responses
{
    public class ResponsePostJson
    {
        // --== Só esse Dois é suficiente
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

    public class ResponseListPostsJson
    {
        public IList<ResponsePostJson> Post { get; set; } = [];
    }
}

