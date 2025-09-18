using EcoInspira.Domain.Entities;
using EcoInspira.Domain.Repositories.Post;

namespace EcoInspira.Infrastructure.DataAccess.Repositories
{
    public class PostRepository : IPostWriteOnlyRepository
    {
        private readonly EcoInspiraDbContext _context;

        public PostRepository(EcoInspiraDbContext context) => _context = context;

         public async Task Add(Post post) => await _context.Post.AddAsync(post);
    }
}
