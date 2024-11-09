USE PIA_MAD_GPO01

GO 

CREATE PROC SP_RegMetodoPago(
  	@IdMetodo				INT,  
	@IdVentaM				INT,
	@MontoPago				DECIMAL(10,2),
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0

	BEGIN
		INSERT INTO DetalleMetodoPago(IdMetodo, MontoPago, IdVentaM)
		VALUES (@IdMetodo, @MontoPago, @IdVentaM)
		SET @Resultado= SCOPE_IDENTITY()
	END

		SET @Mensaje = 'No se puede repetir el nombre del producto'
END

GO 

CREATE PROC SP_ModifMetodoPago(
	@IdVentaM				INT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 1
		BEGIN

			UPDATE DetalleMetodoPago SET 
			IdVentaM = @IdVentaM
			WHERE IdVentaM IS NULL
		END
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'Ya existe este nombre de producto'
		END
END

GO 