using backend.DTOs;
using backend.DTOs.PostResponse;
using backend.Exceptions;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PostService(PostRepository repository)
{
    public async Task<(ResultResponse<List<PostDetailsResponse>>, int)> GetAllPostsDetailsWithStatusAsync()
    {
        List<PostDetailsResponse> response;
        try
        {
            var posts = await repository.GetAllAsync();
            
            if (posts.Count == 0)
                throw new NotFoundException("Não há posts cadastados no momento");

            response = posts.CopyAllResponseFrom();
            return (new ResultResponse<List<PostDetailsResponse>>(response), 200);
        }
        catch (NotFoundException ex)
        {
            return (new ResultResponse<List<PostDetailsResponse>>(ex.Message), 404);
        }
        catch (DbUpdateException)
        {
            return (new ResultResponse<List<PostDetailsResponse>>("EX400 - Erro no banco de dados"), 400);
        }
        catch (Exception)
        {
            return (new ResultResponse<List<PostDetailsResponse>>("EX500 - Error interno no servidor"), 500);
        }
    }
    public async Task<(ResultResponse<PostDetailsResponse>, int)> GetPostDetailsWithStatusAsync(int id)
    {
        PostDetailsResponse response = new();
        try
        {
            var postById = await repository.GetAsync(id);
            
            response.CopyResponseFrom(postById);
            ResultResponse<PostDetailsResponse> request = new(response);
            
            return (request, 200);
        }
        catch (NotFoundException ex)
        {
            return (new ResultResponse<PostDetailsResponse>(ex.Message), 404);
        }
        catch (DbUpdateException)
        {
            return (new ResultResponse<PostDetailsResponse>("EX400 - Erro no banco de dados"), 400);
        }
        catch (Exception)
        {
            return (new ResultResponse<PostDetailsResponse>("EX500 - Error interno no servidor"), 500);
        }
    }

    public async Task<(ResultResponse<PostDetailsResponse>, int)> InsertNewPost(PostCreateResponse model)
    {
        PostDetailsResponse response = new();
        try
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                AuthorName = model.AuthorName,
                Slug = model.Title.GetSlug(),
                CreatedAt = DateTime.Now,
                ModifiedAt = null,
                Tags = await repository.AddTagsAsync(model.Tags)
            };
            var newPost = await repository.CreateAsync(post);
            response.CopyResponseFrom(newPost);

            return (new ResultResponse<PostDetailsResponse>(response), 201);
        }
        catch (DbUpdateException)
        {
            return (new ResultResponse<PostDetailsResponse>("EX400 - Erro no banco de dados"), 400);
        }
        catch (Exception)
        {
            return (new ResultResponse<PostDetailsResponse>("EX500 - Error interno no servidor"), 500);
        }
    }
}