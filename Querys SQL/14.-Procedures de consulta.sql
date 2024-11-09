USE PIA_MAD_GPO01

GO
CREATE PROC SP_ListarMetodo
AS
BEGIN 
	SELECT IdMetodo, NombreMetodo FROM MetodoPago
END

GO
CREATE PROC SP_ListarPuesto
AS
BEGIN 
	SELECT IdPuesto, Nombre_Puesto FROM Puesto
END

GO
CREATE PROC SP_ListarEmpleado
AS
BEGIN 
	SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, 
	E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E
	inner join Puesto P on P.IdPuesto = E.IdPuesto
END

GO
CREATE PROC SP_ListarCajero
AS
BEGIN 
	SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, 
	E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E
	inner join Puesto P on P.IdPuesto = E.IdPuesto
END

GO
CREATE PROC SP_Login
AS
BEGIN 
	SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, 
	E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E
	inner join Puesto P on P.IdPuesto = E.IdPuesto where Estado=1
END

GO
CREATE PROC SP_ListarProducto
AS
BEGIN 
	SELECT Codigo_producto, P.Nombre, Descripcion,Unidad_medida, Costo, Precio_unitario, 
	Existencia, Unidad_vendida, Merma, Stock, Punto_reorden, D.Clave, D.Nombre[Nombre_Depto], 
	P.DescuentoProducto, P.FechaInicio, P.FechaFin, E.Nombre+ ' ' + E.ApePaterno + ' '+ E.ApeMaterno 
	AS NombreMod, P.FechaModificacion FROM Producto P
    INNER JOIN Departamento D ON D.Clave = P.Depto
    INNER JOIN Empleado E ON E.IdEmpleado=P.IdAdminMod
END

GO
CREATE PROC SP_ListarDepartamento
AS
BEGIN 
	SELECT Clave, Nombre, Descuento, DescActivo, Devolucion FROM Departamento
END

GO
CREATE PROC SP_ListarCajas
AS
BEGIN 
	SELECT Id, Num_caja, Estado FROM Caja
END

GO
CREATE PROC SP_ListarVenta
AS
BEGIN 
	Select IdVenta, IdCajero, TipoDocumento, NumeroDocumento, MontoPago, MontoCambio, MontoTotal, IdCaja FROM Venta V
    INNER JOIN Empleado E ON E.IdEmpleado= V.IdCajero
END

GO
CREATE PROC SP_ListarDetalleVenta
AS
BEGIN 
	SELECT DV.IdDetalleVenta, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, 
	DV.PrecioUnitario, DV.Descuento, V.Fecha, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V
	INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta
	INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto
END

GO
CREATE PROC SP_ListarReporteCajero
AS
BEGIN 
	SELECT Convert(DATE, V.Fecha) AS Fecha, E.Nombre, E.ApePaterno, E.ApeMaterno, 
	D.Nombre[Nombre_Depto], SUM(DV.Cantidad) AS Cantidad, SUM(DV.PrecioVenta) AS PrecioVenta, 
	SUM(DV.Utilidad) AS Utilidad, SUM(DV.Subtotal) AS Subtotal, P.Codigo_producto, D.Clave 
	FROM DetalleVenta DV
	INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto INNER JOIN 
	Departamento D ON D.Clave=P.Depto INNER JOIN Venta V ON V.IdVenta= DV.IdVenta 
	INNER JOIN Empleado E ON E.IdEmpleado= V.IdCajero
	GROUP BY Convert(DATE, V.Fecha), E.Nombre, E.ApePaterno, E.ApeMaterno, D.Nombre, P.Codigo_producto, D.Clave
END

GO
CREATE PROC SP_ListarReporteVenta
AS
BEGIN 
	SELECT Fecha, Cantidad, Subtotal, Descuento, PrecioVenta, PrecioUnitario, Utilidad, 
	Precio_unitario, Codigo_producto, Clave, Nombre, Id, Num_caja FROM ReporteVentas
END

GO
CREATE PROC SP_ListarNota
AS
BEGIN 
	SELECT ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, 
	NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC
	INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion
	INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto
END

GO
CREATE PROC SP_NotaBusquedaAvanzada(
@caja VARCHAR(10)
)
AS
BEGIN 
	SELECT Num_Devolucion, Fecha, Id, Num_caja FROM BusquedaAvanzadaNota
	WHERE Num_caja = @caja
END

GO
CREATE PROC SP_NotaBusquedaFolio(
@folio VARCHAR(10)
)
AS
BEGIN 
	SELECT Num_Devolucion, IdTicketV, Num_Recibo, Prod_regresado, Cantidad, 
	Subtotal, Total, PrecioUni, DescuentoP, Fecha, IdProDev, Id, Num_caja FROM BusquedaFolioNotaD
	WHERE Num_Devolucion= @folio
END

GO
CREATE PROC SP_ListarDetalleNota
AS
BEGIN 
	SELECT ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, 
	ND.Unidad_medida, NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC
	INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion
	INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto
END

GO
CREATE PROC SP_DetalleNotaBusquedaFolio(
@folio VARCHAR(10)
)
AS
BEGIN 
	SELECT IdVenta, Fecha, NombreProducto, Cantidad, IdProducto, Devuelto, Id, Num_caja FROM BusquedaFolioNota
	WHERE IdVenta= @folio
END

GO
CREATE PROC SP_DetalleNotaBusquedaProducto(
@folio VARCHAR(10),
@idproducto VARCHAR(10)
)
AS
BEGIN 
	SELECT V.IdVenta, V.Fecha, DV.IdDetalleVenta, DV.IdProducto, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, 
	DV.PrecioUnitario, DV.Descuento, DV.Unidad_medida, D.Devolucion, C.Id, C.Num_caja FROM Venta V
    INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta
    INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON P.Codigo_producto=DV.IdProducto
    INNER JOIN Departamento D ON P.Depto=D.Clave
	WHERE V.IdVenta= @folio AND DV.IdProducto= @idproducto
END

GO
CREATE PROC SP_BusquedaCantidadExisteNota(
@IdTicketV VARCHAR(10),
@IdProDev VARCHAR(10)
)
AS
BEGIN 
	SELECT IdTicketV, IdProDev, Cantidad FROM BusquedaCantidadExisteNota
    WHERE IdTicketV= @IdTicketV AND IdProDev=@IdProDev
END

GO
CREATE PROC SP_ListarAutorizacion(
@idempleado INT
)
AS
BEGIN 
	SELECT a.IdPuesto,a.NombreMenu FROM Autorizacion a
	inner join Puesto p on p.IdPuesto = a.IdPuesto
	inner join Empleado e on e.IdPuesto = p.IdPuesto
	WHERE e.IdEmpleado = @idempleado
END