USE [master]
GO
/****** Object:  Database [DataAdministrator]    Script Date: 17/04/2023 5:45:51 p. m. ******/
CREATE DATABASE [DataAdministrator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataAdministrator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DataAdministrator.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DataAdministrator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DataAdministrator_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DataAdministrator] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataAdministrator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataAdministrator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataAdministrator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataAdministrator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataAdministrator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataAdministrator] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataAdministrator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataAdministrator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataAdministrator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataAdministrator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataAdministrator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataAdministrator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataAdministrator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataAdministrator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataAdministrator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataAdministrator] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DataAdministrator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataAdministrator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataAdministrator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataAdministrator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataAdministrator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataAdministrator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataAdministrator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataAdministrator] SET RECOVERY FULL 
GO
ALTER DATABASE [DataAdministrator] SET  MULTI_USER 
GO
ALTER DATABASE [DataAdministrator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataAdministrator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataAdministrator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataAdministrator] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DataAdministrator] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DataAdministrator] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DataAdministrator', N'ON'
GO
ALTER DATABASE [DataAdministrator] SET QUERY_STORE = ON
GO
ALTER DATABASE [DataAdministrator] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DataAdministrator]
GO
/****** Object:  Table [dbo].[Bearer]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bearer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Bearer] [varchar](1000) NULL,
	[DATE_BEGIN] [date] NULL,
	[DATE_END] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[PostId] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameComments] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[body] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grupo_familiar]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grupo_familiar](
	[usuario] [varchar](50) NOT NULL,
	[cedula] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[genero] [varchar](10) NULL,
	[parentesco] [varchar](20) NULL,
	[edad] [int] NOT NULL,
	[menorEdad] [bit] NULL,
	[fecha_nacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[UserId] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NULL,
	[body] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services_Log]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services_Log](
	[Id_Log] [int] IDENTITY(1,1) NOT NULL,
	[Description_Log] [varchar](1000) NULL,
	[DATE_Log] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[Usuario] [varchar](50) NOT NULL,
	[contrasena] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[grupo_familiar]  WITH CHECK ADD  CONSTRAINT [fk_usuario] FOREIGN KEY([usuario])
REFERENCES [dbo].[usuarios] ([Usuario])
GO
ALTER TABLE [dbo].[grupo_familiar] CHECK CONSTRAINT [fk_usuario]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_COMMENTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[SP_CREATE_COMMENTS]
  @PostId int,
  @name varchar(50),
  @email varchar(50),
  @body varchar(1000)
  AS
  BEGIN
  insert into Comments values (@PostId,@name,@email,@body)
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_FAMILY]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[SP_CREATE_FAMILY]
@Usuario varchar(50),
@Cedula int,
@nombre Varchar(50),
@apellidos Varchar(50),
@Genero varchar(50),
@Parentesco varchar(50),
@Edad int,
@MenorEdad bit,
@FechaNacimiento Date
AS
BEGIN
Insert into grupo_familiar(usuario,cedula,nombre,apellidos,genero,parentesco,edad,menorEdad,fecha_nacimiento) values (@Usuario,@Cedula,@nombre,@apellidos,@Genero,@Parentesco,@Edad,@MenorEdad,@FechaNacimiento)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_POSTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[SP_CREATE_POSTS]
  @UserId int,
  @title varchar(50),
  @body varchar(1000)
  AS
  BEGIN
  insert into Posts values (@UserId,@title,@body)
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_USER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CREATE_USER]
    @usuario varchar(50),
    @contrasena varchar(50)
AS
BEGIN
    INSERT INTO usuarios (usuario, contrasena)
    VALUES (@usuario, @contrasena)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_COMMENTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_DELETE_COMMENTS]
  @id int
  AS
  BEGIN
  Delete from Comments where id = @id
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_FAMILY]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_DELETE_FAMILY]
@Usuario varchar(50),
@contrasena varchar(50),
@Cedula int
AS
BEGIN
Delete from grupo_familiar where @usuario = Usuario and cedula = @Cedula
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_POSTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[SP_DELETE_POSTS]
  @id int
  AS
  BEGIN
  Delete from Posts where id = @id
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DELETE_USER]
@usuario varchar(50)
AS
BEGIN
Delete from usuarios where @usuario = Usuario
End
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_COMMENTS_ALL]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[SP_GET_COMMENTS_ALL]
  AS
  BEGIN
  select * from Comments
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_COMMENTS_BY_ID]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   create procedure [dbo].[SP_GET_COMMENTS_BY_ID]
  @ID int
  AS
  BEGIN
  select * from Comments where id = @ID
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_FAMILY_BY_DOCUMENT]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GET_FAMILY_BY_DOCUMENT]
@Usuario varchar(50),
@Cedula int
AS
BEGIN
select * from grupo_familiar where usuario = @Usuario and cedula = @Cedula
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_FAMILY_BY_USER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GET_FAMILY_BY_USER]
@Usuario varchar(50)
AS
BEGIN
select * from grupo_familiar where usuario = @Usuario
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_LOGS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GET_LOGS]
AS
BEGIN
Select * from Services_Log
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_POSTS_ALL]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[SP_GET_POSTS_ALL]
  AS
  BEGIN
  select * from Posts
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_POSTS_BY_ID]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[SP_GET_POSTS_BY_ID]
  @ID int
  AS
  BEGIN
  select * from Posts where id = @ID
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_USER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_USER]
@usuario varchar(50)
AS
BEGIN
select * from usuarios where Usuario = @usuario
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_BEARER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_INSERT_BEARER]
@Bearer varchar(1000),
@DateBegin Date,
@DateEnd Date
AS
BEGIN
insert into Bearer(Bearer,DATE_BEGIN,DATE_END) values (@Bearer,@DateBegin,@DateEnd)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_LOG]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_INSERT_LOG]
@Log varchar(1000),
@Date Date
AS
BEGIN
Insert into Services_Log(Description_Log,DATE_Log) Values(@Log,@Date)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_COMMENTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[SP_UPDATE_COMMENTS]
  @id int,
  @PostId int,
  @name varchar(50),
  @email varchar(50),
  @body varchar(1000)
  AS
  BEGIN
  update Comments set PostId = @PostId,nameComments = @name,email = @email,body = @body where id = @id
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_FAMILY]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_UPDATE_FAMILY]
@Usuario varchar(50),
@Cedula int,
@nombre Varchar(50),
@apellidos Varchar(50),
@Genero varchar(50),
@Parentesco varchar(50),
@Edad int,
@MenorEdad bit,
@FechaNacimiento Date
AS
BEGIN
update grupo_familiar set cedula = @Cedula,nombre = @Cedula,apellidos = @apellidos,genero = @Genero,parentesco = @Parentesco,edad = @Edad,menorEdad = @MenorEdad,fecha_nacimiento = @FechaNacimiento where usuario = @Usuario and cedula = @Cedula
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_POSTS]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_UPDATE_POSTS]
  @id int,
  @UserId int,
  @title varchar(50),
  @body varchar(1000)
  AS
  BEGIN
  update Posts set UserId = @UserId,title = @title,body = @body where id = @id
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_USER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_USER]
@usuario varchar(50),
@contrasena varchar(50),
@newusuario varchar(50)
AS
BEGIN
update usuarios set Usuario = @newusuario where @usuario = usuario and contrasena = @contrasena
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_USER_PASSWORD]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_USER_PASSWORD]
@usuario varchar(50),
@contrasena varchar(50),
@newcontrasena varchar(50)
AS
BEGIN
update usuarios set contrasena = @newcontrasena where @usuario = usuario and contrasena = @contrasena
END
GO
/****** Object:  StoredProcedure [dbo].[SP_VALIDATE_BEARER]    Script Date: 17/04/2023 5:45:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_VALIDATE_BEARER]
@Bearer varchar(1000)
AS
BEGIN
select * from Bearer where Bearer = @Bearer
END
GO
USE [master]
GO
ALTER DATABASE [DataAdministrator] SET  READ_WRITE 
GO
