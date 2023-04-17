using ApiXYZServices.Clasess;
using ApiXYZServices.DataObjects;
using ApiXYZServices.Interfaces;
using ApiXYZServices.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class JsonPlaceholderController : ControllerBase
{
    // Simula una lista de publicaciones
    private List<JsonPost> posts = new List<JsonPost>
    {
        new JsonPost { Id = 1, UserId = 1, title = "Título de la publicación 1", body = "Contenido de la publicación 1" },
        new JsonPost { Id = 2, UserId = 2, title = "Título de la publicación 2", body = "Contenido de la publicación 2" },
        new JsonPost { Id = 3, UserId = 3, title = "Título de la publicación 3", body = "Contenido de la publicación 3" }
    };

    public ResponseGeneric response;
    public readonly PostsRepository _Repository;
    public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

    public JsonPlaceholderController(PostsRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
    {
        _Repository = repository;
        _hubContext = hubContext;

    }

    // Devuelve la lista de publicaciones en formato JSON
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts()
    {

        try
        {
            response = await _Repository.GetAllPost();
            await _hubContext.Clients.All.BroadcastMessage();

            string json = JsonConvert.SerializeObject(response.Data);
            return Ok(json);
        }
        catch(Exception ex)
        {

        }

        return NoContent();
       
    }
}
