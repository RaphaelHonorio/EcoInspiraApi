using EcoInspira.Domain.Dtos;
using EcoInspira.Domain.Entities;
using EcoInspira.Domain.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace EcoInspira.Infrastructure.DataAccess.Repositories
{
    public class PostRepository : IPostWriteOnlyRepository, IPostReadOnlyRepository
    {
        private readonly EcoInspiraDbContext _context;

        public PostRepository(EcoInspiraDbContext context) => _context = context;

         public async Task Add(Post post) => await _context.Post.AddAsync(post);

        public async Task<IList<Post>> Filter (User user, FilterPostDto filters) 
        {
           var query = _context
                .Post
                .AsNoTracking()
                .Where(post => post.Active && post.Id == user.Id);

            /*if (filters.PostTitle.Any())
            {
                query = query.Where(
                    post => post.Title.Contains(filters.PostTitle));
            }*/

           return await query.ToListAsync();
        }

        public async Task<Post?> GetById(User user, long postId)
        {
            return await _context
                .Post
                .AsNoTracking()
                .FirstOrDefaultAsync(post => post.Active && post.Id == postId && post.UserId == user.Id);
        }
    }
}
