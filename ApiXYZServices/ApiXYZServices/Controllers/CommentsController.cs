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
    public class CommentsController : ControllerBase
    {

        public ResponseGeneric response;
        public readonly CommentsRepository _Repository;
        public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public CommentsController(CommentsRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _Repository = repository;
            _hubContext = hubContext;

        }

        [HttpPost("InsertMassiveComments")]
        public async Task<IActionResult> InsertMassiveComments()
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
                        response = await _Repository.InsertMassiveComments();
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

        [HttpPost("CreateComments")]
        public async Task<IActionResult> CreateComments([FromBody] RequestCreateComments request)
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

                    if(username == "XYZUser" && password == "XYZPass")
                    {

                        response = await _Repository.CreateComments(request);
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
        }

        [HttpPost("GetComments")]
        public async Task<IActionResult> GetComments()
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
                        response = await _Repository.GetAllComments();
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

        [HttpPost("GetCommentsById")]
        public async Task<IActionResult> GetCommentsById(RequestGetCommentsId request)
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
                        response = await _Repository.GetCommentsById(request);
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


        [HttpDelete("DeleteComments")]
        public async Task<IActionResult> DeleteComments(RequestDeleteComments request)
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
                        response = await _Repository.DeleteComments(request);
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


        [HttpPut("UpdateComments")]
        public async Task<IActionResult> UpdateComments(RequestUpdateComments request)
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
                        response = await _Repository.UpdateComments(request);
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
