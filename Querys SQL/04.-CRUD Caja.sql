USE PIA_MAD_GPO01

GO

CREATE PROC SP_RegCaja(
	@Num_caja				VARCHAR(10),
	@Estado					BIT,
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS 
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Caja WHERE Num_caja = @Num_caja)
	BEGIN
		INSERT INTO Caja (Num_Caja, Estado) VALUES (@Num_Caja, @Estado)
		SET @Resultado= SCOPE_IDENTITY()
	END
	ELSE 
		SET @Mensaje = 'El numero de caja no puede repetirse'
END

GO

CREATE PROC SP_EditCaja(
	@Id						INT,
	@Num_caja				VARCHAR(10),
	@Estado					BIT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Caja WHERE Num_caja = @Num_caja AND Id != @Id)
	BEGIN
		UPDATE Caja SET
		Num_caja = @Num_caja,
		Estado = @Estado
		WHERE Id = @Id
	END
	ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'El numero de caja no puede repetirse'
		END
END

GO

CREATE PROC SP_ElimCaja(
	@Id						INT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS 
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (
		SELECT * FROM Caja C
		INNER JOIN Venta V ON V.IdCaja = C.Id
		WHERE C.Id = @Id

	)
	BEGIN
		DELETE TOP(1) FROM Caja WHERE Id = @Id
	END
		ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'Esta caja cuenta con ventas'
		END
END
