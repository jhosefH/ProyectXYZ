using ApiXYZServices.DataObjects;
using ApiXYZServices.Utilities;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static ApiXYZServices.Utilities.Enumerables.ApiEnumerables;

namespace ApiXYZServices.Repository
{
    public class UserRepository
    {

        private readonly IConfiguration _config;
        DynamicParameters parameters = new DynamicParameters();
        ResponseGeneric response;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        //Metodo para crear Usuario

        public async Task<ResponseGeneric> CreateUser(RequestCreateUser request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services CreateUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if(request != null && !string.IsNullOrEmpty(request.user))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if(UserExist == null)
                        {
                            parameters = new DynamicParameters();

                            parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);
                            parameters.Add("@contrasena", request.password, DbType.String, ParameterDirection.Input);

                            var CreateUser = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                            response.CodeError = 200;
                            response.Message = "Usuario Creado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreateUser";

                             Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "Nombre de Usuario ya existente";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreateUser";

                            Log = SaveLog(data);

                            return response;

                        }

                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services CreateUser";

                        Log = SaveLog(data);

                        return response;

                    }
                }

            }
            catch(Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services CreateUser";

                var Log = SaveLog(data);

                return response;

            }

        }

        //Metodo para Consultar Usuario

        public ResponseGeneric GetUser(RequestGetUser request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services GetUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null && !string.IsNullOrEmpty(request.user))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (UserExist == null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetUser";

                            Log = SaveLog(data);

                        }
                        else
                        {
                            response.CodeError = 200;
                            response.Message = "Usuario Encontrado";
                            response.Data = JsonConvert.SerializeObject(UserExist);

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetUser";

                            Log = SaveLog(data);

                        }

                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services GetUser";

                        Log = SaveLog(data);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services GetUser";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric DeleteUser(RequestDeleteUser request)
        {
            try
            {
                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services DeleteUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null && !string.IsNullOrEmpty(request.user))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);

                        var ResponseData = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_DELETE_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (ResponseData == null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeleteUser";

                            Log = SaveLog(data);

                        }
                        else
                        {
                            response.CodeError = 200;
                            response.Message = "Usuario Eliminado Correctamente";
                            response.Data = JsonConvert.SerializeObject(ResponseData);

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeleteUser";

                            Log = SaveLog(data);
                        }

                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services DeleteUser";

                        Log = SaveLog(data);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services DeleteUser";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric UpdatePasswordUser(RequestUpdateUserPass request)
        {
            try
            {
                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services UpdatePasswordUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null && !string.IsNullOrEmpty(request.user))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (UserExist == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                        }
                        else
                        {

                            parameters = new DynamicParameters();
                            parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);
                            parameters.Add("@contrasena", request.password, DbType.String, ParameterDirection.Input);
                            parameters.Add("@newcontrasena", request.NewPassword, DbType.String, ParameterDirection.Input);

                            var UpdateUser = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_UPDATE_USER_PASSWORD}", parameters, commandType: CommandType.StoredProcedure).Result;

                            if(UpdateUser == null)
                            {
                                response.CodeError = 300;
                                response.Message = "Usuario y/o contrasena incorrectos";
                                response.Data = false;

                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services UpdatePasswordUser";

                                Log = SaveLog(data);
                            }
                            else
                            {
                                response.CodeError = 200;
                                response.Message = "Contrasena cambiada correctamente";
                                response.Data = UpdateUser;

                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services UpdatePasswordUser";

                                Log = SaveLog(data);


                            }
                        }

                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services UpdatePasswordUser";

                        Log = SaveLog(data);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services UpdatePasswordUser";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric UpdateUser(RequestUpdateUser request)
        {
            try
            {
                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services UpdateUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null && !string.IsNullOrEmpty(request.user))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (UserExist == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                        }
                        else
                        {

                            parameters = new DynamicParameters();
                            parameters.Add("@usuario", request.NewUser, DbType.String, ParameterDirection.Input);

                            var ValidationUser = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                            if(ValidationUser == null)
                            {
                                parameters = new DynamicParameters();
                                parameters.Add("@usuario", request.user, DbType.String, ParameterDirection.Input);
                                parameters.Add("@contrasena", request.password, DbType.String, ParameterDirection.Input);
                                parameters.Add("@newusuario", request.NewUser, DbType.String, ParameterDirection.Input);

                                var UpdateUser = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_UPDATE_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                                if (UpdateUser != null)
                                {
                                    response.CodeError = 300;
                                    response.Message = "Usuario y/o contrasena incorrectos";
                                    response.Data = false;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services UpdateUser";

                                    Log = SaveLog(data);
                                }
                                else
                                {
                                    response.CodeError = 200;
                                    response.Message = "Nombre de Usuario cambiado correctamente";
                                    response.Data = UpdateUser;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services UpdateUser";

                                    Log = SaveLog(data);
                                }
                            }
                            else
                            {
                                response.CodeError = 300;
                                response.Message = "Nombre de Usuario ya existe";
                                response.Data = false;

                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services UpdateUser";

                                Log = SaveLog(data);
                            }

                          
                        }

                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services UpdateUser";

                        Log = SaveLog(data);
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services UpdateUser";

                var Log = SaveLog(data);
            }

            return response;
        }


        #region Logs

        public bool SaveLog(StructureLogs request)
        {
            try
            {
              //  response = new ResponseGeneric();

                var Log = JsonConvert.SerializeObject(request);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@Log", request.Data, DbType.String, ParameterDirection.Input);
                        parameters.Add("@Date", DateTime.Now, DbType.Date, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_INSERT_LOG}", parameters, commandType: CommandType.StoredProcedure).Result;

                        return true;
                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;
            }

            return false;
        }
        #endregion


        public string GenerateBearer(string cedula)
        {

            try
            {
                string JWT = Util.GenerateJWT(cedula);

                DateTime date = new DateTime();

                date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                using (IDbConnection conn = Connection)
                {

                    if (!string.IsNullOrEmpty(cedula))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@Bearer", JWT, DbType.String, ParameterDirection.Input);
                        parameters.Add("@DateBegin",date, DbType.Date, ParameterDirection.Input);
                        parameters.Add("@DateEnd", date.AddHours(1), DbType.Date, ParameterDirection.Input);

                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_INSERT_BEARER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        return JWT;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;
            }

            return null;

        }

        public bool ValidateBearer(RequestValidateJWT request)
        {

            try
            {
                using (IDbConnection conn = Connection)
                {

                    if (!string.IsNullOrEmpty(request.Bearer))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@Bearer", request.Bearer, DbType.String, ParameterDirection.Input);


                        var Validate = conn.QueryFirstOrDefaultAsync<ResponseValidateBearer>($"{EStoredProcedures.SP_VALIDATE_BEARER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if(Validate != null)
                        {
                            if (Validate.DateEnd < DateTime.Now)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;
            }

            return false;

        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }
    }
}
