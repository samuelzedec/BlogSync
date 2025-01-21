using backend.DTOs;
using backend.DTOs.PostResponse;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;

[ApiController]
[Route("/v1")]
public class PostController : ControllerBase
{
    private readonly PostService _service;

    public PostController(PostService service)
        => _service = service;

    [HttpGet("posts")]
    public async Task<IActionResult> GetAllAsync()
    {
        var (response, statusCode) = await _service.GetAllPostsDetailsWithStatusAsync();
        return statusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response),
            404 => NotFound(response),
            _ => StatusCode(statusCode, response)
        };
    }
    
    [HttpGet("posts/{id:int}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultResponse<Post>("ID inválido para a requisição"));

        var (response, statusCode) = await _service.GetPostDetailsWithStatusAsync(id);
        return statusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response),
            404 => NotFound(response),
            _ => StatusCode(statusCode, response)
        };
    }
    
    [HttpPost("posts")]
    public async Task<IActionResult> CreatePostAsync(
        [FromBody] PostCreateResponse model)
    {
        var (response, statusCode) = await _service.InsertNewPost(model);
        
        
        return statusCode switch
        {
            201 => Created($"posts/{response.Data?.Id}", response),
            400 => BadRequest(response),
            _ => StatusCode(statusCode, response)
        };
    }
}