USE PIA_MAD_GPO01
SELECT * FROM Empleado
GO

CREATE PROC SP_RegEmpleado(
	@Contrasenia			VARCHAR(20),
	@Nombre					VARCHAR(20),
	@ApePaterno				VARCHAR(10),
	@ApeMaterno				VARCHAR(10),
	@CURP					VARCHAR(20),
	@NumNomina				VARCHAR(10),
	@Correo					VARCHAR(30),
	@Telefono				VARCHAR (12),
	@Domicilio				VARCHAR(40),
	@IdPuesto				INT,
	@Estado					BIT,
	@IdEmpleadoResultado	INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN 
	SET @IdEmpleadoResultado = 0
	SET @Mensaje = ''

	if not exists (SELECT * FROM Empleado WHERE CURP = @CURP)
	BEGIN 
		INSERT INTO Empleado (Contrasenia, Nombre, ApePaterno, ApeMaterno, CURP, NumNomina, Correo,	Telefono, Domicilio, IdPuesto, Estado) 
		VALUES (@Contrasenia, @Nombre, @ApePaterno, @ApeMaterno, @CURP, @NumNomina, @Correo, @Telefono, @Domicilio, @IdPuesto, @Estado)

		SET @IdEmpleadoResultado = SCOPE_IDENTITY()
		
	END
	ELSE
		SET @Mensaje = 'Error en los datos ingresados, favor de revisarlos'
END

GO

CREATE PROC SP_EditEmpleado(
	@IdEmpleado				INT,
	@Contrasenia			VARCHAR(20),
	@Nombre					VARCHAR(20),
	@ApePaterno				VARCHAR(10),
	@ApeMaterno				VARCHAR(10),
	@CURP					VARCHAR(20),
	@NumNomina				VARCHAR(10),
	@Correo					VARCHAR(30),
	@Telefono				VARCHAR (12),
	@Domicilio				VARCHAR(40),
	@IdPuesto				INT,
	@Estado					BIT,
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN 
	SET @Respuesta = 0
	SET @Mensaje = ''

	if not exists (SELECT * FROM Empleado WHERE CURP = @CURP and IdEmpleado != @IdEmpleado)
	BEGIN 
		UPDATE Empleado SET
		Contrasenia = @Contrasenia, 
		Nombre = @Nombre, 
		ApePaterno = @ApePaterno, 
		ApeMaterno = @ApeMaterno, 
		CURP = @CURP, 
		NumNomina = @NumNomina, 
		Correo = @Correo,	
		Telefono = @Telefono, 
		Domicilio = @Domicilio, 
		IdPuesto = @IdPuesto, 
		Estado = @Estado
		WHERE IdEmpleado = @IdEmpleado

		SET @Respuesta = 1
		
	END
	ELSE
		SET @Mensaje = 'Error en los datos ingresados, favor de revisarlos'
END

GO

CREATE PROC SP_ElimEmpleado(
	@IdEmpleado				INT,
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN 
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM Venta V
	inner join Empleado E on E.IdEmpleado = V.IdCajero
	WHERE E.IdEmpleado = @IdEmpleado
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar el usuario ya que esta relacionado a una venta\n'
	END

	IF EXISTS (SELECT * FROM NotaCredito N
	inner join Empleado E on E.IdEmpleado = N.IdAdmin
	WHERE E.IdEmpleado = @IdEmpleado
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar el usuario ya que esta relacionado a una Nota de credito\n'
	END

	if(@pasoreglas = 1)
	BEGIN
		DELETE FROM Empleado WHERE IdEmpleado = @IdEmpleado 
		SET @Respuesta = 1
	END
END

