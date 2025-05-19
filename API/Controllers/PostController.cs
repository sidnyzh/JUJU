using Application.DTO.Post.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostApplication _postApplication;

        /// <summary>
        /// Constructor del controlador de Post
        /// </summary>
        /// <param name="postApplication">Servicio de aplicación para lógica de Post</param>
        public PostController(IPostApplication postApplication)
        {
            _postApplication = postApplication;
        }

        /// <summary>
        /// Obtiene todos los posts existentes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postApplication.GetAllPosts();
            return Ok(posts);
        }

        /// <summary>
        /// Crea un nuevo post
        /// </summary>
        /// <param name="createPost">DTO con los datos del post a crear</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest createPost)
        {

            await _postApplication.CreatePost(createPost);
            return Ok();
        }

        /// <summary>
        /// Crea múltiples posts
        /// </summary>
        /// <param name="createPosts">Lista de DTOs con los datos de los posts a crear</param>
        [HttpPost("range")]
        public async Task<IActionResult> CreateRange([FromBody] IEnumerable<CreatePostRequest> createPosts)
        {
            await _postApplication.CreatePosts(createPosts);
            return Ok();
        }

        /// <summary>
        /// Actualiza un post existente
        /// </summary>
        /// <param name="updatePost">DTO con los datos del post a actualizar</param>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequest updatePost)
        {
            await _postApplication.UpdatePost(updatePost);
            return Ok();
        }

        /// <summary>
        /// Elimina un post por su ID
        /// </summary>
        /// <param name="postId">ID del post a eliminar</param>
        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            await _postApplication.DeletePost(postId);
            return Ok();
        }
    }
}
