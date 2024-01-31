using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data.Models;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task AddAsync(PostModel model);
        Task DeleteAsync(int id);
        Task EditAsync(PostModel model);
        Task<IEnumerable<PostModel>> GetAllPostAsync();
        Task<PostModel?> GetByIdAsync(int id);
    }
}
