using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Exceptions;

namespace Domain.Core
{
    public class PostDomain : IPostDomain
    {
        private readonly IGenericRepositoriy<Customer> _customerRepositoryGeneric;
        private readonly IGenericRepositoriy<Post> _postRepository;
        private readonly ICustomerRepository _customerRepository;

        public PostDomain(
            IGenericRepositoriy<Customer> customerRepositoryGeneric,
            IGenericRepositoriy<Post> postRepository,
            ICustomerRepository customerRepository)
        {
            _customerRepositoryGeneric = customerRepositoryGeneric;
            _postRepository = postRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Obtiene todos los posts
        /// </summary>
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAllAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Crea un nuevo post validando que el Customer exista
        /// </summary>
        public async Task CreatePost(Post post)
        {
            await CustomerExists(post.CustomerId).ConfigureAwait(false);
            ProcessPost(post);
            await _postRepository.CreateAsync(post).ConfigureAwait(false);
        }

        /// <summary>
        /// Crea múltiples posts válidos, excluyendo los que tengan CustomerId inexistente
        /// </summary>
        public async Task CreatePosts(IEnumerable<Post> posts)
        {
            var customerIds = posts.Select(p => p.CustomerId).Distinct();

            var existingCustomerIds = new HashSet<int>(
                await _customerRepository.GetExistingIdsAsync(customerIds).ConfigureAwait(false)
            );

            var validPosts = posts
                .Where(p => existingCustomerIds.Contains(p.CustomerId))
                .Select(p =>
                {
                    ProcessPost(p);
                    return p;
                })
                .ToList();

            if (validPosts.Count > 0)
            {
                await _postRepository.CreateRangeAsync(validPosts).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Actualiza un post, validando existencia de post y del costumer
        /// </summary>
        public async Task UpdatePost(Post post)
        {
            await EnsurePostExists(post.PostId).ConfigureAwait(false);
            await CustomerExists(post.CustomerId).ConfigureAwait(false);
            ProcessPost(post);
            await _postRepository.UpdateAsync(post).ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina un post por su ID
        /// </summary>
        public async Task DeletePost(int postId)
        {
            await _postRepository.DeleteAsync(postId).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifica si el Customer existe por su ID
        /// </summary>
        private async Task CustomerExists(int customerId)
        {
            bool exists = await _customerRepositoryGeneric
                .AnyAsync(c => c.CustomerId == customerId)
                .ConfigureAwait(false);

            if (!exists)
                throw new BadRequestException("El cliente no existe");
        }

        /// <summary>
        /// Procesa y normaliza un post (Body truncado y categoría ajustada)
        /// </summary>
        private void ProcessPost(Post post)
        {
            post.Category = GetCategory(post.Type, post.Category);
            post.Body = ProcessBody(post.Body);
        }

        /// <summary>
        /// Trunca el Body si supera los 97 caracteres
        /// </summary>
        private string ProcessBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return body;

            return body.Length > 97 ? body.Substring(0, 97) + "..." : body;
        }

        private async Task EnsurePostExists(int postId)
        {
            bool exists = await _postRepository.AnyAsync(p => p.PostId == postId).ConfigureAwait(false);

            if (!exists)
                throw new NotFoundException("El post no existe.");
        }

        /// <summary>
        /// Devuelve una categoría según el tipo de post
        /// </summary>
        private string GetCategory(int type, string originalCategory)
        {
            switch (type)
            {
                case 1:
                    return "Farándula";
                case 2:
                    return "Política";
                case 3:
                    return "Fútbol";
                default:
                    return originalCategory;
            }
        }
    }
}
