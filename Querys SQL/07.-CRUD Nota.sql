USE PIA_MAD_GPO01

GO 

CREATE PROC SP_RegNota(
  	@IdAdmin				INT,  
	@IdCaja					INT,
	@IdTicketV				INT,
	@MontoCambio			DECIMAL(10,2),
	@Fecha					DATETIME,
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0

	BEGIN
		INSERT INTO NotaCredito(IdAdmin, IdCaja, IdTicketV, MontoCambio, Fecha)
		VALUES (@IdAdmin, @IdCaja, @IdTicketV, @MontoCambio, @Fecha)
		SET @Resultado= SCOPE_IDENTITY()
	END

		SET @Mensaje = 'No se puede repetir el nombre del producto'
END

GO 

CREATE PROC SP_RegNotaDetalle(
  	@Id_Num_Dev				INT,  
	@IdProDev				INT,
	@Prod_regresado			VARCHAR(15),
	@Cantidad				DECIMAL(10,2),
	@Subtotal				DECIMAL(10,2),
	@Total					DECIMAL(10,2),
	@DescuentoP				DECIMAL(10,2),
	@PrecioUni				DECIMAL(10,2),
	@Unidad_medida			BIT, 
	@Merma					BIT, 
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0

	BEGIN
		INSERT INTO NotaCreditoDetalle(Id_Num_Dev, IdProDev, Prod_regresado, Cantidad, Subtotal, Total, DescuentoP, PrecioUni, Unidad_medida, Merma)
		VALUES (@Id_Num_Dev, @IdProDev, @Prod_regresado, @Cantidad, @Subtotal, @Total, @DescuentoP, @PrecioUni, @Unidad_medida, @Merma)
		SET @Resultado= SCOPE_IDENTITY()
	END

	

		SET @Mensaje = 'No se puede repetir el nombre del producto'
END

GO 