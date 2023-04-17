using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApiXYZServices.DataObjects
{

    //Request Structure logs

    public class StructureLogs
    {
        public string Data { get; set; }

        public string Service { get; set; }
    }

    public class RequestValidateJWT
    {
        public string Bearer { get; set; }
    }

    public class RequestGenerateBearer
    {
        public string user { get; set; }

        public string password { get; set; }
    }


    //Request for Table User

    #region RequestforUser
    public class RequestCreateUser
    {
        public string user { get; set; }

        public string password { get; set; }

        public string Bearer { get; set; }
    }

    public class RequestDeleteUser
    {
        public string user { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestUpdateUserPass
    {
        public string user { get; set; }

        public string password { get; set; }

        public string NewPassword { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestUpdateUser
    {
        public string user { get; set; }

        public string password { get; set; }

        public string NewUser { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestGetUser
    {
        public string user { get; set; }

        public string Bearer { get; set; }


    }

    #endregion

    //Request for table Family

    #region RequestForFamily

    public class RequestCreateFamily
    {
        public string Usuario { get; set; }

        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string genero { get; set; }

        public string Parentesco { get; set; }

        public int Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestDeleteFamily
    {
        public string User { get; set; }

        public string Password { get; set; }

        public int Document { get; set; }
        public string Bearer { get; set; }

    }

    public class RequestUpdateFamily
    {
        public string Usuario { get; set; }

        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string genero { get; set; }

        public string Parentesco { get; set; }

        public int Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestGetFamilyByUser
    {
        public string User { get; set; }

        public string Bearer { get; set; }

    }

    public class RequestGetFamilyByDocument
    {
        public string User { get; set; }

        public int Document { get; set; }

        public string Bearer { get; set; }

    }
    #endregion


    #region RequestPost

    public class JsonPost
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }

    public class RequestCreatePost
    {
        public int UserId { get; set; }

        public string title { get; set; }
        
        public string body { get; set; }
    }

    public class RequestDeletePost
    {
        public int Id { get; set; }
    }


    public class RequestUpdatePost
    {

        public int UserId { get; set; }
        public int Id { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }

    public class RequestGetPostId
    {
        public int ID { get; set; }
    }
    #endregion

    #region RequestComments

    public class JsonComments 
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }

    }



    public class RequestCreateComments
    {
        public int PostId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }
    }

    public class RequestDeleteComments
    {
        public int Id { get; set; }
    }

    public class RequestUpdateComments
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }
    }

    public class RequestGetCommentsId
    {
        public int ID { get; set; }
    }
    #endregion
}
