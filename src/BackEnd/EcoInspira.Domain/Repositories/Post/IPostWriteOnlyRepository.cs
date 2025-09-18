namespace EcoInspira.Domain.Repositories.Post
{
    public interface IPostWriteOnlyRepository
    {
        public Task Add(Entities.Post post);
    }
}
