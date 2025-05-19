using Application.DTO.Post.Request;
using Application.DTO.Post.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostApplication
    {
        Task<IEnumerable<GetPostResponse>> GetAllPosts();
        Task CreatePost(CreatePostRequest createPost);
        Task CreatePosts(IEnumerable<CreatePostRequest> createposts);
        Task UpdatePost(UpdatePostRequest updatePost);
        Task DeletePost(int postId);
       
    }
}
