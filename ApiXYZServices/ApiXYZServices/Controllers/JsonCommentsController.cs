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
public class JsonPlaceCommentsholderController : ControllerBase
{
    // Simula una lista de publicaciones
    private List<JsonComments> Comments = new List<JsonComments>
    {
        new JsonComments { Id = 1, PostId = 1, Name = "Título de la publicación 1", Email = "Js.higuita1@gmail.com",Body = "Contenido de la publicación 1" },
        new JsonComments { Id = 2, PostId = 2, Name = "Título de la publicación 2", Email = "Js.higuita1@gmail.com",Body = "Contenido de la publicación 2" },
        new JsonComments { Id = 3, PostId = 3, Name = "Título de la publicación 3", Email = "Js.higuita1@gmail.com",Body = "Contenido de la publicación 3" }
    };

    public ResponseGeneric response;
    public readonly CommentsRepository _Repository;
    public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

    public JsonPlaceCommentsholderController(CommentsRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
    {
        _Repository = repository;
        _hubContext = hubContext;

    }

    // Devuelve la lista de publicaciones en formato JSON
    [HttpGet("Comments")]
    public async Task<IActionResult> GetComments()
    {

        try
        {
            response = await _Repository.GetAllComments();
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
