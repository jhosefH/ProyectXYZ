using ApiXYZServices.Clasess;
using ApiXYZServices.DataObjects;
using ApiXYZServices.Interfaces;
using ApiXYZServices.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ApiXYZServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        public ResponseGeneric response;
        public readonly UserRepository _Repository;
        public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public UserController(UserRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _Repository = repository;
            _hubContext = hubContext;

        }


        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] RequestCreateUser request)
        {
            response = new ResponseGeneric();

            try
            {

                RequestValidateJWT data = new RequestValidateJWT
                {
                    Bearer = request.Bearer
                };

                if (_Repository.ValidateBearer(data))
                {
                    response = await _Repository.CreateUser(request);
                    await _hubContext.Clients.All.BroadcastMessage();

                    if(response.CodeError == 200)
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
                    response.CodeError = 300;
                    response.Message = "Bearer invalido";
                    return Ok(response);
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

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser([FromBody] RequestGetUser request)
        {
            response = new ResponseGeneric();

            try
            {

                RequestValidateJWT data = new RequestValidateJWT
                {
                    Bearer = request.Bearer
                };

                if (_Repository.ValidateBearer(data))
                {
                    response = _Repository.GetUser(request);
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
                    response.CodeError = 300;
                    response.Message = "Bearer invalido";
                    return Ok(response);
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

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] RequestDeleteUser request)
        {
            response = new ResponseGeneric();

            try
            {

                RequestValidateJWT data = new RequestValidateJWT
                {
                    Bearer = request.Bearer
                };

                if (_Repository.ValidateBearer(data))
                {
                    response = _Repository.DeleteUser(request);
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
                    response.CodeError = 300;
                    response.Message = "Bearer invalido";
                    return Ok(response);
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] RequestUpdateUser request)
        {
            response = new ResponseGeneric();

            try
            {

                RequestValidateJWT data = new RequestValidateJWT
                {
                    Bearer = request.Bearer
                };

                if (_Repository.ValidateBearer(data))
                {
                    response = _Repository.UpdateUser(request);
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
                    response.CodeError = 300;
                    response.Message = "Bearer invalido";
                    return Ok(response);
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

        [HttpPut("UpdateUserPass")]
        public async Task<IActionResult> UpdateUserPass([FromBody] RequestUpdateUserPass request)
        {
            response = new ResponseGeneric();

            try
            {

                RequestValidateJWT data = new RequestValidateJWT
                {
                    Bearer = request.Bearer
                };

                if (_Repository.ValidateBearer(data))
                {
                    response = _Repository.UpdatePasswordUser(request);
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
                    response.CodeError = 300;
                    response.Message = "Bearer invalido";
                    return Ok(response);
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

        [HttpPost("GenerateBearer")]
        public async Task<IActionResult> GenerateToken([FromBody] RequestGenerateBearer request)
        {
            response = new ResponseGeneric();

            try
            {
                   var responseBearer = _Repository.GenerateBearer(request.user);
                    await _hubContext.Clients.All.BroadcastMessage();

                    if (responseBearer != null)
                    {
                        return Ok(responseBearer);
                    }
                    else
                    {
                        return NotFound(response);
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
