USE PIA_MAD_GPO01

GO 

CREATE PROC SP_RegProd(
	@Nombre					VARCHAR(20),
	@Descripcion			VARCHAR(90),
	@Unidad_medida			BIT, 
	@Costo					DECIMAL(10,2), 
	@Precio_unitario		DECIMAL(10,2), 
	@Existencia				DECIMAL(10,2), 
	@Stock					BIT,
	@Punto_reorden			DECIMAL(10,2), 
	@Depto					INT,
	@IdAdminMod				INT,
	@DescuentoProducto		DECIMAL(10,2),
	@FechaRegistro			DATETIME,
	@FechaModificacion		DATETIME,
	@FechaInicio			DATETIME,
	@FechaFin				DATETIME,
	@Resultado				INT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Producto WHERE Nombre = @Nombre)
	BEGIN
		INSERT INTO Producto(Nombre, Descripcion, Unidad_medida, Costo, Precio_unitario, Existencia,Stock, Punto_reorden, Depto, IdAdminMod, DescuentoProducto, FechaRegistro, FechaModificacion, FechaInicio, FechaFin)
		VALUES (@Nombre, @Descripcion, @Unidad_medida, @Costo, @Precio_unitario, @Existencia, @Stock, @Punto_reorden, @Depto, @IdAdminMod, @DescuentoProducto, @FechaRegistro, @FechaModificacion, @FechaInicio, @FechaFin)
		SET @Resultado= SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el nombre del producto'
END

GO 

CREATE PROC SP_ModifProd(
	@Codigo_producto		INT,
	@Nombre					VARCHAR(20),
	@Descripcion			VARCHAR(90),
	@Unidad_medida			BIT, 
	@Costo					DECIMAL(10,2), 
	@Precio_unitario		DECIMAL(10,2), 
	@Existencia				DECIMAL(10,2), 
	@Stock					BIT,
	@Punto_reorden			DECIMAL(10,2), 
	@Depto					INT,
	@IdAdminMod				INT,
	@DescuentoProducto		DECIMAL(10,2),
	@FechaModificacion		DATETIME,
	@FechaInicio			DATETIME,
	@FechaFin				DATETIME,
	@Resultado				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Producto WHERE Nombre = @Nombre and Codigo_producto != @Codigo_producto)
		BEGIN

			UPDATE Producto SET 
			Nombre = @Nombre, 
			Descripcion = @Descripcion, 
			Unidad_medida = @Unidad_medida,
			Costo = @Costo, 
			Precio_unitario = @Precio_unitario, 
			Existencia = @Existencia,
			Stock = @Stock, 
			Punto_reorden = @Punto_reorden, 
			Depto = @Depto,
			IdAdminMod=@IdAdminMod,
			DescuentoProducto=@DescuentoProducto,
			FechaModificacion= @FechaModificacion,
			FechaInicio= @FechaInicio,
			FechaFin= @FechaFin
			WHERE Codigo_producto = @Codigo_producto
		END
	ELSE
		BEGIN
			SET @Resultado = 0
			SET @Mensaje = 'Ya existe este nombre de producto'
		END
END

GO 

SELECT * FROM Producto

CREATE PROC SP_ElimProd(
	@Codigo_producto		INT,
	@Respuesta				BIT OUTPUT,
	@Mensaje				VARCHAR(500) OUTPUT
)
AS
BEGIN

	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM DetalleVenta DV
	INNER JOIN Producto P ON P.Codigo_producto = DV.IdProducto
	WHERE P.Codigo_producto = @Codigo_producto
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar el producto ya que se ha vendido antes\n'
		
	END

	IF EXISTS (SELECT * FROM NotaCreditoDetalle NC
	INNER JOIN Producto P ON P.Codigo_producto = NC.IdProDev
	WHERE P.Codigo_producto = @Codigo_producto
	)
	BEGIN 
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar el producto ya que tiene historial\n'
	END
	
	IF(@pasoreglas = 1)
	BEGIN
		DELETE FROM Producto WHERE Codigo_producto = @Codigo_producto
		set @Respuesta = 1
	END
END
