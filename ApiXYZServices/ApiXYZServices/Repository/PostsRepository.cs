using ApiXYZServices.DataObjects;
using Dapper;
using Newtonsoft.Json;
using static ApiXYZServices.Utilities.Enumerables.ApiEnumerables;
using System.Data;
using System.Data.SqlClient;
using RestSharp;
using System.Collections.Generic;

namespace ApiXYZServices.Repository
{
    public class PostsRepository
    {

        private readonly IConfiguration _config;
        DynamicParameters parameters = new DynamicParameters();
        ResponseGeneric response;

        public PostsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ResponseGeneric> InsertMassivePosts()
        {
            try
            {
                response = new ResponseGeneric();



                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/posts");
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var responseD = await client.SendAsync(request);
                responseD.EnsureSuccessStatusCode();
                Console.WriteLine(await responseD.Content.ReadAsStringAsync());

                var x = await responseD.Content.ReadAsStringAsync();

                List<ResponseJsonPosts> Posts = JsonConvert.DeserializeObject<List<ResponseJsonPosts>>(x);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {

                        foreach (var info in Posts)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@UserId ", info.userId, DbType.Int32, ParameterDirection.Input);
                            parameters.Add("@title ", info.title, DbType.String, ParameterDirection.Input);
                            parameters.Add("@body ", info.body, DbType.String, ParameterDirection.Input);

                            var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_POSTS}", parameters, commandType: CommandType.StoredProcedure).Result;
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

        public async Task<ResponseGeneric> CreatePost(RequestCreatePost request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services CreatePost";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@UserId ", request.UserId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@title ", request.title, DbType.String, ParameterDirection.Input);
                        parameters.Add("@body ", request.body, DbType.String, ParameterDirection.Input);

                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_POSTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Posts Creado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreatePost";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo registrar el posts de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreatePost";

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
                        data.Service = "Response Services CreatePost";

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
                data.Service = "Error Catch Services CreatePost";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> DeletePost(RequestDeletePost request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services DeletePost";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@id ", request.Id, DbType.Int32, ParameterDirection.Input);


                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_DELETE_POSTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Posts Eliminado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeletePost";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Eliminar el posts de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeletePost";

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
                        data.Service = "Response Services DeletePost";

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
                data.Service = "Error Catch Services DeletePost";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> UpdatePost(RequestUpdatePost request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services UpdatePost";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@id ", request.Id, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@UserId ", request.UserId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@title ", request.title, DbType.String, ParameterDirection.Input);
                        parameters.Add("@body ", request.body, DbType.String, ParameterDirection.Input);


                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_UPDATE_POSTS}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Posts Actualizado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services UpdatePost";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Actualizar el posts de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services UpdatePost";

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
                        data.Service = "Response Services UpdatePost";

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
                data.Service = "Error Catch Services UpdatePost";

                var Log = SaveLog(data);

                return response;

            }

        }

        public async Task<ResponseGeneric> GetPostById(RequestGetPostId request)
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services GetPostById";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@ID ", request.ID, DbType.Int32, ParameterDirection.Input);



                        var Insert = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_GET_POSTS_BY_ID}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Posts Actualizado correctamente";
                            response.Data = true;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetPostById";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo Actualizar el posts de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetPostById";

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
                        data.Service = "Response Services GetPostById";

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

        public async Task<ResponseGeneric> GetAllPost()
        {
            try
            {
                response = new ResponseGeneric();


                StructureLogs data = new StructureLogs();

                data.Data = "No recibe Request para la peticion solicitada";
                data.Service = "Request Services GetAllPost";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {
                  

                        var Insert = conn.QueryAsync($"{EStoredProcedures.SP_GET_POSTS_ALL}", commandType: CommandType.StoredProcedure).Result;

                        if (Insert != null)
                        {
                            parameters = new DynamicParameters();

                            response.CodeError = 200;
                            response.Message = "Posts Obtenidos Correctamentes";
                            response.Data = Insert;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetAllPost";

                            Log = SaveLog(data);

                            return response;


                        }
                        else
                        {

                            response.CodeError = 300;
                            response.Message = "No se pudo obtener los posts de manera correcta";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetAllPost";

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
                data.Service = "Error Catch Services GetAllPost";

                var Log = SaveLog(data);

                return response;

            }

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


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }
    }
}
