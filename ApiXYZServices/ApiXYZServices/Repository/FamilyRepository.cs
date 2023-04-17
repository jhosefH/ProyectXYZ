using ApiXYZServices.DataObjects;
using Dapper;
using static ApiXYZServices.Utilities.Enumerables.ApiEnumerables;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace ApiXYZServices.Repository
{
    public class FamilyRepository
    {

        private readonly IConfiguration _config;
        DynamicParameters parameters = new DynamicParameters();
        ResponseGeneric response;

        public FamilyRepository(IConfiguration config)
        {
            _config = config;
        }

        public ResponseGeneric CreateFamily(RequestCreateFamily request)
        {

            try
            {
                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services CreateFamily";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {
                    if (request != null && !string.IsNullOrEmpty(request.Usuario))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.Usuario, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (UserExist == null)
                        {
                           
                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services CreateFamily";

                            Log = SaveLog(data);

                        }
                        else
                        {

                            parameters = new DynamicParameters();

                            parameters.Add("@Usuario", request.Usuario, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Cedula", request.Cedula, DbType.Int32, ParameterDirection.Input);

                            var ValidateFamily = conn.QueryFirstOrDefaultAsync<ResponseGetFamily>($"{EStoredProcedures.SP_GET_FAMILY_BY_DOCUMENT}", parameters, commandType: CommandType.StoredProcedure).Result;

                            if (ValidateFamily == null)
                            {
                                parameters = new DynamicParameters();
                                parameters.Add("@Usuario", request.Usuario, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Cedula", request.Cedula, DbType.Int32, ParameterDirection.Input);
                                parameters.Add("@nombre", request.Nombre, DbType.String, ParameterDirection.Input);
                                parameters.Add("@apellidos", request.Apellidos, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Genero", request.genero, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Parentesco", request.Parentesco, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Edad", request.Edad, DbType.Int32, ParameterDirection.Input);

                                if (request.Edad < 18)
                                {
                                    parameters.Add("@MenorEdad", 1, DbType.Byte, ParameterDirection.Input);
                                    parameters.Add("@FechaNacimiento", Convert.ToDateTime(request.FechaNacimiento), DbType.Date, ParameterDirection.Input);
                                }
                                else
                                {
                                    parameters.Add("@MenorEdad", 0, DbType.Byte, ParameterDirection.Input);
                                }

                                var CreateFamily = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_CREATE_FAMILY}", parameters, commandType: CommandType.StoredProcedure).Result;

                                if (CreateFamily == null)
                                {
                                    response.CodeError = 300;
                                    response.Message = "No se pudo registrar el familiar verifique la informacion ingresada";
                                    response.Data = false;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services CreateFamily";

                                    Log = SaveLog(data);
                                }
                                else
                                {
                                    response.CodeError = 200;
                                    response.Message = "Familiar registrado correctamente";
                                    response.Data = CreateFamily;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services CreateFamily";

                                    Log = SaveLog(data);
                                }
                            }
                            else
                            {
                                response.CodeError = 300;
                                response.Message = "Ya se encuentra un familiar registrado con este numero de Documento";
                                response.Data = false;

                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services CreateFamily";

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
                        data.Service = "Response Services CreateFamily";

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
                data.Service = "Error Catch Services CreateFamily";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric UpdateFamily(RequestUpdateFamily request)
        {

            try
            {
                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services UpdateFamily";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {
                    if (request != null && !string.IsNullOrEmpty(request.Usuario))
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@usuario", request.Usuario, DbType.String, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (UserExist == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Usuario No Existe";
                            response.Data = false;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services UpdateFamily";

                            Log = SaveLog(data);

                        }
                        else
                        {

                            parameters = new DynamicParameters();

                            parameters.Add("@Usuario", request.Usuario, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Cedula", request.Cedula, DbType.Int32, ParameterDirection.Input);

                            var ValidateFamily = conn.QueryFirstOrDefaultAsync<ResponseGetFamily>($"{EStoredProcedures.SP_GET_FAMILY_BY_DOCUMENT}", parameters, commandType: CommandType.StoredProcedure).Result;

                            if (ValidateFamily != null)
                            {
                                parameters = new DynamicParameters();
                                parameters.Add("@Usuario", request.Usuario, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Cedula", request.Cedula, DbType.Int32, ParameterDirection.Input);
                                parameters.Add("@nombre", request.Nombre, DbType.String, ParameterDirection.Input);
                                parameters.Add("@apellidos", request.Apellidos, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Genero", request.genero, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Parentesco", request.Parentesco, DbType.String, ParameterDirection.Input);
                                parameters.Add("@Edad", request.Edad, DbType.Int32, ParameterDirection.Input);

                                if (request.Edad < 18)
                                {
                                    parameters.Add("@MenorEdad", 1, DbType.Byte, ParameterDirection.Input);
                                    parameters.Add("@FechaNacimiento", Convert.ToDateTime(request.FechaNacimiento), DbType.Date, ParameterDirection.Input);
                                }
                                else
                                {
                                    parameters.Add("@MenorEdad", 0, DbType.Byte, ParameterDirection.Input);
                                }

                                var CreateFamily = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_UPDATE_FAMILY}", parameters, commandType: CommandType.StoredProcedure).Result;

                                if (CreateFamily == null)
                                {
                                    response.CodeError = 300;
                                    response.Message = "No se pudo actualizar la informacion del familiar verifique la informacion ingresada";
                                    response.Data = false;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services UpdateFamily";

                                    Log = SaveLog(data);
                                }
                                else
                                {
                                    response.CodeError = 200;
                                    response.Message = "Familiar actualizado correctamente";
                                    response.Data = CreateFamily;

                                    data.Data = JsonConvert.SerializeObject(response);
                                    data.Service = "Response Services UpdateFamily";

                                    Log = SaveLog(data);
                                }
                            }
                            else
                            {
                                response.CodeError = 300;
                                response.Message = "No se encontro informacion del familiar consultado,verifique la informacion";
                                response.Data = false;

                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services UpdateFamily";

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
                        data.Service = "Response Services UpdateFamily";

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
                data.Service = "Error Catch Services UpdateFamily";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric DeleteFamily(RequestDeleteFamily request)
        {
            try
            {

                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services DeleteFamily";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    parameters = new DynamicParameters();
                    parameters.Add("@usuario", request.User, DbType.String, ParameterDirection.Input);

                    var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                    if (UserExist == null)
                    {
                        response.CodeError = 300;
                        response.Message = "Usuario No Existe";
                        response.Data = false;

                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services DeleteFamily";

                        Log = SaveLog(data);

                    }
                    else
                    {

                        parameters = new DynamicParameters();

                        parameters.Add("@Usuario", request.User, DbType.String, ParameterDirection.Input);
                        parameters.Add("@Cedula", request.Document, DbType.Int32, ParameterDirection.Input);

                        var ValidateFamily = conn.QueryFirstOrDefaultAsync<ResponseGetFamily>($"{EStoredProcedures.SP_GET_FAMILY_BY_DOCUMENT}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (ValidateFamily == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Familiar no encontrado,verifique la informacion";
                            response.Data = false;


                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services DeleteFamily";

                            Log = SaveLog(data);
                        }
                        else
                        {
                            parameters = new DynamicParameters();

                            parameters.Add("@Usuario", request.User, DbType.String, ParameterDirection.Input);
                            parameters.Add("@contrasena", request.Password, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Cedula", request.Document, DbType.Int32, ParameterDirection.Input);

                            var DeleteFamily = conn.QueryFirstOrDefaultAsync($"{EStoredProcedures.SP_DELETE_FAMILY}", parameters, commandType: CommandType.StoredProcedure).Result;

                            if (DeleteFamily != null)
                            {
                                response.CodeError = 300;
                                response.Message = "No se pudo eliminar el familiar,por favor intentelo nuevamente";
                                response.Data = false;


                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services DeleteFamily";

                                Log = SaveLog(data);
                            }
                            else
                            {
                                response.CodeError = 200;
                                response.Message = "Familiar eliminado correctamente";
                                response.Data = true;


                                data.Data = JsonConvert.SerializeObject(response);
                                data.Service = "Response Services DeleteFamily";

                                Log = SaveLog(data);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services DeleteFamily";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric GetFamilyByUser(RequestGetFamilyByUser request)
        {
            try
            {

                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services GetFamilyByUser";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    parameters = new DynamicParameters();
                    parameters.Add("@usuario", request.User, DbType.String, ParameterDirection.Input);

                    var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                    if (UserExist == null)
                    {
                        response.CodeError = 300;
                        response.Message = "Usuario No Existe";
                        response.Data = false;


                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services GetFamilyByUser";

                        Log = SaveLog(data);

                    }
                    else
                    {

                        parameters = new DynamicParameters();

                        parameters.Add("@Usuario", request.User, DbType.String, ParameterDirection.Input);

                        var ValidateFamily = conn.QueryFirstOrDefaultAsync<ResponseGetFamily>($"{EStoredProcedures.SP_GET_FAMILY_BY_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (ValidateFamily == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Familiar no encontrado,verifique la informacion";
                            response.Data = false;


                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetFamilyByUser";

                            Log = SaveLog(data);
                        }
                        else
                        {
                            response.CodeError = 200;
                            response.Message = "Consulta exitosa";
                            response.Data = ValidateFamily;

                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetFamilyByUser";

                            Log = SaveLog(data);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services GetFamilyByUser";

                var Log = SaveLog(data);
            }

            return response;
        }

        public ResponseGeneric GetFamilyByDocument(RequestGetFamilyByDocument request)
        {
            try
            {

                response = new ResponseGeneric();

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(request);
                data.Service = "Request Services GetFamilyByDocument";

                var Log = SaveLog(data);

                using (IDbConnection conn = Connection)
                {

                    parameters = new DynamicParameters();
                    parameters.Add("@usuario", request.User, DbType.String, ParameterDirection.Input);

                    var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_GET_USER}", parameters, commandType: CommandType.StoredProcedure).Result;

                    if (UserExist == null)
                    {
                        response.CodeError = 300;
                        response.Message = "Usuario No Existe";
                        response.Data = false;


                        data.Data = JsonConvert.SerializeObject(response);
                        data.Service = "Response Services GetFamilyByDocument";

                        Log = SaveLog(data);

                    }
                    else
                    {

                        parameters = new DynamicParameters();

                        parameters.Add("@Usuario", request.User, DbType.String, ParameterDirection.Input);
                        parameters.Add("@Cedula", request.Document, DbType.Int32, ParameterDirection.Input);

                        var ValidateFamily = conn.QueryFirstOrDefaultAsync<ResponseGetFamily>($"{EStoredProcedures.SP_GET_FAMILY_BY_DOCUMENT}", parameters, commandType: CommandType.StoredProcedure).Result;

                        if (ValidateFamily == null)
                        {
                            response.CodeError = 300;
                            response.Message = "Familiar no encontrado,verifique la informacion";
                            response.Data = false;


                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetFamilyByDocument";

                            Log = SaveLog(data);
                        }
                        else
                        {
                            response.CodeError = 200;
                            response.Message = "Consulta exitosa";
                            response.Data = ValidateFamily;


                            data.Data = JsonConvert.SerializeObject(response);
                            data.Service = "Response Services GetFamilyByDocument";

                            Log = SaveLog(data);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = ex.Message;

                StructureLogs data = new StructureLogs();

                data.Data = JsonConvert.SerializeObject(response);
                data.Service = "Error Catch Services GetFamilyByDocument";

                var Log = SaveLog(data);
            }

            return response;
        }

        #region Logs

        public ResponseGeneric SaveLog(StructureLogs request)
        {
            try
            {
                response = new ResponseGeneric();

                var Log = JsonConvert.SerializeObject(request);

                using (IDbConnection conn = Connection)
                {

                    if (request != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@Log", request.Data, DbType.String, ParameterDirection.Input);
                        parameters.Add("@Date", DateTime.Now , DbType.Date, ParameterDirection.Input);

                        var UserExist = conn.QueryFirstOrDefaultAsync<ResponseGetUser>($"{EStoredProcedures.SP_INSERT_LOG}", parameters, commandType: CommandType.StoredProcedure).Result;

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

            return response;
        }
        #endregion

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

                        if (Validate != null)
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
