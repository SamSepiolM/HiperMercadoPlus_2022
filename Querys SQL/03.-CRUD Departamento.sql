USE PIA_MAD_GPO01

GO

CREATE PROC SP_RegDepto(
	@Nombre					VARCHAR(20),
	@Descuento				INT,
	@DescActivo				BIT,
	@Devolucion				BIT,
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Departamento WHERE Nombre = @Nombre)
	BEGIN
		INSERT INTO Departamento (Nombre, Descuento, DescActivo, Devolucion) VALUES (@Nombre, @Descuento, @DescActivo, @Devolucion)
		SET @Resultado= SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el departamento'
END

GO

CREATE PROC SP_EditDepto(
	@Clave					INT,
	@Nombre					VARCHAR(20),
	@Descuento				INT,
	@DescActivo				BIT,
	@Devolucion				BIT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Departamento WHERE Nombre = @Nombre and Clave != @Clave)
	BEGIN
		UPDATE Departamento SET
		Nombre = @Nombre,
		Descuento = @Descuento,
		DescActivo = @DescActivo,
		Devolucion = @Devolucion
		WHERE Clave = @Clave
	END
	ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'No se puede repetir el departamento'
		END
END

GO

CREATE PROC SP_ElimDepto(
	@Clave					INT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (
	SELECT * FROM Departamento D
	INNER JOIN Producto P ON P.Depto = D.Clave
	WHERE D.Clave = @Clave
	
	)
	BEGIN
		DELETE TOP(1) FROM Departamento WHERE Clave = @Clave
	END
	ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'Este departamento cuenta con productos'
		END
END