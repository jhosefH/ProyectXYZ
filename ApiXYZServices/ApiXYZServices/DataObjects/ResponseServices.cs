namespace ApiXYZServices.DataObjects
{

    public class ResponseGetUser
    {
        public string usuario { get; set; }

        public string Contrasena { get; set; }
    }

    public class ResponseGetFamily
    {
        public string Usuario { get; set; }

        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string genero { get; set; }

        public string Parentesco { get; set; }

        public int Edad { get; set; }

        public bool MenorEdad { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }

    public class ResponseValidateBearer
    {
        public string Bearer { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }

    public class ResponseGeneric
    {

        public int CodeError { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }


    }


    public class ResponseJsonPosts
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }


    public class ResponseJsonComments
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }


}
