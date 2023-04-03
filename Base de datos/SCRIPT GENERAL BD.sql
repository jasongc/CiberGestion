CREATE DATABASE bdSesion
GO
USE bdSesion
GO
--======== INCIO CREACIÓN TABLAS======--
CREATE TABLE Perfil(
	iIdPerfil INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	vDescripcionInterna VARCHAR(50),
	vDescripcion VARCHAR(50)
)
GO
CREATE TABLE Estado(
	siEstado SMALLINT NOT NULL,
	vDescripcionInterna VARCHAR(30) NOT NULL,
	vDescripcion VARCHAR(30) NOT NULL
)
GO
CREATE TABLE Usuario(
	iIdUsuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	iIdPerfil INT FOREIGN KEY REFERENCES Perfil(iIdPerfil) NOT NULL,
	vNombres VARCHAR(150) NOT NULL,
	vApellidoMaterno VARCHAR(150) NOT NULL,
	vApellidoPaterno VARCHAR(150) NOT NULL,
	vEmail varchar(250) NOT NULL,
	vContrasenia VARCHAR(30) NOT NULL,
	siEstado SMALLINT NOT NULL
)
GO
CREATE TABLE UsuarioLoginHistorico(
	iIdUsuarioLoginHistorico INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	iIdUsuario INT FOREIGN KEY REFERENCES Usuario(iIdUsuario) NOT NULL,
	siIntentos SMALLINT NOT NULL,
	vJWT NVARCHAR(MAX) NULL,
	dtFechaInicioSesion DATETIME NULL,
	dtFechaCierreSesion DATETIME NULL
)
GO
CREATE TABLE UsuarioContraseniaHistorico(
	iIdUsuarioContraseniaHistorico INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	iIdUsuario INT FOREIGN KEY REFERENCES Usuario(iIdUsuario) NOT NULL,	
	vContrasenia VARCHAR(30) NOT NULL,
	dtFechaCrea DATETIME NOT NULL
)
GO
--======== FIN CREACIÓN TABLAS======--
--======== INCIO CREACIÓN PROCEDURES======--
CREATE PROCEDURE RegistrarIntentoFallidoLogin
	@piIdUsuario INT
AS
BEGIN
	DECLARE @iIdUsuarioLoginHistorico INT,
			@siIntentos SMALLINT,
			@siEstadoBloqueadoLogin SMALLINT = (SELECT TOP 1 siEstado FROM Estado WHERE vDescripcionInterna = 'BLOQUEO_LOGIN')

	SELECT TOP 1
		@iIdUsuarioLoginHistorico =  iIdUsuarioLoginHistorico,
		@siIntentos = siIntentos
	FROM UsuarioLoginHistorico 
	WHERE iIdUsuario = @piIdUsuario
	AND dtFechaCierreSesion IS NULL
	AND dtFechaInicioSesion IS NULL

	UPDATE UsuarioLoginHistorico SET
		siIntentos = @siIntentos + 1
	WHERE iIdUsuarioLoginHistorico = @iIdUsuarioLoginHistorico

	EXEC RegistrarCierreLogin @piIdUsuario = @piIdUsuario

	IF @iIdUsuarioLoginHistorico IS NULL
	BEGIN
		INSERT INTO UsuarioLoginHistorico(
			iIdUsuario,
			vJWT,
			siIntentos,
			dtFechaInicioSesion
		)
		VALUES(
			@piIdUsuario,
			NULL,
			1,
			NULL
		)
	END

	IF @siIntentos >= 3
	BEGIN		
		UPDATE Usuario SET 
			siEstado = @siEstadoBloqueadoLogin
		WHERE iIdUsuario = @piIdUsuario

		RAISERROR('Tiene muchos intentos fallidos y su cuenta ha sido bloqueada, por favor comuníquese con el adminsitrador.', 16, 1)
	END
END
GO
CREATE PROCEDURE RegistrarCierreLogin
	@piIdUsuario INT
AS
BEGIN
	UPDATE UsuarioLoginHistorico SET 
		dtFechaCierreSesion = GETDATE()
	WHERE iIdUsuario = @piIdUsuario
	AND dtFechaCierreSesion IS NULL
	AND dtFechaInicioSesion IS NOT NULL
END
GO
CREATE PROCEDURE RegistrarInicioLogin
	@piIdUsuario INT,
	@pvJWT NVARCHAR(MAX)
