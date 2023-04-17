using ApiXYZServices.Clasess;
using ApiXYZServices.DataObjects;
using ApiXYZServices.Interfaces;
using ApiXYZServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace ApiXYZServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        public ResponseGeneric response;
        public readonly PostsRepository _Repository;
        public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public PostsController(PostsRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _Repository = repository;
            _hubContext = hubContext;

        }

        [HttpPost("InsertMassivePosts")]
        public async Task<IActionResult> InsertMassivePost()
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {
                        response = await _Repository.InsertMassivePosts();
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }

               
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("CreatePosts")]
        public async Task<IActionResult> CreatePost([FromBody] RequestCreatePost request)
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {
                        response = await _Repository.CreatePost(request);
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                    return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }

               
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("GetPost")]
        public async Task<IActionResult> GetPost()
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {
                        response = await _Repository.GetAllPost();
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("GetPostById")]
        public async Task<IActionResult> GetPostById(RequestGetPostId request)
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {

                        response = await _Repository.GetPostById(request);
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(RequestDeletePost request)
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {
                        response = await _Repository.DeletePost(request);
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }
              
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(RequestUpdatePost request)
        {
            response = new ResponseGeneric();

            try
            {

                string authHeader = Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Extrae el nombre de usuario y la contraseña
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int separatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, separatorIndex);
                    string password = usernamePassword.Substring(separatorIndex + 1);

                    if (username == "XYZUser" && password == "XYZPass")
                    {
                        response = await _Repository.UpdatePost(request);
                        await _hubContext.Clients.All.BroadcastMessage();

                        if (response.CodeError == 200)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return NotFound(response);
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // Si el encabezado de autenticación no está presente o no es válido, devuelve un error 401 Unauthorized
                    return Unauthorized();
                }
          
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
