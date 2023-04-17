using ApiXYZServices.Clasess;
using ApiXYZServices.DataObjects;
using ApiXYZServices.Interfaces;
using ApiXYZServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ApiXYZServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {

        public ResponseGeneric response;
        public readonly FamilyRepository _Repository;
        public readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public FamilyController(FamilyRepository repository, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _Repository = repository;
            _hubContext = hubContext;

        }

        [HttpPost("CreateFamily")]
        public async Task<IActionResult> CreateFamily([FromBody] RequestCreateFamily request)
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
                    response =  _Repository.CreateFamily(request);
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

        [HttpDelete("DeleteFamily")]
        public async Task<IActionResult> DeleteFamily([FromBody] RequestDeleteFamily request)
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
                    response = _Repository.DeleteFamily(request);
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

        [HttpPut("UpdateFamily")]
        public async Task<IActionResult> UpdateFamily([FromBody] RequestUpdateFamily request)
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
                    response = _Repository.UpdateFamily(request);
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

        [HttpPost("GetFamilyUser")]
        public async Task<IActionResult> GetFamilyUser([FromBody] RequestGetFamilyByUser request)
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
                    response = _Repository.GetFamilyByUser(request);
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

        [HttpPost("GetFamilyByDocument")]
        public async Task<IActionResult> GetFamilyByDocument([FromBody] RequestGetFamilyByDocument request)
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
                    response = _Repository.GetFamilyByDocument(request);
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

    }
}
