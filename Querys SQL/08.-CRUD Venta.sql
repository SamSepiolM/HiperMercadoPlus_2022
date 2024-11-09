USE PIA_MAD_GPO01

GO 

CREATE PROC SP_RegVenta(
  	@IdCajero				INT,  
	@TipoDocumento			VARCHAR(20),
	@NumeroDocumento		VARCHAR(20),
	@MontoPago				DECIMAL(10,2),
	@MontoCambio			DECIMAL(10,2),
	@MontoTotal				DECIMAL(10,2),
	@DescuentoT				DECIMAL(10,2),
	@IdCaja					INT,
	@Fecha					DATETIME,
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0

	BEGIN
		INSERT INTO Venta(IdCajero, TipoDocumento, NumeroDocumento, MontoPago, MontoCambio, MontoTotal,IdCaja, DescuentoT, Fecha)
		VALUES (@IdCajero, @TipoDocumento, @NumeroDocumento, @MontoPago, @MontoCambio, @MontoTotal, @IdCaja, @DescuentoT, @Fecha)
		SET @Resultado= SCOPE_IDENTITY()
	END

		SET @Mensaje = 'No se puede repetir el nombre del producto'
END

GO 


CREATE PROC SP_RegVentaDetalle(
  	@IdVenta				INT,  
	@IdProducto				INT,
	@NombreProducto			VARCHAR(15),
	@PrecioVenta			DECIMAL(10,2),
	@PrecioUnitario			DECIMAL(10,2),
	@Cantidad				DECIMAL(10,2),
	@Subtotal				DECIMAL(10,2),
	@Descuento				DECIMAL(10,2),
	@Utilidad				DECIMAL(10,2),
	@Unidad_medida			BIT,

	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0
	
	BEGIN
		INSERT INTO DetalleVenta(IdVenta, IdProducto, NombreProducto, PrecioVenta, PrecioUnitario, Cantidad, Subtotal, Descuento, Utilidad, Unidad_medida)
		VALUES (@IdVenta, @IdProducto, @NombreProducto, @PrecioVenta, @PrecioUnitario, @Cantidad, @Subtotal, @Descuento, @Utilidad, @Unidad_medida)


		SET @Resultado= SCOPE_IDENTITY()
	END

		SET @Mensaje = 'No se puede repetir el nombre del producto'
END
GO 

CREATE PROC SP_ModifVenta(
	@IdVenta				INT,
	@IdCajero				INT,  
	@TipoDocumento			VARCHAR(20),
	@NumeroDocumento		VARCHAR(20),
	@MontoPago				DECIMAL(10,2),
	@MontoCambio			DECIMAL(10,2),
	@MontoTotal				DECIMAL(10,2),
	@IdCaja					INT,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Venta WHERE MontoCambio = @MontoCambio and IdVenta != @IdVenta)
		BEGIN

			UPDATE Venta SET 
			IdCajero = @IdCajero, 
			TipoDocumento = @TipoDocumento, 
			NumeroDocumento = @NumeroDocumento,
			MontoPago = @MontoPago, 
			MontoCambio = @MontoCambio, 
			MontoTotal = @MontoTotal,
			IdCaja = @IdCaja
			WHERE IdVenta = @IdVenta
		END
	ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'Ya existe este nombre de producto'
		END
END

GO 

CREATE PROC SP_ElimVenta(
	@IdVenta				INT,
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM DetalleVenta DV
	INNER JOIN Venta V ON V.IdVenta = DV.IdVenta
	WHERE V.IdVenta = @IdVenta
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar la venta ya que contiene productos\n'
		
	END

	IF EXISTS (SELECT * FROM NotaCredito NC
	INNER JOIN Venta V ON V.IdVenta = NC.IdTicketV
	WHERE V.IdVenta = @IdVenta
	)
	BEGIN 
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar la venta ya que tiene historial\n'
	END
	
	IF(@pasoreglas = 1)
	BEGIN
		DELETE FROM Venta WHERE IdVenta = @IdVenta
		set @Respuesta = 1
	END
END
GO

CREATE PROC SP_IniciarTran(
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN TRAN Venta

	SET @Respuesta = 0
	SET @Mensaje = ''

	BEGIN
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar la venta ya que contiene productos\n'
		
	END

GO

CREATE PROC SP_ConfirmarTran(
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN 
COMMIT TRAN Venta

	SET @Respuesta = 0
	SET @Mensaje = ''

	BEGIN
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar la venta ya que contiene productos\n'
		
	END
END
GO

CREATE PROC SP_CancelarTran(
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN 
ROLLBACK TRAN Venta

	SET @Respuesta = 0
	SET @Mensaje = ''

	BEGIN
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar la venta ya que contiene productos\n'
		
	END
END
GO