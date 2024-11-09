USE PIA_MAD_GPO01
GO

CREATE FUNCTION ObtenerSubtotal (

@PrecioV DECIMAL(10,2), @Cantidad DECIMAL(10,2)

)         
    RETURNS  DECIMAL(10,2) 
AS
BEGIN 
      DECLARE @Subtotal DECIMAL(10,2); 

      SET @Subtotal = @PrecioV * @Cantidad; 
  
    RETURN @Subtotal; 
END 

GO

CREATE FUNCTION ObtenerDescuento (

@Descuento DECIMAL(10,2), @Cantidad DECIMAL(10,2), @PrecioV DECIMAL(10,2)

)         
    RETURNS  DECIMAL(10,2) 
AS
BEGIN 
      DECLARE @DescuentoFinal DECIMAL(10,2); 

      SET @DescuentoFinal = (@Descuento / 100) * @PrecioV * @Cantidad; 
  
    RETURN @DescuentoFinal; 
END 

GO

CREATE FUNCTION ObtenerTotalVenta (

@PrecioV DECIMAL(10,2), @Descuento DECIMAL(10,2), @Cantidad DECIMAL(10,2)

)         
    RETURNS  DECIMAL(10,2) 
AS
BEGIN 
      DECLARE @TotalFinal DECIMAL(10,2); 

      SET @TotalFinal = (@PrecioV * @Cantidad) - ((@Descuento / 100) * @PrecioV * @Cantidad);
  
    RETURN @TotalFinal; 
END 

GO

CREATE FUNCTION ObtenerUtilidadVenta (

@PrecioV DECIMAL(10,2), @Descuento DECIMAL(10,2), @Cantidad DECIMAL(10,2), @costoProducto DECIMAL(10,2)

)         
    RETURNS  DECIMAL(10,2) 
AS
BEGIN 
      DECLARE @TotalFinal DECIMAL(10,2); 

      SET @TotalFinal = ((@PrecioV * @Cantidad) - ((@Descuento / 100) * @PrecioV * @Cantidad)) - (@costoProducto * @Cantidad);
  
    RETURN @TotalFinal; 
END 

GO
