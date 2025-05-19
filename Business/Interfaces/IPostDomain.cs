using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPostDomain
    {
        Task<IEnumerable<Post>> GetAllPosts();

        Task CreatePost(Post post);

        Task CreatePosts(IEnumerable<Post> posts);

        Task UpdatePost(Post post);

        Task DeletePost(int id);
        
    }
}
