USE PIA_MAD_GPO01
GO

CREATE TRIGGER trProductoStock
ON Producto
AFTER UPDATE
as

	BEGIN
	DECLARE @OldStock INT, @NewStock  INT
	Declare @stocka bit = 0
	Declare @stocki bit = 1
	Declare @codigo_Producto INT = (Select Codigo_producto FROM inserted) 
	Declare @punto_reorden DECIMAL = (Select Punto_reorden FROM inserted)
	Declare @existencia DECIMAL = (Select Existencia FROM inserted)
	Declare @Stock BIT= (Select Stock FROM inserted)

	SELECT @OldStock = Stock FROM deleted
	SELECT @NewStock = Stock FROM inserted

	if( @existencia <= @punto_reorden)
	UPDATE Producto SET Stock = @stocka Where Codigo_producto = @codigo_Producto 
	
	if( @existencia >= @punto_reorden AND @OldStock=0)
	UPDATE Producto SET Stock = @stocki Where Codigo_producto = @codigo_Producto 
	END

GO

CREATE TRIGGER trgDespuesVenta ON DetalleVenta 
FOR INSERT
AS
	declare @CantidadVendida decimal(10,2);
	declare @IdProducto int;
	
	select @CantidadVendida=i.Cantidad from inserted i;	
	select @IdProducto=i.IdProducto from inserted i;

	UPDATE Producto SET 
			Existencia = Existencia - @CantidadVendida,
			Unidad_vendida= Unidad_vendida + @CantidadVendida
			WHERE Codigo_producto = @IdProducto

GO

CREATE TRIGGER trgDespuesDevolucion ON NotaCreditoDetalle 
FOR INSERT
AS
	declare @CantidadDevuelta decimal(10,2);
	declare @Merma BIT;
	declare @IdProducto int;
	declare @IdNota int;
	declare @NombreProd varchar(15);
	
	select @CantidadDevuelta=i.Cantidad from inserted i;
	select @Merma=i.Merma from inserted i;
	select @IdProducto=i.IdProDev from inserted i;
	select @IdNota = i.Id_Num_Dev from inserted i;
	select @NombreProd = i.Prod_regresado from inserted i;

	IF @Merma=1
		BEGIN
			UPDATE Producto SET 
			Merma = Merma + @CantidadDevuelta
			WHERE Codigo_producto = @IdProducto
			INSERT INTO Merma(prodMerma, IdNotCred, prodNombre)
			VALUES(@IdProducto, @IdNota, @NombreProd)
		END
	ELSE
		UPDATE Producto SET 
			Existencia = Existencia + @CantidadDevuelta
			WHERE Codigo_producto = @IdProducto

	

GO