AS
BEGIN
	DECLARE @iIdUsuarioLoginHistorico INT,
			@siIntentos SMALLINT,
			@dtFechaInicioSesion DATETIME,
			@dtFechaAcceso DATETIME = GETDATE(),
			@bRegistrar BIT = 0

	SELECT TOP 1
		@iIdUsuarioLoginHistorico =  iIdUsuarioLoginHistorico,
		@siIntentos = siIntentos,
		@dtFechaInicioSesion = dtFechaInicioSesion
	FROM UsuarioLoginHistorico 
	WHERE iIdUsuario = @piIdUsuario
	AND dtFechaCierreSesion IS NULL

	IF ISNULL(@iIdUsuarioLoginHistorico,0) <> 0 AND @dtFechaInicioSesion IS NOT NULL
	BEGIN
		EXEC RegistrarCierreLogin @piIdUsuario = @piIdUsuario

		SET @bRegistrar = 1

	END ELSE IF ISNULL(@iIdUsuarioLoginHistorico,0) <> 0 AND @dtFechaInicioSesion IS NULL
	BEGIN
		UPDATE UsuarioLoginHistorico SET 
			dtFechaInicioSesion = @dtFechaAcceso,
			vJWT = @pvJWT
		WHERE iIdUsuarioLoginHistorico = @iIdUsuarioLoginHistorico
	END ELSE
	BEGIN
		SET @bRegistrar = 1
	END

	IF @bRegistrar = 1
	BEGIN
		INSERT INTO UsuarioLoginHistorico(
			iIdUsuario,
			vJWT,
			siIntentos,
			dtFechaInicioSesion
		)
		VALUES(
			@piIdUsuario,
			@pvJWT,
			1,
			@dtFechaAcceso
		)
	END

	
	SELECT @dtFechaAcceso

END
GO
CREATE PROCEDURE CambiarContrasenia
	@piIdUsuario int,
	@pvContrasenia VARCHAR(30),
	@pvNuevaContrasenia VARCHAR(30)
AS
BEGIN TRY
	BEGIN TRANSACTION
		
		SELECT TOP 3
			iIdUsuario,
			vContrasenia,
			dtFechaCrea
		INTO #tmp_UsuarioContraseniaHistorico
		FROM UsuarioContraseniaHistorico WITH(NOLOCK)
		WHERE iIdUsuario = @piIdUsuario
		ORDER BY dtFechaCrea DESC

		--IF EXISTS(SELECT 
		--	  		iIdUsuario 
		--		  FROM #tmp_UsuarioContraseniaHistorico
		--		  GROUP BY iIdUsuario
		--		  HAVING COUNT(iIdUsuario) >= 3)
		--BEGIN
		--	 RAISERROR('La contraseña no puede ser modifica más de 3 veces, por favor comuníquese con su administrador.', 16, 1)
		--END
		--ELSE 
		IF EXISTS(SELECT '1' 
					   FROM #tmp_UsuarioContraseniaHistorico 
					   WHERE vContrasenia = @pvNuevaContrasenia) OR @pvContrasenia = @pvNuevaContrasenia
		BEGIN
			 RAISERROR('Ups! parece que ya utilizó esta contraseña en las últimas 3 actualizaciones, por favor utilice otra.', 16, 1)
		END 
		ELSE IF NOT EXISTS(SELECT 1 FROM Usuario WHERE iIdUsuario = @piIdUsuario AND vContrasenia = @pvContrasenia)
		BEGIN
			 RAISERROR('Ups! parece que su contraseña actual es incorrecta.', 16, 1)
		END
		ELSE
		BEGIN
			INSERT INTO UsuarioContraseniaHistorico(
				iIdUsuario,
				vContrasenia,
				dtFechaCrea
			)
			SELECT 
				iIdUsuario,
				vContrasenia,
				GETDATE()
			FROM Usuario
			WHERE iIdUsuario = @piIdUsuario

			UPDATE Usuario SET 
				vContrasenia = @pvNuevaContrasenia
			WHERE iIdUsuario = @piIdUsuario

		END

		IF OBJECT_ID('tempdb..#tmp_UsuarioContraseniaHistorico') IS NOT NULL DROP TABLE #tmp_UsuarioContraseniaHistorico

	COMMIT TRANSACTION
END TRY
BEGIN CATCH

	DECLARE @Message varchar(MAX) = ERROR_MESSAGE(),
		@Severity int = ERROR_SEVERITY(),
		@State smallint = ERROR_STATE()

	ROLLBACK TRANSACTION

	IF OBJECT_ID('tempdb..#tmp_UsuarioContraseniaHistorico') IS NOT NULL DROP TABLE #tmp_UsuarioContraseniaHistorico
	RAISERROR(@Message, @Severity, @State)
