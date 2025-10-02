using EcoInspira.Domain.Dtos;

namespace EcoInspira.Domain.Repositories.Post
{
    public interface IPostReadOnlyRepository
    {
        Task<IList<Entities.Post>> Filter(Entities.User user, FilterPostDto filters);
        
        Task<Entities.Post?> GetById(Entities.User user, long postId);

    }
}
