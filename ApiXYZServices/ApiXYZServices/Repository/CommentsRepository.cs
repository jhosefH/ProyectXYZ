using ApiXYZServices.DataObjects;
using Dapper;
using Newtonsoft.Json;
using static ApiXYZServices.Utilities.Enumerables.ApiEnumerables;
using System.Data.SqlClient;
using System.Data;

namespace ApiXYZServices.Repository
{
    public class CommentsRepository
    {

        private readonly IConfiguration _config;
        DynamicParameters parameters = new DynamicParameters();
        ResponseGeneric response;

        public CommentsRepository(IConfiguration config)
        {
            _config = config;
        }

        #region Repository Services

        public async Task<ResponseGeneric> InsertMassiveComments()
        {
            try
            {
                response = new ResponseGeneric();

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/comments");
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var responseD = await client.SendAsync(request);
                responseD.EnsureSuccessStatusCode();
                Console.WriteLine(await responseD.Content.ReadAsStringAsync());

                var x = await responseD.Content.ReadAsStringAsync();

                List<ResponseJsonComments> Posts = JsonConvert.DeserializeObject<List<ResponseJsonComments>>(x);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {

                        foreach (var info in Posts)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@PostId ", info.postId, DbType.Int32, ParameterDirection.Input);
                            parameters.Add("@name", info.name, DbType.String, ParameterDirection.Input);
                            parameters.Add("@email  ", info.email, DbType.String, ParameterDirection.Input);
                            parameters.Add("@body  ", info.body, DbType.String, ParameterDirection.Input);

                            var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_COMMENTS}", parameters, commandType: CommandType.StoredProcedure).Result;
                        }


                        parameters = new DynamicParameters();

                        response.CodeError = 200;
                        response.Message = "Posts Creado correctamente";
                        response.Data = true;

                        return response;


                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "Informacion incompleta";
                        response.Data = false;

                        return response;

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return response;

        }

        public async Task<ResponseGeneric> CreateComments(RequestCreateComments request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services CreateComments";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@PostId ", request.PostId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@name", request.Name, DbType.String, ParameterDirection.Input);
                        parameters.Add("@email  ", request.Email, DbType.String, ParameterDirection.Input);
                        parameters.Add("@body  ", request.Body, DbType.String, ParameterDirection.Input);


                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_COMMENTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert == null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Comments Creado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreateComments";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo registrar el Comments de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreateComments";

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
                        data.Service = "Response Services CreateComments";

                        Log = SaveLog(data);

                        return response;

                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services CreateComments";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> DeleteComments(RequestDeleteComments request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services DeleteComments";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@id ", request.Id, DbType.Int32, ParameterDirection.Input);


                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_DELETE_COMMENTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Comments Eliminado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeleteComments";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Eliminar el Comments de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeleteComments";

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
                        data.Service = "Response Services DeleteComments";

                        Log = SaveLog(data);

                        return response;

                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services DeleteComments";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> UpdateComments(RequestUpdateComments request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services UpdateComments";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@id ", request.Id, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@PostId ", request.PostId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@name", request.Name, DbType.String, ParameterDirection.Input);
                        parameters.Add("@email  ", request.Email, DbType.String, ParameterDirection.Input);
                        parameters.Add("@body  ", request.Body, DbType.String, ParameterDirection.Input);


                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_UPDATE_COMMENTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Comments Actualizado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services UpdateComments";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Actualizar el Comments de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services UpdateComments";

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
                        data.Service = "Response Services UpdateComments";

                        Log = SaveLog(data);

                        return response;

                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services UpdateComments";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> GetCommentsById(RequestGetCommentsId request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services GetCommentsById";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@ID ", request.ID, DbType.Int32, ParameterDirection.Input);



                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_GET_COMMENTS_BY_ID}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Comments Obtenido correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetCommentsById";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Obteners el Comments de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetCommentsById";

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
                        data.Service = "Response Services GetCommentsById";

                        Log = SaveLog(data);

                        return response;

                    }
                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services GetPostById";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> GetAllComments()
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = "No recibe Request para la peticion solicitada";
                data.Service = "Request Services GetAllComments";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {


                    var Insert = conn.QueryAsync($"{EStoredProcedures.SP_GET_COMMENTS_ALL}", commandType: CommandType.StoredProcedure).Result;

                    if (Insert != null)
                    {
                        parameters = new DynamicParameters();

                        response.CodeError = 200;
                        response.Message = "Comments Obtenidos Correctamentes";
                        response.Data = Insert;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services GetAllComments";

                        Log = SaveLog(data);

                        return response;


                    }
                    else
                    {

                        response.CodeError = 300;
                        response.Message = "No se pudo obtener los posts de manera correcta";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services GetAllComments";

                        Log = SaveLog(data);

                        return response;

                    }


                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services GetAllComments";

                var Log = SaveLog(data);

                return response;

            }

        }
        #endregion

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


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }

    }
}