END CATCH
GO
CREATE PROCEDURE ObtenerUsuarioLogin
	@pvEmail VARCHAR(250)
AS
BEGIN
	DECLARE @siEstadoEliminado SMALLINT,
			@iIdUsuario INT,
			@siEstadoUsuario SMALLINT

	SELECT 
		@siEstadoEliminado =  siEstado
	FROM Estado WHERE vDescripcionInterna = 'ELIMINADO'

	SELECT TOP 1
		@iIdUsuario = iIdUsuario,
		@siEstadoUsuario = siEstado
	FROM Usuario WHERE vEmail = @pvEmail
	AND siEstado <> @siEstadoEliminado

	IF EXISTS(SELECT 
				siEstado 
			  FROM Estado 
			  WHERE vDescripcionInterna = 'BLOQUEO_LOGIN' 
			  AND siEstado = @siEstadoUsuario) 
	BEGIN
		RAISERROR('Tiene muchos intentos fallidos y su cuenta ha sido bloqueada, por favor comuníquese con el adminsitrador.', 16, 1)
	END
	ELSE IF ISNULL(@iIdUsuario, 0) = 0
	BEGIN
		RAISERROR('El usuario con el que intenta ingresar no existe.', 16, 1)
	END


	--EXEC RegistrarInicioLogin @piIdUsuario = @iIdUsuario

	SELECT 
		iIdUsuario,
		U.iIdPerfil,
		P.vDescripcionInterna AS 'vPerfil',
		vNombres,
		vApellidoMaterno,
		vApellidoPaterno,
		vEmail,
		vContrasenia
	FROM Usuario U WITH(NOLOCK)
	INNER JOIN Perfil P WITH(NOLOCK) ON U.iIdPerfil = P.iIdPerfil
	WHERE iIdUsuario = @iIdUsuario
END
GO
--======== FIN CREACIÓN PROCEDURES======--
--======== INCIO CREACIÓN DATA======--
GO
INSERT INTO Estado VALUES(1, 'ACTIVO', 'Activo')
INSERT INTO Estado VALUES(2, 'ELIMINADO', 'Eliminado')
INSERT INTO Estado VALUES(3, 'BLOQUEO_LOGIN', 'Bloqueado por login')
GO
INSERT INTO Perfil ( vDescripcionInterna, vDescripcion ) VALUES('WEBMASTER', 'Webmaster')
INSERT INTO Perfil ( vDescripcionInterna, vDescripcion ) VALUES('INVITADO', 'Invitado')
GO
INSERT INTO Usuario(
	iIdPerfil,
	vNombres,
	vApellidoMaterno,
	vApellidoPaterno,
	vEmail,
	vContrasenia,
	siEstado
)
VALUES(
	1,
	'JASON JOSEPH',
	'GUTIERREZ',
	'CUADROS',
	'jason.gutierrez.dev@gmail.com',
	'J@s0n123456789',
	1
)
INSERT INTO Usuario(
	iIdPerfil,
	vNombres,
	vApellidoMaterno,
	vApellidoPaterno,
	vEmail,
	vContrasenia,
	siEstado
)
VALUES(
	1,
	'EDWID LUCIANO',
	'MEXICANO',
	'JOAQUIN',
	'joaquin.mexicano.dev@gmail.com',
	'J0@q1n123456789',
	1
)
INSERT INTO Usuario(
	iIdPerfil,
	vNombres,
	vApellidoMaterno,
	vApellidoPaterno,
	vEmail,
	vContrasenia,
	siEstado
)
VALUES(
	2,
	'JENNIFER LUCERO',
	'PAREDES',
	'RAMOS',
	'lucero.paredes.inv@gmail.com',
	'Luc3r0@123456789',
	1
)
INSERT INTO Usuario(
	iIdPerfil,
	vNombres,
	vApellidoMaterno,
	vApellidoPaterno,
	vEmail,
	vContrasenia,
	siEstado
)
VALUES(
	2,
	'JARED',
	'PAREDES',
	'RAMOS',
	'jared.paredes.inv@gmail.com',
	'J@r3d123456789',
	1
)
INSERT INTO Usuario(
	iIdPerfil,
	vNombres,
	vApellidoMaterno,
	vApellidoPaterno,
	vEmail,
	vContrasenia,
	siEstado
)
VALUES(
	2,
	'MAX',
	'GUTIERREZ',
	'CUADROS',
	'max.gutierrez.inv@gmail.com',
	'M@x123456789',
	1
)
--======== FIN CREACIÓN DATA======--

select * from Estado
select * from Perfil
select * from Usuario