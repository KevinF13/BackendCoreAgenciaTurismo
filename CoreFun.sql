---------Base de datos Agencia de Turismo-------
---------Kevin Fajardo-------
--Creacion de Base de datos
CREATE DATABASE Agencia3
USE master
Go
if exists(
    Select name
    from sys.sysdatabases
    where name = 'Agencia3')
        drop database Agencia3
create database Agencia3
go
USE Agencia3
Go

--/Creacion tabla Plan de viaje/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Cliente')
			drop table Cliente 
	else
		
		create table Cliente
		(
		idCliente int identity(1,1) primary key not null,
		nombre varchar(60) not null,
		apellido varchar(60) not null,
		cedula char(10) not null,
		celular varchar(15) not null,
		direccion varchar(100) not null
		)
		go

		--/Creacion tabla Usuarios para Login/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Usuario')
			drop table Usuario 
	else
		
		create table Usuario
		(
		idUsuario int identity(1,1) primary key not null,
		idUsuario_Cliente int not null,
		email varchar(60) not null,
		contrasena varchar(60) not null
		CONSTRAINT fkusuario_cliente FOREIGN KEY (idUsuario_Cliente) REFERENCES Cliente(idCliente)
		)
		go

--/Creacion tabla de Preferencias/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Preferencia')
			drop table Preferencia 
	else
		
		create table Preferencia
		(
		idPreferencia int identity(1,1) primary key not null,
		idPreferencia_Cliente int not null,
		nombre varchar(60) not null,
		CONSTRAINT fkpreferencia_cliente FOREIGN KEY (idPreferencia_Cliente) REFERENCES Cliente(idCliente)
		)
		go



----/Creacion tabla de Compra/
----/Fecha última modificación: 05/06/2023/
--	if exists(
--		select * from sys.sysobjects
--		where type = 'U' and name = 'Compra')
--			drop table Compra 
--	else
		
--		create table Compra
--		(
--		idCompra int identity(1,1) primary key not null,
--		idCompra_Cliente int not null,
--		precio decimal,
--		fechaCompra date default getdate(),
--		CONSTRAINT fkcompra_cliente FOREIGN KEY (idCompra_Cliente) REFERENCES Cliente(idCliente)
--		)
--		go

--/Creacion tabla de Viaje/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Viaje')
			drop table Viaje 
	else
		
		create table Viaje
		(
		idViaje int identity(1,1) primary key not null,
		nombre varchar(60) not null,
		duracion int,
		descripcion varchar(60) not null,
		precio decimal,
		)
		go

--/Creacion tabla de Categoria/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Categoria')
			drop table Categoria 
	else
		
		create table Categoria
		(
		idCategoria int identity(1,1) primary key not null,
		idCategoria_Viaje int not null,
		nombre varchar(60) not null,
		CONSTRAINT fkcategoria_viaje FOREIGN KEY (idCategoria_Viaje) REFERENCES Viaje(idViaje)
		)
		go


--/Creacion tabla de Agencia/
--/Fecha última modificación: 05/06/2023/
	if exists(
		select * from sys.sysobjects
		where type = 'U' and name = 'Agencia')
			drop table Agencia 
	else
		
		create table Agencia
		(
		idAgencia int identity(1,1) primary key not null,
		idAgencia_Cliente int not null,
		idAgencia_Viaje int not null,
		fechaCompra date default getdate(),
		CONSTRAINT fkagencia_cliente FOREIGN KEY (idAgencia_Cliente) REFERENCES Cliente(idCliente),
		CONSTRAINT fkagencia_viaje FOREIGN KEY (idAgencia_Viaje) REFERENCES Viaje(idViaje)
		)
		go


--Verificador de LOGIN
CREATE PROCEDURE VerificarUsuario
    @correoElectronico VARCHAR(50),
    @contrasena VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @usuarioExiste INT;
    
    SELECT @usuarioExiste = COUNT(*) 
    FROM Usuario 
    WHERE email = @correoElectronico 
    AND contrasena = @contrasena;

    IF @usuarioExiste > 0
        SELECT 'Autenticación exitosa.'
    ELSE
        SELECT 'El correo electrónico o la contraseña son incorrectos.'
END

--/Procedimiento almacenado para realizar AGREGAR CLIENTE /
--/Fecha última modificación: 01 de abril 2023/
CREATE PROCEDURE Clientes_add
	@nombre varchar(60),
	@apellido varchar(60),
	@cedula char(10),
	@celular varchar(15)
AS
BEGIN
	INSERT INTO Cliente(nombre,apellido,cedula,celular)
	VALUES(@nombre,
	@apellido,
	@cedula,
	@celular)
END


select * from Usuario
select * from Cliente

select c.*, u.*, p.*,v.*,ca.nombre from Cliente c 
inner join Usuario u on c.idCliente = u.idUsuario
inner join Preferencia p on c.idCliente = p.idPreferencia 
inner join Agencia a on c.idCliente = a.idAgencia_Cliente
inner join Viaje v on a.idAgencia_Viaje = v.idViaje
inner join Categoria ca on v.idViaje = ca.idCategoria_Viaje



select * from Categoria
select * from Preferencia
select c.*, p.*,v.*,ca.nombre from Cliente c 
inner join Preferencia p on c.idCliente = p.idPreferencia 
inner join Agencia a on c.idCliente = a.idAgencia_Cliente
inner join Viaje v on a.idAgencia_Viaje = v.idViaje
inner join Categoria ca on v.idViaje = ca.idCategoria_Viaje



insert Cliente Values ('Kevin','Fajardo', '172638987', '45549488', 'Quito')
insert Usuario Values ('1','kf@gmail.com', '123')
insert Preferencia Values ('1','Aventura')
insert Viaje Values ('Cotopaxi',5, 'Viaje de caminata', 25)
insert Categoria Values (1,'Aventura')

--------------------------------------------PROC DE VIAJES--------------------------------------------

--/Procedimiento almacenado para realizar consultas en Viajes/
--/Fecha última modificación: 05112023/
CREATE PROC Viajes_all
as
SELECT idViaje, nombre, duracion, descripcion, precio FROM Viaje
ORDER BY idViaje ASC
GO
--/Procedimiento almacenado para realizar AGREGAR Viajes/
--/Fecha última modificación: 05112023/
CREATE PROC Viajes_add
@nombre varchar(60),
@duracion varchar(60),
@descripcion varchar(60),
@precio decimal
AS
INSERT INTO Viaje(nombre,duracion,descripcion,precio)
VALUES(@nombre,
@duracion,
@descripcion,
@precio)
GO

--/Procedimiento almacenado para realizar ACTUALIZAR Viajes/
--/Fecha última modificación: 05112023/
CREATE PROC Viajes_update
@id int,
@nombre varchar(60),
@duracion varchar(60),
@descripcion varchar(60),
@precio decimal
AS
UPDATE Viaje
SET nombre = @nombre,
duracion = @duracion,
descripcion = @descripcion,
precio = @precio
WHERE idViaje = @id
GO
--/Procedimiento almacenado para realizar ELIMINAR Viajes/
--/Fecha última modificación: 05112023/
CREATE PROC Viajes_delete
@id int
AS
DELETE FROM Viaje
WHERE idViaje = @id
GO

