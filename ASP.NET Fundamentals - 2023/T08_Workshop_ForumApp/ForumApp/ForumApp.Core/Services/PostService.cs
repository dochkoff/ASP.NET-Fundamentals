using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext _context)
        {
            context = _context;   
        }


        public async Task AddAsync(PostModel model)
        {
            var entity = new Post()
            {
                Title = model.Title,
                Content = model.Content,
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);

            context.Remove(entity);
            await context.SaveChangesAsync();

        }

        public async Task EditAsync(PostModel model)
        {
            var entity = await FindByIdAsync(model.Id);

            entity.Title = model.Title;
            entity.Content = model.Content;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllPostAsync()
        {
            return await context.Posts
                .Select(p=>new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PostModel?> GetByIdAsync(int id)
        {
            return await context.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        private async Task<Post> FindByIdAsync(int id)
        {
            var entity = await context.FindAsync<Post>(id);

            if (entity == null)
            {
                throw new ApplicationException("Invalid post");
            };

            return entity;
        }
    }
}
