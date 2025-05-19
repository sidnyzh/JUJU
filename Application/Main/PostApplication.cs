using Application.DTO.Post.Request;
using Application.DTO.Post.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Main
{
    public class PostApplication : IPostApplication
    {
        private readonly IPostDomain _postDomain;
        private readonly IMapper _mapper;

        public PostApplication(IPostDomain postDomain, IMapper mapper)
        {
            _postDomain = postDomain;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los posts desde la capa de dominio
        /// y los transforma a DTOs de respuesta.
        /// </summary>
        public async Task<IEnumerable<GetPostResponse>> GetAllPosts()
        {
            var posts = await _postDomain.GetAllPosts().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<GetPostResponse>>(posts);
        }

        /// <summary>
        /// Crea un nuevo post a partir de un DTO recibido.
        /// </summary>
        public async Task CreatePost(CreatePostRequest createPost)
        {
            var post = _mapper.Map<Post>(createPost);
            await _postDomain.CreatePost(post).ConfigureAwait(false);
        }

        /// <summary>
        /// Crea varios posts a partir de una colección de DTOs y devuelve el resumen de los no ceados .
        /// </summary>
        public async Task CreatePosts(IEnumerable<CreatePostRequest> createPosts)
        {
            var posts = _mapper.Map<IEnumerable<Post>>(createPosts);
            await _postDomain.CreatePosts(posts).ConfigureAwait(false);
        }

        /// <summary>
        /// Actualiza un post existente con los datos recibidos.
        /// </summary>
        public async Task UpdatePost(UpdatePostRequest updatePost)
        {
            var post = _mapper.Map<Post>(updatePost);
            await _postDomain.UpdatePost(post).ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina un post por su identificador.
        /// </summary>
        public async Task DeletePost(int postId)
        {
            await _postDomain.DeletePost(postId).ConfigureAwait(false);
        }
    }
}
